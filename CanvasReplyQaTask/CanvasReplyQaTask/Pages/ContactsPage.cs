using CanvasReplyQaTask.Models;
using CanvasReplyQaTask.Utils.Selenium;
using CanvasReplyQaTask.Utils.Waits;
using OpenQA.Selenium;

namespace CanvasReplyQaTask.Pages
{
    public class ContactsPage : CommonFragments
    {
        private static readonly By FirstName = By.Id("DetailFormfirst_name-input");
        private static readonly By LastName = By.Id("DetailFormlast_name-input");
        private static readonly By BusinessRole = By.Id("DetailFormbusiness_role-input");
        private static readonly By Category = By.CssSelector("#DetailFormcategories-input");
        private static readonly By CategoryInput = By.XPath("//*[@id='DetailFormcategories-input-search-text']//input");
        private static readonly By PopUpMenuOption = By.CssSelector("div[class^='menu-option']");
        private static readonly By SaveButton = By.Id("DetailForm_save2-label");
        private static readonly By EditFormButton = By.Id("DetailForm_edit-label");

        private void EnterFirstName(string firstname)
            => SeleniumUtils.SendKeys(firstname, FirstName);

        private void EnterLastName(string lastname)
           => SeleniumUtils.SendKeys(lastname, LastName);

        private void SelectBusinessRole(string role)
        {
            SeleniumUtils.Click(BusinessRole);
            SeleniumUtils.GetWebElementWithText(PopUpMenuOption, role).Click();
        }

        public void SelectCategory(string categories)
        {
            foreach (var category in categories.Split(","))
            {
                var element = SeleniumUtils.GetWebElement(Category);
                ActionsUtils.MoveToElementAndClick(element);
                WaitForAjax();
               // Thread.Sleep(5000);
                SeleniumUtils.SendKeys(category, CategoryInput, false);
                SeleniumUtils.GetWebElementWithText(PopUpMenuOption, category).Click();
            }
        }

        public void EnterContactDetails(ContactModel model)
        {
            EnterFirstName(model.Firstname);
            EnterLastName(model.Lastname);
            SelectBusinessRole(model.Role);
            SelectCategory(model.Categories);
        }


        public void ClickSaveButton()
            => SeleniumUtils.Click(SaveButton);

        public ContactModel GetContactDetails()
        {
            Click(SeleniumUtils.GetWebElement(EditFormButton));
            DriverWait.WaitUntilElementIdDisplayed(SaveButton);
            return new ContactModel()
            {
                Firstname = SeleniumUtils.GetElementAttributeText(FirstName,"Value"),
                Lastname = SeleniumUtils.GetElementAttributeText(LastName,"Value"),
                Role = SeleniumUtils.GetElementText(BusinessRole),
                Categories = SeleniumUtils.GetElementText(Category)
            };
        }
    }
}
