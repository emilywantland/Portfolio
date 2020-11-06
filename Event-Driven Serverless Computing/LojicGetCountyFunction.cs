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
using System.Net.Http;
using Newtonsoft.Json;

//Don't forget to configure the aws-lambda-tools-*.json
//Use dotnet lambda deploy-function -cfg aws-lambda-tools-{NameOfThisFunction}.json 
//change namespace to {functionName}Container, for clarity

namespace LouForwardLojicGetCountyContainer
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

        public class Attributes
        {
            public string COUNDIST { get; set; }
        }
        public class Feature
        {
            public Attributes attributes { get; set; }
        }
        public class DistrictInfo
        {
            public List<Feature> features { get; set; }
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
            var appId = await saasafrasDictionary.GetKeyAsInt("1. Client Management|Business Profile");
            if (appId != currentItem.App.AppId)
                solution.log($"");

            //update the function name
            var functionName = "LouForwardLojicGetCountyFunction";
            var uniqueId = currentItem.ItemId.ToString();
            var lockId = await functionLocker.LockFunction(functionName, uniqueId);

            if (string.IsNullOrEmpty(lockId))
            {
                throw new Exception($"Failed to acquire lock for {functionName} and id {uniqueId}");
            }

            try
            {
                Console.WriteLine("Entered Try Block");
                const int LOCATION_FIELD_ID = 126466902; // Business Address Field

                int? revisionId = 0;
                if (command.resource.type == "item.update")
                {
                    var revision = await podio.GetRevisionDifference(Convert.ToInt32(currentItem.ItemId), currentItem.CurrentRevision.Revision - 1, currentItem.CurrentRevision.Revision);
                    revisionId = revision.First().FieldId;
                }

                // Main Process //
                var locationField = currentItem.Field<LocationItemField>(LOCATION_FIELD_ID);

                if (command.resource.type == "item.create" || revisionId == locationField.FieldId)
                {
                    Console.WriteLine("Entered If Statement");

                    const string URL = "https://ags1.lojic.org/ArcGIS/rest/services/LOJIC/AGOLJeflib/MapServer/17/query?geometryType=esriGeometryPoint&f=pjson" +
                   "&returnGeometry=false&spatialRel=esriSpatialRelWithin&inSR=4326&outSR=4326&outFields=COUNDIST&geometry=";

                    // const int SPATIAL_REF = 4326; // Map Reference - Not in Use
                    const int DISTRICT_FIELD_ID = 128975811; // Council District Field

                    var http = new HttpClient { BaseAddress = new Uri(URL) };
                    var url = URL + locationField.Longitude.GetValueOrDefault() + "," + locationField.Latitude.GetValueOrDefault();
                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                    Console.WriteLine(url);
                    var response = await http.PostAsync(request.RequestUri, request.Content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                    DistrictInfo districtInfo = JsonConvert.DeserializeObject<DistrictInfo>(responseString);

                    //Console.WriteLine(result.Features.Attributes.CountDist);
                    var districtNum = districtInfo.features.First().attributes.COUNDIST;

                    //When a new item in Admin is created:
                    var updateMe = new Item() { ItemId = currentItem.ItemId };

                    Console.WriteLine("Updating Item...");

                    //Field to update:
                    var searchKey = updateMe.Field<TextItemField>(DISTRICT_FIELD_ID);
                    searchKey.Value = districtNum;
                    await podio.UpdateItem(updateMe, true);
                }
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
