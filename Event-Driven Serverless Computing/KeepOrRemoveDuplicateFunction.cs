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
using System.Text.RegularExpressions;
using Newtonsoft.Json;

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace LouForwardKeepOrRemoveDuplicateContainer
{
    #region Setup
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

        public void AddEmails(EmailItemField email)
        {
            var emails = new List<EmailPhoneFieldResult>();
            foreach (var e in email.Value)
            {
                emails.Add(new EmailPhoneFieldResult
                {
                    Value = e.Value,
                    Type = e.Type,
                });
            }
            email.Value = emails;
        }

        public void AddPhoneNumbers(PhoneItemField numbers)
        {
            var phoneNumbers = new List<EmailPhoneFieldResult>();
            foreach (var number in numbers.Value)
            {
                phoneNumbers.Add(new EmailPhoneFieldResult
                {
                    Value = number.Value,
                    Type = number.Type,
                });
            }
            numbers.Value = phoneNumbers;
        }

        public void GetOptions(CategoryItemField category, CategoryItemField category2)
        {
            var options = new List<string>();
            foreach (var opt in category.Options)
            {
                options.Add(opt.Text);
            }
            category2.OptionTexts = options;
        }

        public static string StripHTML(string input) => Regex.Replace(input, "<.*?>", String.Empty);

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
            var appId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging");
            if (appId != currentItem.App.AppId)
                solution.log($"");

            //update the function name
            var functionName = "LouForwardKeepOrRemoveDuplicateFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }
            #endregion

            try
            {
                // Setting place holder for the fieldId
                int fieldId = 0;

                // Getting App ID's
                var clientProfileAppId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile");
                var preStartProfilesAppId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles");
                var duplicateStagingAppId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging");
                var businessProfileAppId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile");

                var duplicateKeepRemoveLeadField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Keep/Remove Lead"));

                int? revisionId = 0;
                if (command.resource.type == "item.update")
                {
                    var revision = await podio.GetRevisionDifference(Convert.ToInt32(currentItem.ItemId), currentItem.CurrentRevision.Revision - 1, currentItem.CurrentRevision.Revision);
                    revisionId = revision.First().FieldId;
                }

                #region Get Items in Duplicate Staging
                // Duplicate Staging App
                var duplicateFirstNameField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|First Name"));
                var duplicateLastNameField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Last Name"));
                var duplicateCompanyNameField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Company Name"));
                var duplicateAddressField = currentItem.Field<LocationItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Address"));
                var duplicateAdditionalAddressField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Additional Address Information"));
                var duplicateEmailAddressField = currentItem.Field<EmailItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Email"));
                var duplicateWebsiteField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Website"));
                var duplicatePhoneField = currentItem.Field<PhoneItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Phone"));
                var duplicateContactPreferenceField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Contact Preference"));
                var duplicateTypeOfBusinessField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Type of Business"));
                var duplicateStageOfBusinessField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Please choose one that best reflects your stage of business."));
                var duplicateApplyToBusinessField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Do any of the following apply to your business"));
                var duplicateBusinessConceptField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Please describe your business or concept."));
                var duplicateBusinessOpenField = currentItem.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|When did your business open?"));
                var duplicateFullTimeEmployeesField = currentItem.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|How many full-time employees (or equivalents)?"));
                var duplicateBusinessPlanField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Do you have a business plan?"));
                var duplicateFinancingField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Do you have financing in place?"));
                var duplicateInterestedFinanceField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Are you interested in financing for your business?"));
                var duplicateFinanceSeekingField = currentItem.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|If yes, how much are you seeking?"));
                var duplicateAssistanceField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Please tell us about the assistance you need:"));
                var duplicateExitStrategyField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Do you have an Exit Strategy / Succession Plan in place?"));
                var duplicateSalesForecastField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Are you acheiving annual sales forecasts?"));
                var duplicateCompetitionUnderstandField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|How well do you understand your competition?"));
                var duplicateGrowthBarriersField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|What are some of the barriers to the growth of your business?  Check all that apply."));
                var duplicateAssistedBeforeField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|Have you received business assistance before?"));
                var duplicatePleaseExplainField = currentItem.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|If so, please explain."));
                var duplicateHearResourceField = currentItem.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Duplicate Staging|How did you hear about this resource?"));
                #endregion

                // Setting duplicate found to false
                var filterMatch = false;

                // Checking conditions necessary for flows down the line
                if (duplicateTypeOfBusinessField.Options.Any()
                   && duplicateTypeOfBusinessField.Options.First().Text != "Non-Profit"
                   && duplicateStageOfBusinessField.Options.Any()
                   && duplicateStageOfBusinessField.Options.First().Text == "Idea Stage / Pre-Start Up"
                   && duplicateBusinessPlanField.Options.Any()
                   && duplicateBusinessPlanField.Options.First().Text == "No")
                {
                    // Setting duplicate case to true
                    filterMatch = true;
                }

                if (revisionId == duplicateKeepRemoveLeadField.FieldId && duplicateKeepRemoveLeadField.Options.Any() && duplicateKeepRemoveLeadField.Options.First().Text == "Remove Duplicate")
                {
                    Console.WriteLine("Remove Duplicate was Selected.");
                    await podio.DeleteItem(currentItem.ItemId, false);
                    Console.WriteLine("Item was deleted.");
                }
                else if (revisionId == duplicateKeepRemoveLeadField.FieldId && duplicateKeepRemoveLeadField.Options.Any() && duplicateKeepRemoveLeadField.Options.First().Text == "Keep Lead")
                {
                    Console.WriteLine("Keep Lead Was Selected");

                    // If duplicate case is true...
                    if (filterMatch)
                    {
                        Item newClientProfile = new Item();

                        #region Get Items in Client Profile
                        var clientProfileFirstNameField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|First Name"));
                        var clientProfileLastNameField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Last Name"));
                        var clientProfileBusinessAddressField = newClientProfile.Field<LocationItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Business Address"));
                        var clientProfileAdditionalAddressField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Additional Address Information"));
                        var clientProfileEmailAddressField = newClientProfile.Field<EmailItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Email"));
                        var clientProfilePhoneField = newClientProfile.Field<PhoneItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Phone"));
                        var clientProfileContactPreferenceField = newClientProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Contact Preferance"));
                        var clientProfileAssistedBeforeField = newClientProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Have you received business assistance before?"));
                        var clientProfilePleaseExplainField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|If so, please explain."));
                        #endregion

                        #region Null Checks - Creating New Client Profile
                        if (duplicateFirstNameField.Value != null)
                        {
                            clientProfileFirstNameField.Value = duplicateFirstNameField.Value;
                        }
                        if (duplicateLastNameField.Value != null)
                        {
                            clientProfileLastNameField.Value = duplicateLastNameField.Value;
                        }
                        if (duplicateAddressField.Locations.Count() > 0)
                        {
                            clientProfileBusinessAddressField.Location = duplicateAddressField.Locations.First();
                        }
                        if (duplicateAdditionalAddressField.Value != null)
                        {
                            clientProfileAdditionalAddressField.Value = duplicateAdditionalAddressField.Value;
                        }
                        if (duplicateEmailAddressField.Value != null)
                        {
                            AddEmails(duplicateEmailAddressField);
                            clientProfileEmailAddressField.Value = duplicateEmailAddressField.Value;
                        }
                        if (duplicatePhoneField.Value != null)
                        {
                            AddPhoneNumbers(duplicatePhoneField);
                            clientProfilePhoneField.Value = duplicatePhoneField.Value;
                        }
                        if (duplicateContactPreferenceField.Options.Any())
                        {
                            clientProfileContactPreferenceField.OptionText = duplicateContactPreferenceField.Options.First().Text;
                        }
                        if (duplicateAssistedBeforeField.Options.Any())
                        {
                            clientProfileAssistedBeforeField.OptionText = duplicateAssistedBeforeField.Options.First().Text;
                        }
                        if (duplicatePleaseExplainField.Value != null)
                        {
                            clientProfilePleaseExplainField.Value = StripHTML(duplicatePleaseExplainField.Value);
                        }
                        #endregion

                        var createdProfile = await podio.CreateItem(newClientProfile, clientProfileAppId, false);
                        await podio.CommentOnItem("This lead was sent to Client Profile.", currentItem.ItemId, false);
                        Console.WriteLine("Client Profile Created");

                        Item newPreStartProfile = new Item();

                        #region Get Items in Pre-Start Profiles
                        var preStartCompanyNameField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Company Name (if applicable)"));
                        var preStartFullNameField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Client Full Name"));
                        var preStartFileStatusField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Pre-Start File Status"));
                        var preStartPhaseField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Pre-Start Phase"));
                        var preStartAttendedEntrepreneurshipField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Attended Exploring Entrepreneurship"));
                        var preStartClientInformationField = newPreStartProfile.Field<AppItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Client Information"));
                        var preStartBusinessAddressField = newPreStartProfile.Field<LocationItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Business Address"));
                        var preStartEmailAddressField = newPreStartProfile.Field<EmailItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Email"));
                        var preStartEmailTextField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Email Text"));
                        var preStartPhoneField = newPreStartProfile.Field<PhoneItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Phone"));
                        var preStartWebsiteField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Business Web Address"));
                        var preStartContactPreferenceField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Contact Preferance"));
                        var preStartApplyToBusinessField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Do any of the following apply to your business"));
                        var preStartBusinessOpenField = newPreStartProfile.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Projected Business Launch (if applicable)"));
                        var preStartProjectedEmployeesField = newPreStartProfile.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Projected Number of Employees"));
                        var preStartHearResourceField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|How did you hear about this resource?"));
                        var preStartBusinessConceptField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Please describe your business."));
                        var preStartBusinessPlanField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Do you have a business plan?"));
                        var preStartFinancingField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Do you have financing in place?"));
                        var preStartInterestedFinanceField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Are you interested in financing for your business?"));
                        var preStartFinanceSeekingField = newPreStartProfile.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|If yes, how much are you seeking?"));
                        var preStartCompetitionUnderstandField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|How well do you understand your competition?"));
                        var preStartGrowthBarriersField = newPreStartProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|What are some of the barriers to the growth of your business?  Check all that apply."));
                        var preStartOtherBarriersField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Any other barriers?"));
                        var preStartFileCreationDateField = newPreStartProfile.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Pre-Start File Creation Date"));
                        var preStartClientFullNameField = newPreStartProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|Client Full Name"));
                        #endregion

                        #region Null Checks - Creating New Pre-Start Profile
                        preStartFileStatusField.OptionText = "Lead";
                        preStartPhaseField.OptionText = "Warm";
                        preStartAttendedEntrepreneurshipField.OptionText = "No";
                        preStartFileCreationDateField.Start = DateTime.Now.Date;
                        preStartClientInformationField.ItemId = createdProfile;

                        // Can't set until pre start profile is created - Caleb's structure

                        if (duplicateCompanyNameField.Value != null)
                        {
                            preStartCompanyNameField.Value = duplicateCompanyNameField.Value;
                        }
                        if (duplicateAddressField.Locations.Count() > 0)
                        {
                            preStartBusinessAddressField.Location = duplicateAddressField.Locations.First();
                        }
                        if (duplicateEmailAddressField.Value != null)
                        {
                            AddEmails(duplicateEmailAddressField);
                            preStartEmailAddressField.Value = duplicateEmailAddressField.Value;
                        }
                        if (duplicatePhoneField.Value != null)
                        {
                            AddPhoneNumbers(duplicatePhoneField);
                            preStartPhoneField.Value = duplicatePhoneField.Value;
                        }
                        if (duplicateWebsiteField.Value != null)
                        {
                            preStartWebsiteField.Value = duplicateWebsiteField.Value;
                        }
                        if (duplicateContactPreferenceField.Options.Any())
                        {
                            preStartContactPreferenceField.OptionText = duplicateContactPreferenceField.Options.First().Text;
                        }
                        if (duplicateApplyToBusinessField.Options.Any())
                        {
                            preStartApplyToBusinessField.OptionText = duplicateApplyToBusinessField.Options.First().Text;
                        }
                        if (duplicateBusinessOpenField.HasValue())
                        {
                            preStartBusinessOpenField.Start = duplicateBusinessOpenField.Start;
                        }
                        if (duplicateFullTimeEmployeesField.Value != null)
                        {
                            preStartProjectedEmployeesField.Value = duplicateFullTimeEmployeesField.Value;
                        }
                        if (duplicateHearResourceField.Options.Any())
                        {
                            preStartHearResourceField.OptionText = duplicateHearResourceField.Options.First().Text;
                        }
                        if (duplicateBusinessConceptField.Value != null)
                        {
                            preStartBusinessConceptField.Value = duplicateBusinessConceptField.Value;
                        }
                        if (duplicateBusinessPlanField.Options.Any())
                        {
                            preStartBusinessPlanField.OptionText = duplicateBusinessPlanField.Options.First().Text;
                        }
                        if (duplicateFinancingField.Options.Any())
                        {
                            preStartFinancingField.OptionText = duplicateFinancingField.Options.First().Text;
                        }
                        if (duplicateInterestedFinanceField.Options.Any())
                        {
                            preStartInterestedFinanceField.OptionText = duplicateInterestedFinanceField.Options.First().Text;
                        }
                        if (duplicateFinanceSeekingField.Value != null)
                        {
                            preStartFinanceSeekingField.Value = duplicateFinanceSeekingField.Value;
                        }
                        if (duplicateCompetitionUnderstandField.Options.Any())
                        {
                            preStartCompetitionUnderstandField.OptionText = duplicateCompetitionUnderstandField.Options.First().Text;
                        }
                        if (duplicateGrowthBarriersField.Options.Any())
                        {
                            GetOptions(duplicateGrowthBarriersField, preStartGrowthBarriersField);
                        }
                        if (duplicateAssistanceField.Values != null)
                        {
                            preStartOtherBarriersField.Value = StripHTML(duplicateAssistanceField.Value);
                        }
                        if (duplicateFirstNameField != null && duplicateLastNameField != null)
                        {
                            preStartFullNameField.Value = duplicateLastNameField.Value + ", " + duplicateFirstNameField.Value;
                        }
                        #endregion

                        var createPreStart = await podio.CreateItem(newPreStartProfile, preStartProfilesAppId, false);
                        await podio.CommentOnItem("This lead was sent to Pre-Start Profiles.", currentItem.ItemId, false);
                        Console.WriteLine("Pre-Start Profile Created");
                        var createdPreStart = await podio.GetItem(createPreStart);

                        Item updatePreStart = new Item
                        {
                            ItemId = createdPreStart.ItemId
                        };

                        var updatePrestartIdNumberField = updatePreStart.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Pre-Start Profiles|ID NUMBER"));

                        updatePrestartIdNumberField.Value = createdPreStart.ItemId.ToString();
                        await podio.UpdateItem(updatePreStart, false);
                    }

                    // If duplicate case is false...
                    else
                    {
                        Item newClientProfile = new Item();

                        #region Get Items in Client Profile
                        var clientProfileFirstNameField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|First Name"));
                        var clientProfileLastNameField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Last Name"));
                        var clientProfileCompanyNameField = newClientProfile.Field<AppItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Company Name"));
                        var clientProfileBusinessAddressField = newClientProfile.Field<LocationItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Business Address"));
                        var clientProfileAdditionalAddressField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Additional Address Information"));
                        var clientProfileEmailAddressField = newClientProfile.Field<EmailItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Email"));
                        var clientProfilePhoneField = newClientProfile.Field<PhoneItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Phone"));
                        var clientProfileContactPreferenceField = newClientProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Contact Preferance"));
                        var clientProfileAssistedBeforeField = newClientProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|Have you received business assistance before?"));
                        var clientProfilePleaseExplainField = newClientProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Client Profile|If so, please explain."));
                        #endregion

                        #region Null Checks - Creating New Client Profile
                        if (duplicateFirstNameField.Value != null)
                        {
                            clientProfileFirstNameField.Value = duplicateFirstNameField.Value;
                        }
                        if (duplicateLastNameField.Value != null)
                        {
                            clientProfileLastNameField.Value = duplicateLastNameField.Value;
                        }
                        if (duplicateAddressField.Locations.Count() > 0)
                        {
                            clientProfileBusinessAddressField.Location = duplicateAddressField.Locations.First();
                        }
                        if (duplicateAdditionalAddressField.Value != null)
                        {
                            clientProfileAdditionalAddressField.Value = duplicateAdditionalAddressField.Value;
                        }
                        if (duplicateEmailAddressField.Value != null)
                        {
                            AddEmails(duplicateEmailAddressField);
                            clientProfileEmailAddressField.Value = duplicateEmailAddressField.Value;
                        }
                        if (duplicatePhoneField.Value != null)
                        {
                            AddPhoneNumbers(duplicatePhoneField);
                            clientProfilePhoneField.Value = duplicatePhoneField.Value;
                        }
                        if (duplicateContactPreferenceField.Options.Any())
                        {
                            clientProfileContactPreferenceField.OptionText = duplicateContactPreferenceField.Options.First().Text;
                        }
                        if (duplicateAssistedBeforeField.Options.Any())
                        {
                            clientProfileAssistedBeforeField.OptionText = duplicateAssistedBeforeField.Options.First().Text;
                        }
                        if (duplicatePleaseExplainField.Value != null)
                        {
                            clientProfilePleaseExplainField.Value = StripHTML(duplicatePleaseExplainField.Value);
                        }
                        #endregion

                        var createdProfile = await podio.CreateItem(newClientProfile, clientProfileAppId, false);
                        await podio.CommentOnItem("This lead was sent to Client Profile.", currentItem.ItemId, false);
                        Console.WriteLine("Client Profile Created");

                        Item newBusinessProfile = new Item();

                        #region Get Items in Business Profile
                        var busClientNameField = newBusinessProfile.Field<AppItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Client Name"));
                        var busCompanyNameField = newBusinessProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Company Name"));
                        var busBusinessAddressField = newBusinessProfile.Field<LocationItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Business Address"));
                        var busAdditionalAddressField = newBusinessProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Additional Address Information"));
                        var busEmailAddressField = newBusinessProfile.Field<EmailItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Email"));
                        var busPhoneField = newBusinessProfile.Field<PhoneItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Phone"));
                        var busContactPreferenceField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Contact Preferance"));
                        var busApplyToBusinessField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Do any of the following apply to your business"));
                        var busStageOfBusinessField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Please choose one that best reflects your stage of business."));
                        var busBusinessOpenField = newBusinessProfile.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|When did your business open?"));
                        var busBusinessConceptField = newBusinessProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Please describe your business."));
                        var busBusinessPlanField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Do you have a business plan?"));
                        var busFinancingField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Do you have financing in place?"));
                        var busInterestedFinanceField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Are you interested in financing for your business?"));
                        var busSalesForecastField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Are you acheiving annual sales forcasts?"));
                        var busCompetitionUnderstandField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|How well do you understand your competition?"));
                        var busGrowthBarriersField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|What are some of the barriers to the growth of your business?  Check all that apply."));
                        var busOtherBarriersField = newBusinessProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Any other barriers?"));
                        var busHearResourceField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|How did you hear about this resource?"));
                        var busFinanceSeekingField = newBusinessProfile.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|If yes, how much are you seeking?"));
                        var busNumberOfEmployeesField = newBusinessProfile.Field<NumericItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Number of Employees"));
                        var busExitStrategyField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Do you have an Exit Strategy / Succession Plan in place?"));
                        var busFileStatusField = newBusinessProfile.Field<CategoryItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Business File Status"));
                        var busFileCreationDateField = newBusinessProfile.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Business File Creation Date"));
                        var busDateLastProvidedChangeField = newBusinessProfile.Field<DateItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Date of Last Provider Assign or Change"));
                        var busWebsiteField = newBusinessProfile.Field<TextItemField>(fieldId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile|Business Web Address"));
                        #endregion

                        #region Null Checks - Creating New Business Profile
                        busFileStatusField.OptionText = "Lead";
                        busFileCreationDateField.Start = DateTime.Now.Date;
                        busDateLastProvidedChangeField.Start = DateTime.Now.Date;
                        busClientNameField.ItemId = createdProfile;

                        if (duplicateCompanyNameField.Value != null)
                        {
                            busCompanyNameField.Value = duplicateCompanyNameField.Value;
                        }
                        if (duplicateAddressField.Locations.Count() > 0)
                        {
                            busBusinessAddressField.Location = duplicateAddressField.Locations.First();
                        }
                        if (duplicateAdditionalAddressField.Value != null)
                        {
                            busAdditionalAddressField.Value = duplicateAdditionalAddressField.Value;
                        }
                        if (duplicateEmailAddressField.Value != null)
                        {
                            AddEmails(duplicateEmailAddressField);
                            busEmailAddressField.Value = duplicateEmailAddressField.Value;
                        }
                        if (duplicatePhoneField.Value != null)
                        {
                            AddPhoneNumbers(duplicatePhoneField);
                            busPhoneField.Value = duplicatePhoneField.Value;
                        }
                        if (duplicateContactPreferenceField.Options.Any())
                        {
                            busContactPreferenceField.OptionText = duplicateContactPreferenceField.Options.First().Text;
                        }
                        if (duplicateApplyToBusinessField.Options.Any())
                        {
                            busApplyToBusinessField.OptionText = duplicateApplyToBusinessField.Options.First().Text;
                        }
                        if (duplicateBusinessConceptField.Value != null)
                        {
                            busBusinessConceptField.Value = duplicateBusinessConceptField.Value;
                        }
                        if (duplicateBusinessOpenField.Values != null)
                        {
                            busBusinessOpenField.Values = duplicateBusinessOpenField.Values;
                        }
                        if (duplicateFullTimeEmployeesField.Value != null)
                        {
                            busNumberOfEmployeesField.Value = duplicateFullTimeEmployeesField.Value;
                        }
                        if (duplicateBusinessPlanField.Options.Any())
                        {
                            busBusinessPlanField.OptionText = duplicateBusinessPlanField.Options.First().Text;
                        }
                        if (duplicateFinancingField.Options.Any())
                        {
                            busFinancingField.OptionText = duplicateFinancingField.Options.First().Text;
                        }
                        if (duplicateFinanceSeekingField.Value != null)
                        {
                            busFinanceSeekingField.Value = duplicateFinanceSeekingField.Value;
                        }
                        if (duplicateExitStrategyField.Options.Any())
                        {
                            busExitStrategyField.OptionText = duplicateExitStrategyField.Options.First().Text;
                        }
                        if (duplicateSalesForecastField.Options.Any())
                        {
                            busSalesForecastField.OptionText = duplicateSalesForecastField.Options.First().Text;
                        }
                        if (duplicateCompetitionUnderstandField.Options.Any())
                        {
                            busCompetitionUnderstandField.OptionText = duplicateCompetitionUnderstandField.Options.First().Text;
                        }
                        if (duplicateGrowthBarriersField.Options.Any())
                        {
                            GetOptions(duplicateGrowthBarriersField, busGrowthBarriersField);
                        }
                        if (duplicateHearResourceField.Options.Any())
                        {
                            busHearResourceField.OptionText = duplicateHearResourceField.Options.First().Text;
                        }
                        if (duplicateAssistanceField.Values != null)
                        {
                            busOtherBarriersField.Value = StripHTML(duplicateAssistanceField.Value);
                        }
                        if (duplicateInterestedFinanceField.Options.Any())
                        {
                            busInterestedFinanceField.OptionText = duplicateInterestedFinanceField.Options.First().Text;
                        }
                        if (duplicateStageOfBusinessField.Options.Any())
                        {
                            busStageOfBusinessField.OptionText = duplicateStageOfBusinessField.Options.First().Text;
                        }
                        if (duplicateWebsiteField.Value != null)
                        {
                            busWebsiteField.Value = duplicateWebsiteField.Value;
                        }
                        #endregion

                        var createdBusProfile = await podio.CreateItem(newBusinessProfile, businessProfileAppId, false);
                        await podio.CommentOnItem("This lead was sent to Business Profile.", currentItem.ItemId, false);
                        Console.WriteLine("Business Profile Created");

                        // Fills in Company Name in Client Profile after Business Profile is Created
                        var updateClientProfile = new Item() { ItemId = currentItem.ItemId };
                        clientProfileCompanyNameField.ItemId = createdBusProfile;
                        await podio.UpdateItem(updateClientProfile, false);
                    }
                }
                else
                {
                    await podio.CommentOnItem("Keep or Remove was Unset", currentItem.ItemId, false);
                    Console.WriteLine("Keep or Remove was Unset");
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