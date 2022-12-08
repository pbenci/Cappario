using OpenQA.Selenium;
using System;
using System.Threading;

namespace Cappario
{
    public class Program
    {
        static void Main(string[] args)
        {
            IWebDriver Driver = new Browsers().LaunchChrome();
            Driver.Manage().Window.Maximize();
            var LoginPage = new LoginPage(Driver);
            LoginPage.GoToUrl();
            LoginPage.Login();
            var RentalsPage = new RentalsPage(Driver);
            RentalsPage.GoTo();
            RentalsPage.SetStartDateFrom();
            Thread.Sleep(5000);
            Driver.Quit();
        }
    }
}