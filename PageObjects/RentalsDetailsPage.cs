using OpenQA.Selenium;

namespace Cappario
{
    public class RentalsDetailsPage : BackendMenu
    {
        private WebElements ContractRow => new(Driver, By.CssSelector($"tbody > tr:nth-of-type(1)"));
        private WebElements GeneralInfoTab => new(Driver, By.CssSelector($"[data-view='info_view'] > .asset_menu"));
        private WebElements BranchName => new(Driver, By.CssSelector($".accordion div:nth-of-type(4) > .m-t-xs"));
        private WebElements JobsiteAddress => new(Driver, By.CssSelector($".pt-2 > .col-8 > span"));
        private WebElements EditGeneralInfoButton => new(Driver, By.CssSelector($".crud-edit > .fa"));
        private WebElements ContractCode => new(Driver, By.CssSelector($".font-size-16"));
        private WebElements FiscalBranchDropdown => new(Driver, By.Id("contract_branch_id_chosen"));
        private WebElements EditGeneralInfoSaveButton => new(Driver, By.CssSelector(".crud-save"));
        private WebElements EditGeneralInfoConfirmModalSaveButton => new(Driver, By.CssSelector("[data-action='confirmAndSubmitBehind']"));
        private WebElements ErrorToast => new(Driver, By.CssSelector("[role='alert']"));

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
            Interaction.Click(ContractCode.Element);
            Interaction.Click(GeneralInfoTab.Element);
            WaitForOverlayToDisappear();
        }

        public void EditFiscalBranch(string RightFiscalBranch, string Status)
        {
            Interaction.Click(EditGeneralInfoButton.Element);
            Interaction.Click(FiscalBranchDropdown.Element);
            WebElements FiscalBranch = new(Driver, By.XPath($"//*[@id='contract_branch_id_chosen']//*[text()='{RightFiscalBranch}']"));
            Interaction.Click(FiscalBranch.Element);
            Interaction.Click(EditGeneralInfoSaveButton.Element);
            try
            {
                Wait.ForElementToExist(ErrorToast);
            }
            catch (NoSuchElementException)
            {
                Interaction.Click(EditGeneralInfoConfirmModalSaveButton.Element);
                WaitForOverlayToDisappear();
            }
        }
    }
}