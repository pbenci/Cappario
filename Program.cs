using OpenQA.Selenium;
using System.Threading.Tasks;

namespace Cappario
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConfigurationsChecker.Check();
            var PostAuthRequest = new PostAuthRequest();
            PostAuthRequest.GetToken();
            var GetIntegrationContractsRequest = new GetIntegrationContractsRequest(PostAuthRequest.Token);
            GetIntegrationContractsRequest.GetContractsToCheck();
            var GetContractsRequest = new GetContractsRequest(PostAuthRequest.Token, GetIntegrationContractsRequest.ListOfIdOfContractsToCheck);
            GetContractsRequest.GetCodeOfContractsThatNeedBranchChange();
            Parallel.ForEach(GetContractsRequest.ListOfCodeOfContractsThatNeedBranchChange, new ParallelOptions { MaxDegreeOfParallelism = 4 }, Contract =>
            {
                IWebDriver Driver = new Browsers().LaunchChrome();
                Driver.Manage().Window.Maximize();
                var LoginPage = new LoginPage(Driver);
                LoginPage.GoToUrl();
                LoginPage.Login();
                var RentalsPage = new RentalsPage(Driver);
                RentalsPage.GoTo();
                RentalsPage.SearchContractByCode(Contract.Code);
                var RentalsDetailsPage = new RentalsDetailsPage(Driver);
                RentalsDetailsPage.GoTo();
                RentalsDetailsPage.EditFiscalBranch(Contract.Code, Contract.RightFiscalBranch);
                Driver.Quit();
            });
        }
    }
}