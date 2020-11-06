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
using PodioCore.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;

namespace AlMarkeeContactsAppDuplicateContainer
{
    #region StartUp
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
            var functionName = "AlMarkeeContactsAppDuplicateFunction";
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

                    // Search podio app for that valid location address
                    SearchService search = new SearchService(podio);

                    // Getting Contact App Id
                    var contactsAppId = int.Parse(dictionary["Easy Day Master|Contacts"]);
                    Console.WriteLine($"Entered Contacts - App ID: {contactsAppId}");

                    // Contacts App Fields to interact with
                    //var contactsFirstName = currentItem.Field<TextItemField>(int.Parse(dictionary["Easy Day Master|Contacts|First Name"]));
                    //var contactsLastName = currentItem.Field<TextItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Last Name"]));
                    var contactsAddressField = currentItem.Field<LocationItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Mailing Address"]));
                    var contactsPhoneField = currentItem.Field<PhoneItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Phone Number"]));
                    var contactsEmailField = currentItem.Field<PhoneItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Email Address"]));

                    #region Getting Address Value and Email Values
                    //Add address location to a string variable. 
                    Console.WriteLine($"Address Value: {contactsAddressField.Locations.FirstOrDefault()}");
                    var addressValueResult = contactsAddressField.Locations.FirstOrDefault().ToString().Trim();

                    var emailValue = new List<string>(); //List for all email attached
                    foreach (var x in contactsEmailField.Value)
                    {
                        Console.WriteLine($"Email Address: {x.Value}");
                        //Adds all email to list (Email Address field)
                        emailValue.Add(x.Value);
                    }
                    #endregion

                    #region Batch Filtering for Address
                    var addressFilter = new FilterOptions
                    {
                        Filters = new Dictionary<string, object> { { contactsAddressField.FieldId.ToString(), addressValueResult } }
                    };
                    var addressFilteredItems = await podio.FilterItems(contactsAppId, addressFilter);

                    Console.WriteLine($"{JsonConvert.SerializeObject(addressFilteredItems)}"); //Filtered Result Converted to Json

                    //Check all return items to match if they have address value from the batch reponse.
                    var newAddressFilteredList = from x in addressFilteredItems.Items
                                                 where x.ItemId != currentItem.ItemId
                                                 where x.Fields.Where(a => a.FieldId == contactsAddressField.FieldId).Select(b => b.Values.First.SelectToken("value")).FirstOrDefault().ToString() == addressValueResult
                                                 select x.ItemId;


                    #endregion

                    #region Searching Address If Filtered Found Nothing

                    // Searching app for search term (location field)
                    var searchAddressResult = new List<SearchResult>();
                    if (addressFilteredItems.Filtered <= 1 && newAddressFilteredList.Count() == 0)
                    {
                        searchAddressResult = search.SearchInApp(contactsAppId, addressValueResult.ToString()).Result;
                        Console.WriteLine($"Podio Search = {JsonConvert.SerializeObject(searchAddressResult)}");
                    }
                    

                    // Filtering the list
                    var newAddressSearchList = from x in searchAddressResult where x.Id != currentItem.ItemId select x.Id;

                    var newAddressLists = newAddressFilteredList.Concat(newAddressSearchList); //Combining Two List

                    foreach (var x in newAddressLists)
                    {
                        Console.WriteLine($"Same Address ItemID: {x}");
                    }
                    #endregion

                    #region Batch Filtering for Email Address
                    var emailFilter = new FilterOptions
                    {
                        Filters = new Dictionary<string, object> { { contactsEmailField.FieldId.ToString(), emailValue } }
                    };

                    var emailFilteredItems = await podio.FilterItems(contactsAppId, emailFilter);

                    Console.WriteLine($"{JsonConvert.SerializeObject(emailFilteredItems)}"); //Filtered Result Converted to Json

