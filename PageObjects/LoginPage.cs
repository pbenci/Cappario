using OpenQA.Selenium;
using System.Configuration;

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
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BackendUrl"));
        }

        public void Login()
        {
            Interaction.Write(LoginEmailField.Element, ConfigurationManager.AppSettings.Get("BackendUsername"));
            Interaction.Write(LoginPasswordField.Element, ConfigurationManager.AppSettings.Get("BackendPassword"));
            Interaction.Click(LoginButton.Element);
        }
    }
}