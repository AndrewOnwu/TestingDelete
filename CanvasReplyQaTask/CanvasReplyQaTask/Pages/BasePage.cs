using CanvasReplyQaTask.Utils.Selenium;
using CanvasReplyQaTask.Utils.Waits;
using CanvasReplyQaTask.WebDriver;
using OpenQA.Selenium;

namespace CanvasReplyQaTask.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
       // protected DriverWait Wait;
        private static readonly By AjaxLocator = By.Id("ajaxStatusDiv");

        public BasePage()
        {
            Driver = Webdriver.Driver;
           // Wait = new DriverWait();
        }





        public void ClickWithoutAjaxAwait(By locator, string elementText)
        {
            Click(SeleniumUtils.GetWebElementWithText(locator, elementText), false);
        }

        public void ClickButton(string text)
        {
            Click(SeleniumUtils.GetWebElementWithText(By.TagName("button"), text));
            WaitForAjax();
        }
        public void ClickLinkName(string linkName)
        {
            Click(SeleniumUtils.GetWebElement(By.LinkText(linkName)));
        }

        public void WaitForAjax()
        {
            DriverWait.WaitForElementTextToEqual(AjaxLocator, string.Empty);
        }

        public void Click(IWebElement webElement, bool waitForAjax = true)
        {
            SeleniumUtils.Click(webElement);
            
            if (waitForAjax)
                WaitForAjax();
        }

        public void AcceptAlert()
        {
            SeleniumUtils.AcceptAlert();
            WaitForAjax();
        }
    }
}