﻿using OpenQA.Selenium;
using System.Diagnostics.Metrics;
using System;
using System.Threading.Tasks;
using System.Configuration;

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
            if (bool.Parse(ConfigurationManager.AppSettings.Get("ModifyContract")))
            {
                Parallel.ForEach(GetIntegrationContractsRequest.ListOfCodeOfContractsThatNeedBranchChange, new ParallelOptions { MaxDegreeOfParallelism = 4 }, Contract =>
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
            else
            {
                foreach (Contract Contract in GetIntegrationContractsRequest.ListOfCodeOfContractsThatNeedBranchChange)
                {
                    if (Contract.RightFiscalBranch != null)
                    {
                        Results.Log($"The contract {Contract.Code} for customer {Contract.CustomerName} should be switched from {Contract.CurrentBranch} to {Contract.RightFiscalBranch}");
                    }
                }
            }
        }
    }
}