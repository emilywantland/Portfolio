using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using PodioCore;
using Saasafras.Event.Helpers;
using Saasafras.Lambda;
using PodioCore.Items;
using Saasafras.Lambda.Interfaces;
using Saasafras.Interfaces;
using Saasafras.Model;
using System.Linq;
using PodioCore.Utils.ItemFields;
using System.Collections.Generic;
using PodioCore.Models;
using PodioCore.Models.Request;
using PodioCore.Services;
using Saasafras.Event;
using PodioCore.Comments;
using Newtonsoft.Json;

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace AlMarkeePropertiesAppDuplicateContainer
{
    #region Startup
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

            //update the function name
            var functionName = "AlMarkeePropertiesAppDuplicateFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }
            #endregion

            try
            {
                if (command.resource.type == "item.create")
                {
                    Console.WriteLine("Entered Try Block");

                    var dictionary = await saasafrasDictionary.GetDictionary();

                    // Setting place holders for the fieldId
                    int fieldId = 0;

                    // Properties App Fields to interact with
                    var propertiesAddressField = currentItem.Field<LocationItemField>(fieldId = int.Parse(dictionary["Easy Day Master|Properties|Property Address"]));

                    // Pulling the full address off the item
                    Console.WriteLine($"Entered Location");
                    var locationArray = propertiesAddressField.Locations.FirstOrDefault();
                    Console.WriteLine($"{locationArray}");

                    // Search podio app for that valid location address
                    SearchService search = new SearchService(podio);

                    // Properties App ID
                    var propertiesAppId = int.Parse(dictionary["Easy Day Master|Properties"]);
                    Console.WriteLine($"Properties App Id: {propertiesAppId}");

                    // Filtering app for search term (location field)
                    var addressFilter = new FilterOptions
                    {
                        Filters = new Dictionary<string, object> { { propertiesAddressField.FieldId.ToString(), locationArray } }
                    };
                    var addressFilteredItems = await podio.FilterItems(propertiesAppId, addressFilter);

                    Console.WriteLine($"{JsonConvert.SerializeObject(addressFilteredItems)}"); //Filtered Result Converted to Json

                    // Filtering the list
                    var newAddressFilteredList = from x in addressFilteredItems.Items where x.ItemId != currentItem.ItemId select x.ItemId;

                    // Searching app for search term (location field)
                    var searchAddressResult = new List<SearchResult>();
                    if (addressFilteredItems.Filtered <= 1 && newAddressFilteredList.Count() == 0)
                    {
                        searchAddressResult = search.SearchInApp(propertiesAppId, addressFilter.ToString()).Result;
                        Console.WriteLine($"Podio Search = {JsonConvert.SerializeObject(searchAddressResult)}");
                    }

                    // Filtering the list
                    var newAddressSearchList = from x in searchAddressResult where x.Id != currentItem.ItemId select x.Id;

                    var newAddressLists = newAddressFilteredList.Concat(newAddressSearchList); //Combining Two List

                    Console.WriteLine($"{newAddressLists.Count()} Duplicates Found");

                    if (newAddressLists.Any())
                    {
                        Console.WriteLine($"Updating ItemId: {currentItem.ItemId}");

                        // Updating the item with all the references
                        var updateItem = new Item { ItemId = currentItem.ItemId };

                        // Field Id for Duplicate Category Field
                        var propertiesDuplicateCategoryField = updateItem.Field<CategoryItemField>(int.Parse(dictionary["Easy Day Master|Properties|Duplicate Status"]));

                        propertiesDuplicateCategoryField.OptionText = "Duplicate";

                        //// Updating the title
                        //var propertiesTitleField = updateItem.Field<TextItemField>(int.Parse(dictionary["Easy Day Master|Properties|Property Title"]));

                        //propertiesTitleField.Value = $"[DUPLICATE] {propertiesTitleField.Value}";

                        // Adding all the references
                        var propertieswiththeSameAddress = updateItem.Field<AppItemField>(int.Parse(dictionary["Easy Day Master|Properties|Properties with the Same Address"]));
                        propertieswiththeSameAddress.ItemIds = newAddressLists;

                        await podio.UpdateItem(updateItem, false);

                        await podio.CommentOnItem("Another property has the same address listed. Any duplicates have been attached.", currentItem.ItemId, false);

                        Console.WriteLine("Done Updating Item");

                    }

                    else
                    {
                        Console.WriteLine("No Duplicate Found!");
                    }
                }
            }

            #region Ending
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
#endregion