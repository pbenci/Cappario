using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Cappario
{
    public class GetIntegrationContractsRequest
    {
        public string Token { get; private set; }
        public string EndpointUrl { get; } = ConfigurationManager.AppSettings.Get("ApiBaseUrl") + "/v5/integration/contracts";
        public string FromCreationDate => DateTime.Now.AddDays(double.Parse(ConfigurationManager.AppSettings.Get("NumberOfDaysInThePast"))).ToString(("yyyy-MM-dd 00:00:00"));
        public dynamic DeserializedJson { get; private set; }
        public List<string> ListOfIdOfContractsToCheck { get; private set; } = new List<string>();

        public GetIntegrationContractsRequest(string Token)
        {
            this.Token = Token;
        }

        private void SendRequest(int Offset)
        {
            RestClient Client = new RestClient(EndpointUrl);
            RestRequest Request = new RestRequest(Method.GET);
            Request.AddParameter("Authorization", "Bearer " + Token, ParameterType.HttpHeader);
            Request.AddParameter("creation_date_from", FromCreationDate);
            Request.AddParameter("status", "approved,running");
            Request.AddParameter("offset", Offset);
            DeserializedJson = JsonConvert.DeserializeObject<dynamic>(Client.Execute(Request).Content);
        }

        public List<string> GetContractsToCheck()
        {
            var Offset = 0;

            SendRequest(Offset);
            if (DeserializedJson["next"] == null)
            {
                var Contracts = DeserializedJson["contracts"].Count;
                for (int e = 0; e < Contracts; e++)
                {
                    ListOfIdOfContractsToCheck.Add(Convert.ToString(DeserializedJson["contracts"][e]["id"]));
                    Console.WriteLine("Added contract " + DeserializedJson["contracts"][e]["code"] + " to the list of contracts that must be checked");
                }
            }
            else
            {
                var Contracts = DeserializedJson["contracts"].Count;
                for (int e = 0; e < Contracts; e++)
                {
                    ListOfIdOfContractsToCheck.Add(Convert.ToString(DeserializedJson["contracts"][e]["id"]));
                    Console.WriteLine("Added contract " + DeserializedJson["contracts"][e]["code"] + " to the list of contracts that must be checked");
                }
                while (DeserializedJson["next"] != null)
                {
                    SendRequest(Offset);
                    Offset += 100;
                    var ContractsInPage = DeserializedJson["contracts"].Count;
                    for (int e = 0; e < ContractsInPage; e++)
                    {
                        ListOfIdOfContractsToCheck.Add(Convert.ToString(DeserializedJson["contracts"][e]["id"]));
                        Console.WriteLine("Added contract " + DeserializedJson["contracts"][e]["code"] + " to the list of contracts that must be checked");
                    }
                }
            }
            return ListOfIdOfContractsToCheck;
        }
    }
}