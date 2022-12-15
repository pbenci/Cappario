using OpenQA.Selenium;

namespace Cappario
{
    public class RentalsPage : BackendMenu
    {
        private WebElements KeywordSearchField => new(Driver, By.Id($"advSearch_tab_3_key"));
        private WebElements ApplyFiltersButton => new(Driver, By.CssSelector($"#crud_filter_form_tab_3 > div.crud_search_action.p-b-sm.hidden-xs > div > div > div.col-lg-2.hidden-xs > div > button"));

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

        public void SearchContractByCode(string ContractCode)
        {
            Interaction.Write(KeywordSearchField.Element, ContractCode);
            Interaction.Click(ApplyFiltersButton.Element);
        }
    }
}