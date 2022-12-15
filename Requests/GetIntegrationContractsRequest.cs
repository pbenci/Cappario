using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Cappario
{
    public class GetIntegrationContractsRequest
    {
        public string Token { get; private set; }
        public string EndpointUrl { get; } = Config.ApiBaseUrl + "/integration/contracts";
        public string FromCreationDate => DateTime.Now.AddDays(-3).ToString(("yyyy-MM-dd 00:00:00"));
        public dynamic DeserializedJson { get; private set; }
        public List<string> ContractsToCheckList { get; private set; } = new List<string>();

        public GetIntegrationContractsRequest(string Token)
        {
            this.Token = Token;
        }

        public void SendRequest()
        {
            RestClient Client = new RestClient(EndpointUrl);
            RestRequest Request = new RestRequest(Method.GET);
            Request.AddParameter("Authorization", "Bearer " + Token, ParameterType.HttpHeader);
            Request.AddParameter("creation_date_from", FromCreationDate);
            Request.AddParameter("status", "approved,running");
            DeserializedJson = JsonConvert.DeserializeObject<dynamic>(Client.Execute(Request).Content);
        }

        public List<string> GetContractsToCheck()
        {
            SendRequest();
            var contracts = DeserializedJson["contracts"].Count;
            for (int e = 0; e < contracts; e++)
            {
                ContractsToCheckList.Add(Convert.ToString(DeserializedJson["contracts"][e]["code"]));
                Console.WriteLine("Added " + DeserializedJson["contracts"][e]["code"] + " to the list");

            }
            return ContractsToCheckList;
        }
    }
}