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

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace RepaytientEnrollmentCreateProfilesContainer
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
            var functionName = "RepaytientEnrollmentCreateProfilesFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }
            #endregion

            try
            {
                // Logging
                Console.WriteLine("Entered Try Block");

                // Getting the dictionary service
                var dictionary = await saasafrasDictionary.GetDictionary();

                // Getting App ID's
                var enrollmentsAppId = int.Parse(dictionary["1- Template|Enrollments"]);
                var clientsAppId = int.Parse(dictionary["1- Template|Clients"]);
                var allEnrollmentsAppId = int.Parse(dictionary["* Repaytient *|All Enrollments"]);
                var clientProfilesAppId = int.Parse(dictionary["* Repaytient *|Client Profiles"]);

                // Setting place holders for the fieldId
                int fieldId = 0;

                // Getting Field ID's

                #region Enrollments App Field ID's
                // Getting Field ID's for Items in the Enrollments App
                var enrollmentsFirstNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|First Name"]));
                var enrollmentsLastNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Last Name"]));
                var enrollmentsStreetAddressField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Street Address"]));
                var enrollmentsCityField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|City"]));
                var enrollmentsStateField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|State"]));
                var enrollmentsPostalCodeField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|State"]));
                var enrollmentsSSNField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Social Security Number (SSN)"]));
                var enrollmentsDOBField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Date of Birth"]));
                var enrollmentsEmailField = currentItem.Field<EmailItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Email"]));
                var enrollmentsHospitalOrFacilityField = currentItem.Field<AppItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Hospital or Facility"]));
                var enrollmentsPaymentPlanAmountField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Payment Plan Amount"]));
                var enrollmentsTypeOfInsuranceField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Type of Insurance"]));
                var enrollmentsCurrentEmployerField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Current Employer"]));
                var enrollmentsPayFrequencyField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Pay Frequency"]));
                var enrollmentsDateOfNextPayDateField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Date of Next Pay Date"]));
                var enrollmentsGrossMonthlyIncomeField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Gross Monthly Income"]));
                //var enrollmentsFrequencyOfRepaymentField = currentItem.Field<CalculationItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Frequency of Repayment"]));
                var enrollmentsRepaymentDateField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Repayment Date"]));
                var enrollmentsPaymentAmountField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Payment Amount"]));
                var enrollmentsPaymentPlanStatusField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Payment Plan Status"]));
                var enrollmentsTermOptionsField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["1- Template|Enrollments|Term Options"]));
                #endregion

                #region Clients App Field ID's
                // Getting Field ID's for Items in the Clients App
                var clientsFirstNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|First Name"]));
                var clientsLastNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|Last Name"]));
                var clientsStreetAddressField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|Street Address"]));
                var clientsCityField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|City"]));
                var clientsStateField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|State"]));
                var clientsPostalCodeField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|State"]));
                var clientsSSNField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|Social Security Number"]));
                var clientsDOBField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|Date of Birth"]));
                var clientsEmailField = currentItem.Field<EmailItemField>(fieldId = int.Parse(dictionary["1- Template|Clients|Email"]));
                #endregion

                #region All Enrollments App Field ID's
                // Getting Field ID's for Items in the All Enrollments App
                var allEnrollmentsFirstNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|First Name"]));
                var allEnrollmentsLastNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Last Name"]));
                var allEnrollmentsStreetAddressField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Street Address"]));
                var allEnrollmentsCityField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|City"]));
                var allEnrollmentsStateField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|State"]));
                var allEnrollmentsPostalCodeField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|State"]));
                var allEnrollmentsSSNField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Social Security Number (SSN)"]));
                var allEnrollmentsDOBField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Date of Birth"]));
                var allEnrollmentsEmailField = currentItem.Field<EmailItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Email"]));
                var allEnrollmentsHospitalOrFacilityField = currentItem.Field<AppItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Hospital or Facility"]));
                var allEnrollmentsPaymentPlanAmountField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Payment Plan Amount"]));
                var allEnrollmentsTypeOfInsuranceField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Type of Insurance"]));
                var allEnrollmentsCurrentEmployerField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Current Employer"]));
                var allEnrollmentsPayFrequencyField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Pay Frequency"]));
                var allEnrollmentsDateOfNextPayDateField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Date of Next Pay Date"]));
                var allEnrollmentsGrossMonthlyIncomeField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Gross Monthly Income"]));
                //var allEnrollmentsFrequencyOfRepaymentField = currentItem.Field<CalculationItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Frequency of Repayment"]));
                var allEnrollmentsRepaymentDateField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Repayment Date"]));
                var allEnrollmentsPaymentAmountField = currentItem.Field<MoneyItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Payment Amount"]));
                var allEnrollmentsPaymentPlanStatusField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Payment Plan Status"]));
                var allEnrollmentsTermOptionsField = currentItem.Field<CategoryItemField>(fieldId = int.Parse(dictionary["* Repaytient *|All Enrollments|Term Options"]));
                #endregion

                #region Client Profiles Field ID's
                // Getting Field ID's for Items in the Client Profiles App
                var clientProfilesFirstNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|First Name"]));
                var clientProfilesLastNameField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|Last Name"]));
                var clientProfilesStreetAddressField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|Street Address"]));
                var clientProfilesCityField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|City"]));
                var clientProfilesStateField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|State"]));
                var clientProfilesPostalCodeField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|State"]));
                var clientProfilesSSNField = currentItem.Field<TextItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|Social Security Number"]));
                var clientProfilesDOBField = currentItem.Field<DateItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|Date of Birth"]));
                var clientProfilesEmailField = currentItem.Field<EmailItemField>(fieldId = int.Parse(dictionary["* Repaytient *|Client Profiles|Email"]));
                #endregion

                // Creating Items

                #region Creating Item in Clients App

                Item newClient = new Item();

                clientsFirstNameField.Value = enrollmentsFirstNameField.Value;
                clientsLastNameField.Value = enrollmentsLastNameField.Value;
                clientsStreetAddressField.Value = enrollmentsStreetAddressField.Value;
                clientsCityField.Value = enrollmentsCityField.Value;
                clientsStateField.Value = enrollmentsStateField.Value;
                clientsPostalCodeField.Value = enrollmentsPostalCodeField.Value;
                clientsSSNField.Value = enrollmentsSSNField.Value;
                clientsDOBField.Values = enrollmentsDOBField.Values;
                clientsEmailField.Value = enrollmentsEmailField.Value;

                await podio.CreateItem(newClient, clientsAppId, false);
                Console.WriteLine("Item Created in the Clients App");
                #endregion

                #region Creating Item in Client Profiles App

                Item newClientProfile = new Item();

                clientProfilesFirstNameField.Value = enrollmentsFirstNameField.Value;
                clientProfilesLastNameField.Value = enrollmentsLastNameField.Value;
                clientProfilesStreetAddressField.Value = enrollmentsStreetAddressField.Value;
                clientProfilesCityField.Value = enrollmentsCityField.Value;
                clientProfilesStateField.Value = enrollmentsStateField.Value;
                clientProfilesPostalCodeField.Value = enrollmentsPostalCodeField.Value;
                clientProfilesSSNField.Value = enrollmentsSSNField.Value;
                clientProfilesDOBField.Values = enrollmentsDOBField.Values;
                clientProfilesEmailField.Value = enrollmentsEmailField.Value;

                await podio.CreateItem(newClientProfile, clientProfilesAppId, false);
                Console.WriteLine("Item Created in the Client Profiles App");
                #endregion

                #region Creating Item in All Enrollments App

                Item newAllEnrollment = new Item();

                allEnrollmentsFirstNameField.Value = enrollmentsFirstNameField.Value;
                allEnrollmentsLastNameField.Value = enrollmentsLastNameField.Value;
                allEnrollmentsStreetAddressField.Value = enrollmentsStreetAddressField.Value;
                allEnrollmentsCityField.Value = enrollmentsCityField.Value;
                allEnrollmentsStateField.Value = enrollmentsStateField.Value;
                allEnrollmentsPostalCodeField.Value = enrollmentsPostalCodeField.Value;
                allEnrollmentsSSNField.Value = enrollmentsSSNField.Value;
                allEnrollmentsDOBField.Values = enrollmentsDOBField.Values;
                allEnrollmentsEmailField.Value = enrollmentsEmailField.Value;
                // Needs Revision
                //allEnrollmentsHospitalOrFacilityField.Value = enrollmentsHospitalOrFacilityField.Value;
                allEnrollmentsPaymentPlanAmountField.Value = enrollmentsPaymentPlanAmountField.Value;
                allEnrollmentsTypeOfInsuranceField.OptionText = enrollmentsTypeOfInsuranceField.Options.First().Text;
                allEnrollmentsCurrentEmployerField.Value = enrollmentsCurrentEmployerField.Value;
                allEnrollmentsPayFrequencyField.OptionText = enrollmentsPayFrequencyField.Options.First().Text;
                allEnrollmentsDateOfNextPayDateField.Values = enrollmentsDateOfNextPayDateField.Values;
                allEnrollmentsGrossMonthlyIncomeField.Value = enrollmentsGrossMonthlyIncomeField.Value;
                allEnrollmentsRepaymentDateField.Values = enrollmentsRepaymentDateField.Values;
                allEnrollmentsPaymentAmountField.Value = enrollmentsPaymentAmountField.Value;
                allEnrollmentsPaymentPlanStatusField.OptionText = "Pending";
                allEnrollmentsTermOptionsField.OptionText = enrollmentsTermOptionsField.Options.First().Text;

                await podio.CreateItem(newAllEnrollment, allEnrollmentsAppId, false);
                Console.WriteLine("Item Created in the All Enrollments App");
                #endregion
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
