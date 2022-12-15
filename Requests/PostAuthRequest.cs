using Newtonsoft.Json;
using RestSharp;

namespace Cappario
{
    public class PostAuthRequest
    {
        public string EndpointUrl { get; } = Config.ApiBaseUrl + "/auth/backend";
        public string Token { get; private set; }

        public void GetToken()
        {
            RestClient Client = new RestClient(EndpointUrl);
            RestRequest Request = new RestRequest("request/oauth") { Method = Method.POST };
            Request.AddHeader("Accept", "application/json");
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Request.AddParameter("client_id", Config.ClientId);
            Request.AddParameter("client_secret", Config.ClientSecret);
            Request.AddParameter("username", Config.BackendUsername);
            Request.AddParameter("password", Config.BackendPassword);
            Request.AddParameter("grant_type", "client_credentials");
            IRestResponse Response = Client.Execute(Request);
            Token = JsonConvert.DeserializeObject<dynamic>(Response.Content)["access_token"];
        }
    }
}