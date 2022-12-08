using OpenQA.Selenium;
using System;

namespace Cappario
{
    public class RentalsPage : BackendMenu
    {
        private WebElements StartDateFromField => new(Driver, By.Id($"advSearch_tab_3_date_start_dal"));
        private string StartFromDate => DateTime.Now.AddDays(-3).ToShortDateString();

        public RentalsPage(IWebDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
            Wait = new(Driver);
        }

        public void GoTo()
        {
            WaitForOverlayToDisappear();
            Interaction.Click(RentalMainMenuButton.Element);
            Interaction.Click(RentalMenuButton.Element);
            WaitForOverlayToDisappear();
        }

        public void SetStartDateFrom()
        {
            Interaction.Click(StartDateFromField.Element);
            ((IJavaScriptExecutor)Driver).ExecuteScript($"document.getElementsByName('advSearch_tab_3[date_start_dal]').item(0).value = '{StartFromDate}';");
        }
    }
}