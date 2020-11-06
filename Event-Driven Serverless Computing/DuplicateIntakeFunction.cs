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

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace LouForwardNewDuplicateIntakeContainer
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
            var functionName = "TemplateFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }
            #endregion
            try
            {
                var dictionary = await saasafrasDictionary.GetDictionary();

                // Logging
                Console.WriteLine("Entered Try Block");

                var clientProfileAppId = int.Parse(dictionary["1. Client Management|Client Profile"]);

                var viewServ = new ViewService(podio);
                var views = await viewServ.GetViews(clientProfileAppId);

                IEnumerable<View> view;

                view = from v in views
                       where v.Name == "By Created Date."
                       select v;

                Console.WriteLine("Got View");

                // Filter Limit
                const int limit = 100;
                var filterOptions = new FilterOptions
                {
                    Limit = limit,
                    Filters = view.First().Filters
                };

                Console.WriteLine("Created filter options");

                // Setting place holders for the fieldId and iteration number and declaring iteration
                int fieldId = 0;
                int i = 0;
                Item iteration;

                // Getting App ID's
                var duplicateStagingAppId = int.Parse(dictionary["1. Client Management|Duplicate Staging"]);
                var preStartProfilesAppId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles"]);
                var businessProfileAppId = int.Parse(dictionary["1. Client Management|Business Profile"]);

                // Getting the list of profile items in Client Profile
                var profileList = await podio.FilterItems(clientProfileAppId, filterOptions);

                #region Get Items in Intake Form
                // Intake Form App
                var intakeFirstNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|First Name"]));
                var intakeLastNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Last Name"]));
                var intakeCompanyNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Company Name"]));
                var intakeAddressField = currentItem.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Address"]));
                var intakeAdditionalAddressField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Additional Address Information"]));
                var intakeEmailAddressField = currentItem.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Email"]));
                var intakeWebsiteField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Website"]));
                var intakePhoneField = currentItem.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Phone"]));
                var intakeContactPreferenceField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Contact Preference"]));
                var intakeBusinessConceptField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Please describe your business or concept."]));
                var intakeApplyToBusinessField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Do any of the following apply to your business"]));
                var intakeBusinessOpenField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|When did your business open?"]));
                var intakeFullTimeEmployeesField = currentItem.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|How many full-time employees (or equivalents)?"]));
                var intakeFinancingField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Do you have financing in place?"]));
                var intakeInterestedFinanceField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Are you interested in financing for your business?"]));
                var intakeFinanceSeekingField = currentItem.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|If yes, how much are you seeking?"]));
                var intakeAssistanceField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Please tell us about the assistance you need:"]));
                var intakeExitStrategyField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Do you have an Exit Strategy / Succession Plan in place?"]));
                var intakeSalesForecastField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Are you acheiving annual sales forecasts?"]));
                var intakeCompetitionUnderstandField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|How well do you understand your competition?"]));
                var intakeGrowthBarriersField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|What are some of the barriers to the growth of your business?  Check all that apply."]));
                var intakeAssistedBeforeField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Have you received business assistance before? (Including workshops, consultation, education)"]));
                var intakePleaseExplainField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|If so, please explain."]));
                var intakeHearResourceField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|How did you hear about this resource?"]));
                var intakeTypeOfBusinessField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Type of Business"]));
                var intakeStageOfBusinessField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Please choose one that best reflects your stage of business."]));
                var intakeBusinessPlanField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|Do you have a business plan?"]));
                var intakeNewIdeaField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Intake Form|If this is a new idea, please describe your experience in this field of work:"]));
                #endregion

                #region Non-Profit Case
                // If the client indicated their business is a non-profit...
                if (intakeTypeOfBusinessField.Options.First().Text == "Non-Profit")
                {
                    Console.WriteLine("Client selected Non-Profit");
                    await podio.CommentOnItem($"Client was sent a 'Non - Profit Information' email at {DateTime.Now}", currentItem.ItemId, true);

                    // Globiflow will run and send an email

                    // Do not duplicate check, end the flow
                    goto PostLoop;
                }
                #endregion

                var currentEmailList = intakeEmailAddressField.Value.ToArray();
                Console.WriteLine($"Checking against email: {currentEmailList[0].Value.ToString()}");
                var currentEmail = currentEmailList[0].Value.ToString();

                // For each profile in the profile list...
                foreach (var item in profileList.Items)
                {
                    // Logging
                    Console.WriteLine($"Iteration: {i}");

                    iteration = await podio.GetItem(item.ItemId);

                    // Getting Client Profile name's for log check
                    var profileFirstNameField = iteration.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|First Name"]));
                    var profileLastNameField = iteration.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Last Name"]));

                    // Logging
                    if (profileFirstNameField.Value != null)
                    {
                        Console.WriteLine($"Client First Name: {profileFirstNameField.Value.ToString()}");
                    }
                    if (profileLastNameField.Value != null)
                    {
                        Console.WriteLine($"Client Last Name: {profileLastNameField.Value.ToString()}");
                    }

                    // Getting Client Profile Email
                    var currentProfileEmailField = iteration.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Email"]));

                    // If the intake email address and the client profile email for this iteration are not null...
                    if (intakeEmailAddressField.Value != null && currentProfileEmailField.Value != null)
                    {
                        // Adding emails to an array
                        var profileEmailList = currentProfileEmailField.Value.ToArray();

                        // For each email in the client profile email field for this iteration...
                        foreach (var e in profileEmailList)
                        {
                            // If the client profile email is equal to the intake email...
                            if (e.Value == currentEmail)
                            {
                                // Logging
                                Console.WriteLine("Found an email match");
                                Console.WriteLine($"{e.Value} should equal {currentEmail}");

                                Item newDuplicate = new Item();

                                #region Get Items in Duplicate Staging
                                // Duplicate Staging App
                                var duplicateFirstNameField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|First Name"]));
                                var duplicateLastNameField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Last Name"]));
                                var duplicateCompanyNameField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Company Name"]));
                                var duplicateAddressField = newDuplicate.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Address"]));
                                var duplicateAdditionalAddressField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Additional Address Information"]));
                                var duplicateEmailAddressField = newDuplicate.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Email"]));
                                var duplicateWebsiteField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Website"]));
                                var duplicatePhoneField = newDuplicate.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Phone"]));
                                var duplicateContactPreferenceField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Contact Preference"]));
                                var duplicateTypeOfBusinessField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Type of Business"]));
                                var duplicateStageOfBusinessField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Please choose one that best reflects your stage of business."]));
                                var duplicateApplyToBusinessField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Do any of the following apply to your business"]));
                                var duplicateBusinessConceptField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Please describe your business or concept."]));
                                var duplicateBusinessOpenField = newDuplicate.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|When did your business open?"]));
                                var duplicateFullTimeEmployeesField = newDuplicate.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|How many full-time employees (or equivalents)?"]));
                                var duplicateBusinessPlanField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Do you have a business plan?"]));
                                var duplicateFinancingField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Do you have financing in place?"]));
                                var duplicateInterestedFinanceField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Are you interested in financing for your business?"]));
                                var duplicateFinanceSeekingField = newDuplicate.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|If yes, how much are you seeking?"]));
                                var duplicateAssistanceField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Please tell us about the assistance you need:"]));
                                var duplicateExitStrategyField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Do you have an Exit Strategy / Succession Plan in place?"]));
                                var duplicateSalesForecastField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Are you acheiving annual sales forecasts?"]));
                                var duplicateCompetitionUnderstandField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|How well do you understand your competition?"]));
                                var duplicateGrowthBarriersField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|What are some of the barriers to the growth of your business?  Check all that apply."]));
                                var duplicateAssistedBeforeField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|Have you received business assistance before?"]));
                                var duplicatePleaseExplainField = newDuplicate.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|If so, please explain."]));
                                var duplicateHearResourceField = newDuplicate.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Duplicate Staging|How did you hear about this resource?"]));
                                #endregion

                                #region Null Checks - Creating New Duplicate
                                // Assigning values to new duplicate fields
                                if (intakeFirstNameField.Value != null)
                                {
                                    duplicateFirstNameField.Value = intakeFirstNameField.Value;
                                }
                                if (intakeLastNameField.Value != null)
                                {
                                    duplicateLastNameField.Value = intakeLastNameField.Value;
                                }
                                if (intakeCompanyNameField.Value != null)
                                {
                                    duplicateCompanyNameField.Value = intakeCompanyNameField.Value;
                                }
                                if (intakeAddressField.Locations.Count() > 0)
                                {
                                    duplicateAddressField.Location = intakeAddressField.Locations.First();
                                }
                                if (intakeAdditionalAddressField.Value != null)
                                {
                                    duplicateAdditionalAddressField.Value = intakeAdditionalAddressField.Value;
                                }
                                if (intakeEmailAddressField.Value != null)
                                {
                                    AddEmails(intakeEmailAddressField);
                                    duplicateEmailAddressField.Value = intakeEmailAddressField.Value;
                                }
                                if (intakeWebsiteField.Value != null)
                                {
                                    duplicateWebsiteField.Value = intakeWebsiteField.Value;
                                }
                                if (intakePhoneField.Value != null)
                                {
                                    AddPhoneNumbers(intakePhoneField);
                                    duplicatePhoneField.Value = intakePhoneField.Value;
                                }
                                if (intakeContactPreferenceField.Options.Any())
                                {
                                    duplicateContactPreferenceField.OptionText = intakeContactPreferenceField.Options.First().Text;
                                }
                                if (intakeTypeOfBusinessField.Options.Any())
                                {
                                    duplicateTypeOfBusinessField.OptionText = intakeTypeOfBusinessField.Options.First().Text;
                                }
                                if (intakeApplyToBusinessField.Options.Any())
                                {
                                    duplicateApplyToBusinessField.OptionText = intakeApplyToBusinessField.Options.First().Text;
                                }
                                if (intakeBusinessConceptField.Value != null)
                                {
                                    duplicateBusinessConceptField.Value = intakeBusinessConceptField.Value;
                                }
                                if (intakeBusinessOpenField.Values != null)
                                {
                                    duplicateBusinessOpenField.Values = intakeBusinessOpenField.Values;
                                }
                                if (intakeFullTimeEmployeesField.Value != null)
                                {
                                    duplicateFullTimeEmployeesField.Value = intakeFullTimeEmployeesField.Value;
                                }
                                if (intakeBusinessPlanField.Options.Any())
                                {
                                    duplicateBusinessPlanField.OptionText = intakeBusinessPlanField.Options.First().Text;
                                }
                                if (intakeFinancingField.Options.Any())
                                {
                                    duplicateFinancingField.OptionText = intakeFinancingField.Options.First().Text;
                                }
                                if (intakeFinanceSeekingField.Value != null)
                                {
                                    duplicateFinanceSeekingField.Value = intakeFinanceSeekingField.Value;
                                }
                                if (intakeAssistanceField.Values != null)
                                {
                                    duplicateAssistanceField.Value = StripHTML(intakeAssistanceField.Value);
                                }
                                if (intakeExitStrategyField.Options.Any())
                                {
                                    duplicateExitStrategyField.OptionText = intakeExitStrategyField.Options.First().Text;
                                }
                                if (intakeSalesForecastField.Options.Any())
                                {
                                    duplicateSalesForecastField.OptionText = intakeSalesForecastField.Options.First().Text;
                                }
                                if (intakeCompetitionUnderstandField.Options.Any())
                                {
                                    duplicateCompetitionUnderstandField.OptionText = intakeCompetitionUnderstandField.Options.First().Text;
                                }
                                if (intakeGrowthBarriersField.Options.Any())
                                {
                                    GetOptions(intakeGrowthBarriersField, duplicateGrowthBarriersField);
                                }
                                if (intakeAssistedBeforeField.Options.Any())
                                {
                                    duplicateAssistedBeforeField.OptionText = intakeAssistedBeforeField.Options.First().Text;
                                }
                                if (intakePleaseExplainField.Value != null)
                                {
                                    duplicatePleaseExplainField.Value = StripHTML(intakePleaseExplainField.Value);
                                }
                                if (intakeHearResourceField.Options.Any())
                                {
                                    duplicateHearResourceField.OptionText = intakeHearResourceField.Options.First().Text;
                                }
                                #endregion

                                await podio.CreateItem(newDuplicate, duplicateStagingAppId, false);
                                await podio.CommentOnItem("This lead was sent to duplicate staging, it can be deleted or kept.", currentItem.ItemId, false);
                                Console.WriteLine("Duplicate Created");

                                goto PostLoop;
                            }
                        }
                    }
                    i++;
                }
                // Logging
                Console.WriteLine("No Dup found");

                // Setting duplicate found to false
                var filterMatch = false;

                #region No Match Case Conditions
                // Checking conditions necessary for flows down the line
                if (intakeTypeOfBusinessField.Options.Any()
                   && intakeTypeOfBusinessField.Options.First().Text != "Non-Profit"
                   && intakeStageOfBusinessField.Options.Any()
                   && (intakeStageOfBusinessField.Options.First().Text == "Idea Stage / Pre-Start Up" || intakeStageOfBusinessField.Options.First().Text == "Start-Up")
                   && intakeBusinessPlanField.Options.Any()
                   && intakeBusinessPlanField.Options.First().Text == "No")
                {
                    // Setting duplicate case to true
                    filterMatch = true;
                }
                #endregion

                // If duplicate case is true...
                if (filterMatch)
                {
                    Item newClientProfile = new Item();

                    #region Get Items in Client Profile
                    var clientProfileFirstNameField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|First Name"]));
                    var clientProfileLastNameField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Last Name"]));
                    var clientProfileBusinessAddressField = newClientProfile.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Business Address"]));
                    var clientProfileAdditionalAddressField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Additional Address Information"]));
                    var clientProfileEmailAddressField = newClientProfile.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Email"]));
                    var clientProfilePhoneField = newClientProfile.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Phone"]));
                    var clientProfileContactPreferenceField = newClientProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Contact Preferance"]));
                    var clientProfileAssistedBeforeField = newClientProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Have you received business assistance before?"]));
                    var clientProfilePleaseExplainField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|If so, please explain."]));
                    #endregion

                    #region Null Checks - Creating New Client Profile
                    if (intakeFirstNameField.Value != null)
                    {
                        clientProfileFirstNameField.Value = intakeFirstNameField.Value;
                    }
                    if (intakeLastNameField.Value != null)
                    {
                        clientProfileLastNameField.Value = intakeLastNameField.Value;
                    }
                    if (intakeAddressField.Locations.Count() > 0)
                    {
                        clientProfileBusinessAddressField.Location = intakeAddressField.Locations.First();
                    }
                    if (intakeAdditionalAddressField.Value != null)
                    {
                        clientProfileAdditionalAddressField.Value = intakeAdditionalAddressField.Value;
                    }
                    if (intakeEmailAddressField.Value != null)
                    {
                        AddEmails(intakeEmailAddressField);
                        clientProfileEmailAddressField.Value = intakeEmailAddressField.Value;
                    }
                    if (intakePhoneField.Value != null)
                    {
                        AddPhoneNumbers(intakePhoneField);
                        clientProfilePhoneField.Value = intakePhoneField.Value;
                    }
                    if (intakeContactPreferenceField.Options.Any())
                    {
                        clientProfileContactPreferenceField.OptionText = intakeContactPreferenceField.Options.First().Text;
                    }
                    if (intakeAssistedBeforeField.Options.Any())
                    {
                        clientProfileAssistedBeforeField.OptionText = intakeAssistedBeforeField.Options.First().Text;
                    }
                    if (intakePleaseExplainField.Value != null)
                    {
                        clientProfilePleaseExplainField.Value = StripHTML(intakePleaseExplainField.Value);
                    }
                    #endregion

                    var createdProfile = await podio.CreateItem(newClientProfile, clientProfileAppId, false);
                    await podio.CommentOnItem("This lead was sent to Client Profile.", currentItem.ItemId, false);
                    Console.WriteLine("Client Profile Created");

                    Item newPreStartProfile = new Item();

                    #region Get Items in Pre-Start Profiles
                    var preStartCompanyNameField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Company Name (if applicable)"]));
                    var preStartFullNameField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Client Full Name"]));
                    var preStartFileStatusField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Pre-Start File Status"]));
                    var preStartPhaseField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Pre-Start Phase"]));
                    var preStartAttendedEntrepreneurshipField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Attended Exploring Entrepreneurship"]));
                    var preStartClientInformationField = newPreStartProfile.Field<AppItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Client Information"]));
                    var preStartBusinessAddressField = newPreStartProfile.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Business Address"]));
                    var preStartEmailAddressField = newPreStartProfile.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Email"]));
                    var preStartEmailTextField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Email Text"]));
                    var preStartPhoneField = newPreStartProfile.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Phone"]));
                    var preStartWebsiteField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Business Web Address"]));
                    var preStartContactPreferenceField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Contact Preferance"]));
                    var preStartApplyToBusinessField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Do any of the following apply to your business"]));
                    var preStartBusinessOpenField = newPreStartProfile.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Projected Business Launch (if applicable)"]));
                    var preStartProjectedEmployeesField = newPreStartProfile.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Projected Number of Employees"]));
                    var preStartHearResourceField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|How did you hear about this resource?"]));
                    var preStartBusinessConceptField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Please describe your business."]));
                    var preStartBusinessPlanField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Do you have a business plan?"]));
                    var preStartFinancingField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Do you have financing in place?"]));
                    var preStartInterestedFinanceField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Are you interested in financing for your business?"]));
                    var preStartFinanceSeekingField = newPreStartProfile.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|If yes, how much are you seeking?"]));
                    var preStartCompetitionUnderstandField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|How well do you understand your competition?"]));
                    var preStartGrowthBarriersField = newPreStartProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|What are some of the barriers to the growth of your business?  Check all that apply."]));
                    var preStartOtherBarriersField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Any other barriers?"]));
                    var preStartFileCreationDateField = newPreStartProfile.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Pre-Start File Creation Date"]));
                    var preStartClientFullNameField = newPreStartProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|Client Full Name"]));
                    #endregion

                    #region Null Checks - Creating New Pre-Start Profile
                    preStartFileStatusField.OptionText = "Lead";
                    preStartPhaseField.OptionText = "Warm";
                    preStartAttendedEntrepreneurshipField.OptionText = "No";
                    preStartFileCreationDateField.Start = DateTime.Now.Date;
                    preStartClientInformationField.ItemId = createdProfile;

                    // Can't set until pre start profile is created - Caleb's structure

                    if (intakeCompanyNameField.Value != null)
                    {
                        preStartCompanyNameField.Value = intakeCompanyNameField.Value;
                    }
                    if (intakeAddressField.Locations.Count() > 0)
                    {
                        preStartBusinessAddressField.Location = intakeAddressField.Locations.First();
                    }
                    if (intakeEmailAddressField.Value != null)
                    {
                        AddEmails(intakeEmailAddressField);
                        preStartEmailAddressField.Value = intakeEmailAddressField.Value;
                    }
                    if (intakePhoneField.Value != null)
                    {
                        AddPhoneNumbers(intakePhoneField);
                        preStartPhoneField.Value = intakePhoneField.Value;
                    }
                    if (intakeWebsiteField.Value != null)
                    {
                        preStartWebsiteField.Value = intakeWebsiteField.Value;
                    }
                    if (intakeContactPreferenceField.Options.Any())
                    {
                        preStartContactPreferenceField.OptionText = intakeContactPreferenceField.Options.First().Text;
                    }
                    if (intakeApplyToBusinessField.Options.Any())
                    {
                        preStartApplyToBusinessField.OptionText = intakeApplyToBusinessField.Options.First().Text;
                    }
                    if (intakeBusinessOpenField.HasValue())
                    {
                        preStartBusinessOpenField.Start = intakeBusinessOpenField.Start;
                    }
                    if (intakeFullTimeEmployeesField.Value != null)
                    {
                        preStartProjectedEmployeesField.Value = intakeFullTimeEmployeesField.Value;
                    }
                    if (intakeHearResourceField.Options.Any())
                    {
                        preStartHearResourceField.OptionText = intakeHearResourceField.Options.First().Text;
                    }
                    if (intakeBusinessConceptField.Value != null)
                    {
                        preStartBusinessConceptField.Value = intakeBusinessConceptField.Value;
                    }
                    if (intakeBusinessPlanField.Options.Any())
                    {
                        preStartBusinessPlanField.OptionText = intakeBusinessPlanField.Options.First().Text;
                    }
                    if (intakeFinancingField.Options.Any())
                    {
                        preStartFinancingField.OptionText = intakeFinancingField.Options.First().Text;
                    }
                    if (intakeInterestedFinanceField.Options.Any())
                    {
                        preStartInterestedFinanceField.OptionText = intakeInterestedFinanceField.Options.First().Text;
                    }
                    if (intakeFinanceSeekingField.Value != null)
                    {
                        preStartFinanceSeekingField.Value = intakeFinanceSeekingField.Value;
                    }
                    if (intakeCompetitionUnderstandField.Options.Any())
                    {
                        preStartCompetitionUnderstandField.OptionText = intakeCompetitionUnderstandField.Options.First().Text;
                    }
                    if (intakeGrowthBarriersField.Options.Any())
                    {
                        GetOptions(intakeGrowthBarriersField, preStartGrowthBarriersField);
                    }
                    if (intakeAssistanceField.Values != null)
                    {
                        preStartOtherBarriersField.Value = StripHTML(intakeAssistanceField.Value);
                    }
                    if (intakeFirstNameField != null && intakeLastNameField != null)
                    {
                        preStartFullNameField.Value = intakeLastNameField.Value + ", " + intakeFirstNameField.Value;
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

                    var updatePrestartIdNumberField = updatePreStart.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Pre-Start Profiles|ID NUMBER"]));

                    updatePrestartIdNumberField.Value = createdPreStart.ItemId.ToString();
                    await podio.UpdateItem(updatePreStart, false);
                }

                // If duplicate case is false...
                else
                {
                    Item newClientProfile = new Item();

                    #region Get Items in Client Profile
                    var clientProfileFirstNameField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|First Name"]));
                    var clientProfileLastNameField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Last Name"]));
                    var clientProfileCompanyNameField = newClientProfile.Field<AppItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Company Name"]));
                    var clientProfileBusinessAddressField = newClientProfile.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Business Address"]));
                    var clientProfileAdditionalAddressField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Additional Address Information"]));
                    var clientProfileEmailAddressField = newClientProfile.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Email"]));
                    var clientProfilePhoneField = newClientProfile.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Phone"]));
                    var clientProfileContactPreferenceField = newClientProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Contact Preferance"]));
                    var clientProfileAssistedBeforeField = newClientProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|Have you received business assistance before?"]));
                    var clientProfilePleaseExplainField = newClientProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Client Profile|If so, please explain."]));
                    #endregion

                    #region Null Checks - Creating New Client Profile
                    if (intakeFirstNameField.Value != null)
                    {
                        clientProfileFirstNameField.Value = intakeFirstNameField.Value;
                    }
                    if (intakeLastNameField.Value != null)
                    {
                        clientProfileLastNameField.Value = intakeLastNameField.Value;
                    }
                    if (intakeAddressField.Locations.Count() > 0)
                    {
                        clientProfileBusinessAddressField.Location = intakeAddressField.Locations.First();
                    }
                    if (intakeAdditionalAddressField.Value != null)
                    {
                        clientProfileAdditionalAddressField.Value = intakeAdditionalAddressField.Value;
                    }
                    if (intakeEmailAddressField.Value != null)
                    {
                        AddEmails(intakeEmailAddressField);
                        clientProfileEmailAddressField.Value = intakeEmailAddressField.Value;
                    }
                    if (intakePhoneField.Value != null)
                    {
                        AddPhoneNumbers(intakePhoneField);
                        clientProfilePhoneField.Value = intakePhoneField.Value;
                    }
                    if (intakeContactPreferenceField.Options.Any())
                    {
                        clientProfileContactPreferenceField.OptionText = intakeContactPreferenceField.Options.First().Text;
                    }
                    if (intakeAssistedBeforeField.Options.Any())
                    {
                        clientProfileAssistedBeforeField.OptionText = intakeAssistedBeforeField.Options.First().Text;
                    }
                    if (intakePleaseExplainField.Value != null)
                    {
                        clientProfilePleaseExplainField.Value = StripHTML(intakePleaseExplainField.Value);
                    }
                    #endregion

                    var createdProfile = await podio.CreateItem(newClientProfile, clientProfileAppId, false);
                    await podio.CommentOnItem("This lead was sent to Client Profile.", currentItem.ItemId, false);
                    Console.WriteLine("Client Profile Created");

                    Item newBusinessProfile = new Item();

                    #region Get Items in Business Profile
                    var busClientNameField = newBusinessProfile.Field<AppItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Client Name"]));
                    var busCompanyNameField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Company Name"]));
                    var busBusinessAddressField = newBusinessProfile.Field<LocationItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Business Address"]));
                    var busAdditionalAddressField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Additional Address Information"]));
                    var busEmailAddressField = newBusinessProfile.Field<EmailItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Email"]));
                    var busPhoneField = newBusinessProfile.Field<PhoneItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Phone"]));
                    var busContactPreferenceField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Contact Preferance"]));
                    var busApplyToBusinessField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Do any of the following apply to your business"]));
                    var busStageOfBusinessField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Please choose one that best reflects your stage of business."]));
                    var busBusinessOpenField = newBusinessProfile.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|When did your business open?"]));
                    var busBusinessConceptField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Please describe your business."]));
                    var busBusinessPlanField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Do you have a business plan?"]));
                    var busFinancingField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Do you have financing in place?"]));
                    var busInterestedFinanceField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Are you interested in financing for your business?"]));
                    var busSalesForecastField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Are you acheiving annual sales forcasts?"]));
                    var busCompetitionUnderstandField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|How well do you understand your competition?"]));
                    var busGrowthBarriersField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|What are some of the barriers to the growth of your business?  Check all that apply."]));
                    var busOtherBarriersField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Any other barriers?"]));
                    var busHearResourceField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|How did you hear about this resource?"]));
                    var busFinanceSeekingField = newBusinessProfile.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|If yes, how much are you seeking?"]));
                    var busNumberOfEmployeesField = newBusinessProfile.Field<NumericItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Number of Employees"]));
                    var busExitStrategyField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Do you have an Exit Strategy / Succession Plan in place?"]));
                    var busFileStatusField = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Business File Status"]));
                    var busFileCreationDateField = newBusinessProfile.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Business File Creation Date"]));
                    var busDateLastProvidedChangeField = newBusinessProfile.Field<DateItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Date of Last Provider Assign or Change"]));
                    var busWebsiteField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Business Web Address"]));
                    var busNewIdeaField = newBusinessProfile.Field<TextItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|If this is a new idea, please describe your experience in this field of work:"]));
                    var busUnsubscribed = newBusinessProfile.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1. Client Management|Business Profile|Unsubscribed?"]));
                    #endregion

                    #region Null Checks - Creating New Business Profile
                    busFileStatusField.OptionText = "Lead";
                    busFileCreationDateField.Start = DateTime.Now.Date;
                    busDateLastProvidedChangeField.Start = DateTime.Now.Date;
                    busClientNameField.ItemId = createdProfile;
                    busUnsubscribed.OptionText = "No";

                    if (intakeCompanyNameField.Value != null)
                    {
                        busCompanyNameField.Value = intakeCompanyNameField.Value;
                    }
                    if (intakeAddressField.Locations.Count() > 0)
                    {
                        busBusinessAddressField.Location = intakeAddressField.Locations.First();
                    }
                    if (intakeAdditionalAddressField.Value != null)
                    {
                        busAdditionalAddressField.Value = intakeAdditionalAddressField.Value;
                    }
                    if (intakeEmailAddressField.Value != null)
                    {
                        AddEmails(intakeEmailAddressField);
                        busEmailAddressField.Value = intakeEmailAddressField.Value;
                    }
                    if (intakePhoneField.Value != null)
                    {
                        AddPhoneNumbers(intakePhoneField);
                        busPhoneField.Value = intakePhoneField.Value;
                    }
                    if (intakeContactPreferenceField.Options.Any())
                    {
                        busContactPreferenceField.OptionText = intakeContactPreferenceField.Options.First().Text;
                    }
                    if (intakeApplyToBusinessField.Options.Any())
                    {
                        busApplyToBusinessField.OptionText = intakeApplyToBusinessField.Options.First().Text;
                    }
                    if (intakeBusinessConceptField.Value != null)
                    {
                        busBusinessConceptField.Value = intakeBusinessConceptField.Value;
                    }
                    if (intakeBusinessOpenField.Values != null)
                    {
                        busBusinessOpenField.Values = intakeBusinessOpenField.Values;
                    }
                    if (intakeFullTimeEmployeesField.Value != null)
                    {
                        busNumberOfEmployeesField.Value = intakeFullTimeEmployeesField.Value;
                    }
                    if (intakeBusinessPlanField.Options.Any())
                    {
                        busBusinessPlanField.OptionText = intakeBusinessPlanField.Options.First().Text;
                    }
                    if (intakeFinancingField.Options.Any())
                    {
                        busFinancingField.OptionText = intakeFinancingField.Options.First().Text;
                    }
                    if (intakeFinanceSeekingField.Value != null)
                    {
                        busFinanceSeekingField.Value = intakeFinanceSeekingField.Value;
                    }
                    if (intakeExitStrategyField.Options.Any())
                    {
                        busExitStrategyField.OptionText = intakeExitStrategyField.Options.First().Text;
                    }
                    if (intakeSalesForecastField.Options.Any())
                    {
                        busSalesForecastField.OptionText = intakeSalesForecastField.Options.First().Text;
                    }
                    if (intakeCompetitionUnderstandField.Options.Any())
                    {
                        busCompetitionUnderstandField.OptionText = intakeCompetitionUnderstandField.Options.First().Text;
                    }
                    if (intakeGrowthBarriersField.Options.Any())
                    {
                        GetOptions(intakeGrowthBarriersField, busGrowthBarriersField);
                    }
                    if (intakeHearResourceField.Options.Any())
                    {
                        busHearResourceField.OptionText = intakeHearResourceField.Options.First().Text;
                    }
                    if (intakeAssistanceField.Values != null)
                    {
                        busOtherBarriersField.Value = StripHTML(intakeAssistanceField.Value);
                    }
                    if (intakeInterestedFinanceField.Options.Any())
                    {
                        busInterestedFinanceField.OptionText = intakeInterestedFinanceField.Options.First().Text;
                    }
                    if (intakeStageOfBusinessField.Options.Any())
                    {
                        busStageOfBusinessField.OptionText = intakeStageOfBusinessField.Options.First().Text;
                    }
                    if (intakeWebsiteField.Value != null)
                    {
                        busWebsiteField.Value = intakeWebsiteField.Value;
                    }
                    if (intakeNewIdeaField.Value != null)
                    {
                        busNewIdeaField.Value = intakeNewIdeaField.Value;
                    }
                    #endregion

                    var createdBusProfile = await podio.CreateItem(newBusinessProfile, businessProfileAppId, true);
                    await podio.CommentOnItem("This lead was sent to Business Profile.", currentItem.ItemId, false);
                    Console.WriteLine("Business Profile Created");

                    // Fills in Company Name in Client Profile after Business Profile is Created
                    //var updateClientProfile = new Item() { ItemId = currentItem.ItemId };
                    //clientProfileCompanyNameField.ItemId = createdBusProfile;
                    //await podio.UpdateItem(updateClientProfile, false);
                }
                PostLoop:
                Console.WriteLine("Exited Loop");
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