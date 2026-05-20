using CanvasReplyQaTask.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CanvasReplyQaTask.Utils.Selenium
{
    public static class ActionsUtils
    {
        private static IWebDriver driver => Webdriver.Driver;
        public static void MoveToElementAndClick(IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Click().Perform();
        }
        public static void MoveToElement(IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Perform();
        }
    }
}
