using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using PodioCore;
using Saasafras.Event.Helpers;
using Saasafras.Lambda;
using PodioCore.Items;
using Saasafras.Lambda.Interfaces;
using Saasafras.Interfaces;
using Saasafras.Model;
using PodioCore.Utils.ItemFields;
using PodioCore.Models;
using PodioCore.Models.Request;
using PodioCore.Services;
using Saasafras.Event;
using C4HExcelUploadFunctionContainer;
using PodioCore.Comments;
using System.Threading;

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace C4HExcelUploadFunctionContainer
{
    public class MyHandler : IEventHandler<SaasafrasSolutionCommand<SaasafrasPodioEvent>>
    {
        private readonly ISolutionLoggerFactory solutionLoggerFactory;
        private readonly ISaasafrasDictionary saasafrasDictionary;
        private readonly IConfiguration configuration;
        private readonly IAccessTokenProvider accessTokenProvider;
        private readonly IFunctionLocker functionLocker;


        public MyHandler(IFunctionLocker functionLocker, ISolutionLoggerFactory solutionLoggerFactory, ISaasafrasDictionary saasafrasDictionary, IConfiguration configuration, IAccessTokenProvider accessTokenProvider)
        {
            this.functionLocker = functionLocker;
            this.solutionLoggerFactory = solutionLoggerFactory;
            this.saasafrasDictionary = saasafrasDictionary;
            this.configuration = configuration;
            this.accessTokenProvider = accessTokenProvider;
        }

        public async Task<FunctionContainerResponse> Handle(SaasafrasSolutionCommand<SaasafrasPodioEvent> command)
        {
            client.log("This will go the client log file.");
            solution.log("This will go to the solution log file.");

            //you can get metadata on the trigger v
            //if (command.resource.type != "item.create")
            //throw new Exception("expecting an item.create event");

            //check for valid itemId
            if (!int.TryParse(command.resource.item_id, out int itemId))
                throw new ArgumentException("we were expecting an integer");

            //get Podio client 
            var podio = new Podio(accessTokenProvider);

            var userService = new PodioCore.Services.UserService(podio);
            solution.log("calling podio");
            var user = await userService.GetUser();
            solution.log($"podio api user_id : {user.UserId}");
            solution.log($"podio api email : {user.Mail}");

            //get item that fired the event
            var currentItem = await podio.GetFullItem(itemId) ?? throw new Exception($"failed to get item {itemId}");
            client.log($"Item was created in {currentItem.App.Config.Name}.");

            //How to use the dictionary
            //var appId = await saasafrasDictionary.GetKeyAsInt("Space Name|App Name");
            //if (appId != currentItem.App.AppId)
            //    solution.log($"");

            //update the function name
            var functionName = "C4HExcelUploadFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }
            try
            {
                //FUNCTION CODE HERE !!
                // **** Use this to model calling the dictionary and using it to map fields on excel upload ******
                // **** https://github.com/saasafras/SaasafrasPodioFunction/tree/master/PodioFileImportFunction ****

                await podio.CommentOnItem("Beginning upload.", currentItem.ItemId, false);
                Console.WriteLine("Entered try block of C4HExcelUploadFunction");

                var dictionary = await saasafrasDictionary.GetDictionary();
                Console.WriteLine("Got saasafras dictionary.");

                int excelUploadAppId = int.Parse(dictionary["C4H Import Skiptrace|Excel Upload"]);
                int leadsAppId = int.Parse(dictionary["C4H Lead Gen|Leads"]);
                Console.WriteLine("Got the excel upload and leads app ids.");

                var importMapping = ImportMappingBuilder.MakeImportMapping("C4H Lead Gen", "Leads", dictionary)
                    .AddTextFieldMapping("Owner", 0)
                    .AddTextFieldMapping("Property Address", 1)
                    .AddTextFieldMapping("County", 2)
                    .AddLocationFieldMapping("Seller Mailing Address", 3, ImportMappingBuilder.LocationField.value)
                    .AddOtherFieldMapping("CAD Link", 4)
                    .AddTextFieldMapping("Subdivision", 5)
                    .AddOtherFieldMapping("Acres", 6)
                    .AddOtherFieldMapping("Taxes Owed", 7)
                    .AddTextFieldMapping("Phone Number With Text", 8)
                    .AddTextFieldMapping("Lead Source", 9)
                    .AddTextFieldMapping("Legal Description", 10)
                    .AddTextFieldMapping("Document #", 11)
                    .GetMappingFields();
                Console.WriteLine("Mapped fields from excel sheet.");

                var fileId = currentItem.Files.First().FileId;
                Console.WriteLine($"The file id of the attached excel sheet is {fileId}.");

                var fileService = new FileService(podio);
                var importFileId = await fileService.CopyFile(fileId);
                Console.WriteLine("Copied file.");

                var importService = new ImporterService(podio);
                var importedItems = await importService.ImportAppItems(importFileId, leadsAppId, importMapping);

                await podio.CommentOnItem("Upload complete. Leads successfully created in C4H Lead Gen workspace.", currentItem.ItemId, false);
                
                Console.WriteLine("Getting and setting category field to update.");
                var catFieldToUpdate = int.Parse(dictionary["C4H Import Skiptrace|Excel Upload|Import Status"]);
                var updatedItem = new Item() { ItemId = currentItem.ItemId };
                var updatedField = new CategoryItemField
                {
                    FieldId = catFieldToUpdate,
                    OptionText = "Imported"
                };

                updatedItem.Fields.Add(updatedField);

                var response = await podio.UpdateItem(updatedItem, false);

                Console.WriteLine("Updated the excel import item to Import Status as 'Imported'.");

                // waiting before changing Import Status field to Imported for triggering round robin.
                Console.WriteLine("Waiting 15 seconds before beginning round robin. This will give Podio time to register the import before triggering next function.");
                await podio.CommentOnItem("Will begin Round Robin Funtion in 15 seconds.", currentItem.ItemId, false);
                Thread.Sleep(15000);

                await podio.CommentOnItem("Beginning round robin assigning imported leads a C4H call rep.", currentItem.ItemId, false);
                var staffManagementAppId = await saasafrasDictionary.GetKeyAsInt("Databases|Staff Management");

                // Outbound Caller View
                #region Outbound Callers
                Console.WriteLine("Getting 'Outbound Caller' View");

                var viewServ = new ViewService(podio);
                var views = await viewServ.GetViews(staffManagementAppId);

                IEnumerable<View> view;

                view = from v in views
                       where v.Name == "Outbound Caller"
                       select v;

                Console.WriteLine("Got 'Outbound Caller' View");

                var filterOptions = new FilterOptions
                {
                    Filters = view.First().Filters
                };

                Console.WriteLine("Created 'Outbound Caller' filter options");

                var callerFilter = await podio.FilterItems(staffManagementAppId, filterOptions);
                var callers = callerFilter.Items;
                Console.WriteLine($"There are {callers.Count()} staff to assign leads to.");
                #endregion

                // Get lead items that have not been assigned
                Console.WriteLine("Getting 'Rep Not Assigned' View");

                viewServ = new ViewService(podio);
                views = await viewServ.GetViews(leadsAppId);

                IEnumerable<View> desiredView;

                desiredView = from v in views
                              where v.Name == "Rep NOT Assigned"
                              select v;

                Console.WriteLine("Got 'Rep Not Assigned' View");

                var filter = new FilterOptions
                {
                    Filters = desiredView.First().Filters
                };

                Console.WriteLine("Created filter options");

                var itemsToUpdate = await podio.FilterItems(leadsAppId, filter);

                // Get lead items that have been assigned to see last outbound caller assigned
                Console.WriteLine("Getting 'Rep Assign' View");

                IEnumerable<View> assignView;

                assignView = from v in views
                             where v.Name == "Rep Assign"
                             select v;

                Console.WriteLine("Got 'Rep Assign' View");

                var repAssign = new FilterOptions
                {
                    Limit = 1,
                    Filters = assignView.First().Filters
                };

                Console.WriteLine("Created filter options");

                // getting last assigned C4H rep
                int fieldId;
                var repAssignItems = await podio.FilterItems(leadsAppId, repAssign);
                var lastAssigned = repAssignItems.Items.First();
                var lastAssignedRep = lastAssigned.Field<AppItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("C4H Lead Gen|Leads|C4H Rep"));
                var rep = lastAssignedRep.Items.First();

                // getting index of last assigned rep to know where to start assigning new leads
                List<int> employeeList = new List<int>();
                foreach (var i in callers)
                {
                    employeeList.Add(i.ItemId);
                }

                var index = employeeList.IndexOf(rep.ItemId);
                Console.WriteLine($"The last assigned rep was {rep.Title} at index {index} in the list of employees.");

                // if last assigned c4h rep is not in employeeList, it will start at index [0]

                // Letting user know how many items to reassign.
                // set up counter
                var count = itemsToUpdate.Items.Count();
                var c = 0;
                var intro = count == 1 ? "There is" : "There are";
                var item = count == 1 ? "item" : "items";
                await podio.CommentOnItem($"{intro} {count} {item} to assign.", currentItem.ItemId, false);
                Console.WriteLine($"{intro} {count} {item} to assign.");
                var appFieldToUpdate = saasafrasDictionary.GetKeyAsInt("C4H Lead Gen|Leads|C4H Rep");
                var leadCatFieldToUpdate = saasafrasDictionary.GetKeyAsInt("C4H Lead Gen|Leads|Assignment Status");
                var staffItems = callerFilter.Items;
                var staffCount = staffItems.Count();

                foreach (var update in itemsToUpdate.Items)
                {
                    // go to next item in callerFilter collection
                    index++;
                    // if who to give leads to is smaller than leads to give away, start give leads to over at top.
                    if (index >= staffCount)
                    {
                        index = 0;
                    }

                    c++;
                    Console.WriteLine("Updating item number " + c + " with the item id of " + update.ItemId + " titled " + update.Title + " out of " + count + " in the collection.");
                    var tempItem = await podio.GetItem(update.ItemId);

                    var updateItem = new Item { ItemId = tempItem.ItemId };

                    var updateField = new AppItemField
                    {
                        FieldId = await appFieldToUpdate,
                        ItemId = callerFilter.Items.ElementAt(index).ItemId
                    };
                    updateItem.Fields.Add(updateField);

                    var updateCatField = new CategoryItemField
                    {
                        FieldId = await leadCatFieldToUpdate,
                        OptionText = "Assigned"
                    };

                    updateItem.Fields.Add(updateCatField);
                    Console.WriteLine($"Assigned the lead to {staffItems.ElementAt(index).Title} from the filtered staff items.");

                    response = await podio.UpdateItem(updateItem, false);
                }

                Console.WriteLine($"{c} leads have been assigned a rep.");
                await podio.CommentOnItem($"{c} leads have been assigned a rep.\nImport Process Complete!", currentItem.ItemId, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Function {functionName}");
                Console.WriteLine($"Exception: {ex}");
            }

            finally
            {
                await functionLocker.UnlockFunction(functionName, uniqueId, lockId);
            }

            return new FunctionContainerResponse
            {
                message = "success"
            };
        }
        public async System.Threading.Tasks.Task Init(IServiceProvider services, IConfiguration configuration)
        {
            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
