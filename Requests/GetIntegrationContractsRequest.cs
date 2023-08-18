using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Cappario
{
    public class GetIntegrationContractsRequest
    {
        public string Token { get; private set; }
        public string EndpointUrl { get; } = ConfigurationManager.AppSettings.Get("ApiBaseUrl") + "/v5/integration/contracts";
        public string FromCreationDate => DateTime.Now.AddDays(double.Parse(ConfigurationManager.AppSettings.Get("NumberOfDaysInThePast"))).ToString(("yyyy-MM-dd 00:00:00"));
        public dynamic DeserializedJson { get; private set; }
        public List<string> ListOfIdOfContractsToCheck { get; private set; } = new List<string>();
        public List<Contract> ListOfCodeOfContractsThatNeedBranchChange { get; private set; } = new List<Contract>();

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

        public List<Contract> GetContractsToCheck()
        {
            Console.WriteLine("Checking contracts, please wait...");
            Regex Rx = new Regex(@"\b(\d{5})\b");
            var Offset = 0;
            SendRequest(Offset);
            if (DeserializedJson["next"] == null)
            {
                var Contracts = DeserializedJson["contracts"].Count;
                for (int e = 0; e < Contracts; e++)
                {
                    if (DeserializedJson["contracts"][e]["branch"]["code"] != "801" && Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value) != DeserializedJson["contracts"][e]["branch"]["code"].ToString())
                    {
                        Contract Contract = new(
                            DeserializedJson["contracts"][e]["code"].ToString(),
                            Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value),
                            DeserializedJson["contracts"][e]["customer"]["code"].ToString(),
                            DeserializedJson["contracts"][e]["customer"]["name"].ToString(),
                            Excel.GetRightBranchFromCode(DeserializedJson["contracts"][e]["branch"]["id"].ToString())
                        );
                        ListOfCodeOfContractsThatNeedBranchChange.Add(Contract);
                    }
                    Console.WriteLine("Checked contract" + " " + DeserializedJson["contracts"][e]["code"]);
                }
            }
            else
            {
                var Contracts = DeserializedJson["contracts"].Count;
                for (int e = 0; e < Contracts; e++)
                {
                    if (DeserializedJson["contracts"][e]["branch"]["code"] != "801" && Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value) != Excel.GetRightBranchFromCode(DeserializedJson["contracts"][e]["branch"]["id"].ToString()))
                    {
                        Contract Contract = new(
                            DeserializedJson["contracts"][e]["code"].ToString(),
                            Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value),
                            DeserializedJson["contracts"][e]["customer"]["code"].ToString(),
                            DeserializedJson["contracts"][e]["customer"]["name"].ToString(),
                            Excel.GetRightBranchFromCode(DeserializedJson["contracts"][e]["branch"]["id"].ToString())
                        );
                        ListOfCodeOfContractsThatNeedBranchChange.Add(Contract);
                    }
                    Console.WriteLine("Checked contract" + " " + DeserializedJson["contracts"][e]["code"]);
                }
                while (DeserializedJson["next"] != null)
                {
                    Offset += 100;
                    SendRequest(Offset);
                    var ContractsInPage = DeserializedJson["contracts"].Count;
                    for (int e = 0; e < ContractsInPage; e++)
                    {
                        if (DeserializedJson["contracts"][e]["branch"]["code"] != "801" && Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value) != Excel.GetRightBranchFromCode(DeserializedJson["contracts"][e]["branch"]["id"].ToString()))
                        {
                            Contract Contract = new(
                                DeserializedJson["contracts"][e]["code"].ToString(),
                                Excel.GetRightBranchFromZipCode(Rx.Match(DeserializedJson["contracts"][e]["jobsite"]["address"].ToString()).Value),
                                DeserializedJson["contracts"][e]["customer"]["code"].ToString(),
                                DeserializedJson["contracts"][e]["customer"]["name"].ToString(),
                                Excel.GetRightBranchFromCode(DeserializedJson["contracts"][e]["branch"]["id"].ToString())
                            );
                            ListOfCodeOfContractsThatNeedBranchChange.Add(Contract);
                        }
                        Console.WriteLine("Checked contract" + " " + DeserializedJson["contracts"][e]["code"]);
                    }
                }
            }
            return ListOfCodeOfContractsThatNeedBranchChange;
        }
    }
}