                    //Check all email in each items on weather they had emails that we added to the list earlier.
                    var newEmailAddressLists = from x in emailFilteredItems.Items
                                               where x.ItemId != currentItem.ItemId
                                               where x.Fields.Where(a => a.FieldId == contactsEmailField.FieldId).Select(b => b.Values.Children().Values("value")).Select(c => c.Any(value => emailValue.Contains(value.ToString()))).Any() == true
                                               select x.ItemId;

                    foreach (var x in newEmailAddressLists)
                    {
                        Console.WriteLine($"Same Email Address ItemID: {x}");
                    }

                    #endregion

                    #region Phone Number Search
                    // Search podio app for that valid location address
                    var searchPhoneNumberResult = new List<SearchResult>();
                    foreach (var x in contactsPhoneField.Value)
                    {
                        Console.WriteLine($"Phone Number: {x.Value}");
                        var phoneNumberValue = x.Value;
                        // Searching app for search term (Phone Number field)
                        var searchValue = search.SearchInApp(contactsAppId, phoneNumberValue).Result;
                        searchPhoneNumberResult.AddRange(searchValue);
                    }
                    // Filtering the list
                    var newPhoneNumberLists = from x in searchPhoneNumberResult where x.Id != currentItem.ItemId select x.Id;
                    #endregion

                    if (newAddressLists.Any() || newPhoneNumberLists.Any() || newEmailAddressLists.Any())
                    {
                        Console.WriteLine($"Updating ItemId: {currentItem.ItemId}");

                        // Updating the item with all the references
                        var updateItem = new Item { ItemId = currentItem.ItemId };

                        //// Updating the Title
                        //var contactsFirstNameField = updateItem.Field<TextItemField>(int.Parse(dictionary["Easy Day Master|Contacts|First Name"]));
                        //var contactsLastField = updateItem.Field<TextItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Last Name"]));

                        //contactsFirstNameField.Value = "[DUPLICATE] - ";
                        //contactsLastField.Value = $"{contactsFirstName.Value} {contactsLastName.Value}";

                        // Field Id for Address Field
                        var contactsWithTheSameAddress = updateItem.Field<AppItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Contacts with the same Address"]));

                        // Field Id for Phone Number Field
                        var contactsWithTheSamePhone = updateItem.Field<AppItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Contacts with the same Phone Number"]));

                        // Field Id for Email Address Field
                        var contactsWithTheSameEmail = updateItem.Field<AppItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Contacts with the same Email"]));

                        // Field Id for Duplicate Category Field
                        var contactsDuplicateCategoryField = updateItem.Field<CategoryItemField>(int.Parse(dictionary["Easy Day Master|Contacts|Duplicate Status"]));

                        if (newAddressLists.Any())
                        {
                            await podio.CommentOnItem("Another contact has the same address listed. Any duplicates have been attached.", currentItem.ItemId, false); Console.WriteLine("Address Duplicate Found!");
                            // Adding all the reference for address
                            contactsWithTheSameAddress.ItemIds = newAddressLists;
                            contactsDuplicateCategoryField.OptionText = "Duplicate";
                        }

                        if (newPhoneNumberLists.Any())
                        {
                            await podio.CommentOnItem("Another contact has the same phone number listed. Any duplicates have been attached.", currentItem.ItemId, false); Console.WriteLine("Phone Number Duplicate Found!");
                            // Adding all the reference for Phone Number
                            contactsWithTheSamePhone.ItemIds = newPhoneNumberLists;
                            contactsDuplicateCategoryField.OptionText = "Duplicate";
                        }

                        if (newEmailAddressLists.Any())
                        {
                            await podio.CommentOnItem("Another contact has the same email address listed. Any duplicates have been attached.", currentItem.ItemId, false); Console.WriteLine("Email Address Duplicate Found!");
                            // Adding all the reference for Email Address
                            contactsWithTheSameEmail.ItemIds = newEmailAddressLists;
                            contactsDuplicateCategoryField.OptionText = "Duplicate";
                        }

                        await podio.UpdateItem(updateItem, false);

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