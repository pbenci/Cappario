﻿using OpenQA.Selenium;
using System;

namespace Cappario
{
    public class LoginPage : BackendMenu
    {
        private WebElements LoginEmailField => new(Driver, By.Id("signin_username"));
        private WebElements LoginPasswordField => new(Driver, By.Id("signin_password"));
        private WebElements LoginButton => new(Driver, By.ClassName("btn-primary"));
        private WebElements Toast => new(Driver, By.ClassName("toast-message"));

        public LoginPage(IWebDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
            Interaction = new Interactions(Driver);
        }

        public void GoToUrl()
        {
            Driver.Navigate().GoToUrl(Config.BackendUrl);
        }

        public void Login()
        {
            Interaction.Write(LoginEmailField.Element, Config.BackendUsername);
            Interaction.Write(LoginPasswordField.Element, Config.BackendPassword);
            Interaction.Click(LoginButton.Element);
        }
    }
}