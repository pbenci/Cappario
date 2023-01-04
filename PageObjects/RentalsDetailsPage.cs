using OpenQA.Selenium;
using System;

namespace Cappario
{
    public class RentalsDetailsPage : BackendMenu
    {
        private WebElements ContractRow => new(Driver, By.CssSelector($"tbody > tr:nth-of-type(1)"));
        private WebElements GeneralInfoTab => new(Driver, By.CssSelector($"[data-view='info_view'] > .asset_menu"));
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

        public void EditFiscalBranch(string ContractCode, string RightFiscalBranch)
        {
            Interaction.Click(EditGeneralInfoButton.Element);
            Interaction.Click(FiscalBranchDropdown.Element);
            WebElements FiscalBranch = new(Driver, By.XPath($"//*[@id='contract_branch_id_chosen']//*[text()='{RightFiscalBranch}']"));
            Interaction.Click(FiscalBranch.Element);
            Interaction.Click(EditGeneralInfoSaveButton.Element);
            WaitForOverlayToDisappear();
            try
            {
                Wait.ForElementToExist(ErrorToast);
                Console.WriteLine(ContractCode + " " + "fiscal branch couldn't be edited");
                Results.Log(ContractCode + " " + "fiscal branch couldn't be edited");
            }
            catch (NoSuchElementException)
            {
                Interaction.Click(EditGeneralInfoConfirmModalSaveButton.Element);
                Console.WriteLine(ContractCode + " " + "fiscal branch edited successfully");
                Results.Log(ContractCode + " " + "fiscal branch edited successfully");
            }
        }
    }
}