using CanvasReplyQaTask.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CanvasReplyQaTask.Utils.Selenium
{
    public static class SeleniumUtils
    {
        private static IWebDriver driver => Webdriver.Driver;
        public static IWebElement GetWebElementWithText(By locator, string text)
        {
            return driver.FindElements(locator).FirstOrDefault(
                       ele => ele.Text.Trim().Equals(text))
                   ?? throw new Exception($"Unable to find element where text is : {text}");
        }

        public static void SendKeys(string text, By locator, bool clearText = true)
        {
            var element = driver.FindElement(locator);
            //ActionsUtils.MoveToElement(element);
            Click(element);
            if (clearText)
                element.Clear();
            element.SendKeys(text);
        }

        public static void SelectDropDownListByName(string name, By locator)
        {
            if (string.IsNullOrEmpty(name))
                return;

            var element = new SelectElement(driver.FindElement(locator));
            element.SelectByText(name);
        }

        public static void Check(this IWebElement webElement)
        {
            if (!webElement.Selected)
                webElement.Click();
        }

        public static string GetElementAttributeText(By locator, string attribute)
            => driver.FindElement(locator).GetAttribute(attribute);

        public static string GetElementText(By locator)
            => driver.FindElement(locator).Text;

        public static IWebElement GetWebElement(By locator)
        {
            return driver.FindElement(locator);
        }


        public static IList<IWebElement> GetWebElements(By locator)
        {
            return driver.FindElements(locator);
        }

        public static void Click(By locator)
          => GetWebElement(locator).Click();

        public static void Click(IWebElement element)
         => element.Click();

        public static void AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }


        public static void Click(By locator, string elementText)
        => Click(GetWebElementWithText(locator, elementText));

  
    }
}
