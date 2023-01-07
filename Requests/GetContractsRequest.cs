using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Cappario
{
    public class GetContractsRequest
    {
        public string Token { get; private set; }
        public string EndpointUrl { get; } = ConfigurationManager.AppSettings.Get("ApiBaseUrl") + "/v4/contract";
        public dynamic DeserializedJson { get; private set; }
        public List<string> ListOfIdOfContractsToCheck { get; private set; }
        public List<Contract> ListOfCodeOfContractsThatNeedBranchChange { get; private set; } = new List<Contract>();

        public GetContractsRequest(string Token, List<string> ListOfIdOfContractsToCheck)
        {
            this.Token = Token;
            this.ListOfIdOfContractsToCheck = ListOfIdOfContractsToCheck;
        }

        private void SendRequest(string ContractId)
        {
            RestClient Client = new RestClient(EndpointUrl);
            RestRequest Request = new RestRequest(Method.GET);
            Request.AddParameter("Authorization", "Bearer " + Token, ParameterType.HttpHeader);
            Request.AddParameter("lang", "en");
            Request.AddParameter("contract_id", ContractId);
            DeserializedJson = JsonConvert.DeserializeObject<dynamic>(Client.Execute(Request).Content);
        }

        public List<Contract> GetCodeOfContractsThatNeedBranchChange()
        {
            var Contracts = ListOfIdOfContractsToCheck.Count;
            for (int e = 0; e < Contracts; e++)
            {
                SendRequest(ListOfIdOfContractsToCheck[e]);
                Contract Contract = new(
                    DeserializedJson["content"]["contract"]["code"].ToString(),
                    Excel.GetRightBranchFromZipCode(DeserializedJson["content"]["contract"]["customer_job_site"]["address_detail"]["postal_code"].ToString()),
                    DeserializedJson["content"]["contract"]["status_code"].ToString(),
                    DeserializedJson["content"]["contract"]["customer"]["code"].ToString(),                    
                    (bool.Parse(DeserializedJson["content"]["contract"]["customer"]["is_company"].ToString()) ? $"{DeserializedJson["content"]["contract"]["customer"]["company_name"]}" : $"{DeserializedJson["content"]["contract"]["customer"]["first_name"]} {DeserializedJson["content"]["contract"]["customer"]["last_name"]}"),
                    DeserializedJson["content"]["contract"]["branch_name"].ToString()
                );
                if (Excel.GetRightBranchFromZipCode(DeserializedJson["content"]["contract"]["customer_job_site"]["address_detail"]["postal_code"].ToString()) != DeserializedJson["content"]["contract"]["branch_name"].ToString())
                {
                    if (Contract.RightFiscalBranch != null)
                    {
                        ListOfCodeOfContractsThatNeedBranchChange.Add(Contract);
                    }
                }
                Excel.ExcelApp.Quit();
                Console.WriteLine("Checked contract " + Contract.Code);
            }
            return ListOfCodeOfContractsThatNeedBranchChange;
        }
    }
}