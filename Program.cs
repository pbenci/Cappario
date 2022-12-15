using OpenQA.Selenium;
using System.Threading;

namespace Cappario
{
    public class Program
    {
        static void Main(string[] args)
        {
            var PostAuthRequest = new PostAuthRequest();
            PostAuthRequest.GetToken();
            var GetIntegrationContractsRequest = new GetIntegrationContractsRequest(PostAuthRequest.Token);
            GetIntegrationContractsRequest.GetContractsToCheck();
            IWebDriver Driver = new Browsers().LaunchChrome();
            Driver.Manage().Window.Maximize();
            var LoginPage = new LoginPage(Driver);
            LoginPage.GoToUrl();
            LoginPage.Login();
            var RentalsPage = new RentalsPage(Driver);
            RentalsPage.GoTo();
            RentalsPage.SearchContractByCode(GetIntegrationContractsRequest.ContractsToCheckList[0]);
            var RentalsDetailsPage = new RentalsDetailsPage(Driver);
            RentalsDetailsPage.GoTo();
            RentalsDetailsPage.EditFiscalBranch();
            Excel.ReadFile();
            Thread.Sleep(5000);
            Driver.Quit();
        }
    }
}