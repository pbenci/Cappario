using OpenQA.Selenium;
using System;

namespace Cappario
{
    public class RentalsDetailsPage : BackendMenu
    {
        private WebElements ContractRow => new(Driver, By.CssSelector($"#table-tab_3 > tbody > tr"));
        private WebElements GeneralInfoTab => new(Driver, By.CssSelector($"#detail-submenu-tab_4 > div > ul > li:nth-child(7) > a"));
        private WebElements BranchName => new(Driver, By.CssSelector($"#accordion_tab_4_2 > div > div > div:nth-child(4) > div.m-t-xs.m-b-sm"));
        private WebElements JobsiteAddress => new(Driver, By.CssSelector($"#accordion_tab_4_4 > div > div > div > div.m-t-xs.m-b-sm > div > div.row.pt-2 > div.col-8 > span"));
        private WebElements EditGeneralInfoButton => new(Driver, By.CssSelector($"#detail-content-data-tab_4 > div:nth-child(2) > div > div > div > div > div.row.mx-0.sticky-edit > div > div > div:nth-child(2) > a"));

        public RentalsDetailsPage(IWebDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
            Wait = new(Driver);
        }

        public void GoTo()
        {
            WaitForOverlayToDisappear();
            Interaction.Click(ContractRow.Element);
            WaitForOverlayToDisappear();
            Interaction.Click(GeneralInfoTab.Element);
            WaitForOverlayToDisappear();
            Console.WriteLine(BranchName.Element.Text);
            Console.WriteLine(JobsiteAddress.Element.Text);
        }

        public void EditFiscalBranch()
        {
            Interaction.Click(EditGeneralInfoButton.Element);
        }
    }
}