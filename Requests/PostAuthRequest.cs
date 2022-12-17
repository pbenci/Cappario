using Newtonsoft.Json;
using RestSharp;
using System.Configuration;

namespace Cappario
{
    public class PostAuthRequest
    {
        public string EndpointUrl { get; } = ConfigurationManager.AppSettings.Get("ApiBaseUrl") + "/auth/backend";
        public string Token { get; private set; }

        public void GetToken()
        {
            RestClient Client = new RestClient(EndpointUrl);
            RestRequest Request = new RestRequest("request/oauth") { Method = Method.POST };
            Request.AddHeader("Accept", "application/json");
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Request.AddParameter("client_id", ConfigurationManager.AppSettings.Get("ClientId"));
            Request.AddParameter("client_secret", ConfigurationManager.AppSettings.Get("ClientSecret"));
            Request.AddParameter("username", ConfigurationManager.AppSettings.Get("BackendUsername"));
            Request.AddParameter("password", ConfigurationManager.AppSettings.Get("BackendPassword"));
            Request.AddParameter("grant_type", "client_credentials");
            IRestResponse Response = Client.Execute(Request);
            Token = JsonConvert.DeserializeObject<dynamic>(Response.Content)["access_token"];
        }
    }
}