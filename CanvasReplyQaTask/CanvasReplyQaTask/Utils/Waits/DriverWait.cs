using CanvasReplyQaTask.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CanvasReplyQaTask.Utils.Waits
{
    public static class DriverWait
    {
        private static WebDriverWait _wait => new WebDriverWait(Webdriver.Driver, TimeSpan.FromSeconds(20));

        public static bool WaitUntilElementIdDisplayed(By locator)
        {
            try
            {
                return _wait.Until(d => d.FindElement(locator).Displayed);
            }
            catch
            {
                return false;
            }
        }

        public static void WaitForElementTextToEqual(By locator, string text) =>
            _wait.Until(d => d.FindElement(locator).Text.Equals(text));

    }
}