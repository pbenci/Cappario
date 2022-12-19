using OpenQA.Selenium;

namespace Cappario
{
    public class RentalsPage : BackendMenu
    {
        private WebElements KeywordSearchField => new(Driver, By.CssSelector($".crud_search_primary .col-xl-2 .input-group > [type='text']"));
        private WebElements ApplyFiltersButton => new(Driver, By.CssSelector($".hidden-xs.col-lg-2 .btn"));

        public RentalsPage(IWebDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
            Wait = new(Driver);
        }

        public void GoTo()
        {
            WaitForOverlayToDisappear();
            if (RentalMenuButton.Element.Displayed)
            {
                Interaction.Click(RentalMenuButton.Element);
            }
            else
            {
                Interaction.Click(RentalMainMenuButton.Element);
                Interaction.Click(RentalMenuButton.Element);
            }
            WaitForOverlayToDisappear();
        }

        public void SearchContractByCode(string ContractCode)
        {
            Interaction.Write(KeywordSearchField.Element, ContractCode);
            Interaction.Click(ApplyFiltersButton.Element);
        }
    }
}