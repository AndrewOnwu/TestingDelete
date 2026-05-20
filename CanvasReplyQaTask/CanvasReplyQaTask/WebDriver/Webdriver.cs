using CanvasReplyQaTask.Hooks;
using CanvasReplyQaTask.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CanvasReplyQaTask.WebDriver
{
    public class Webdriver
    {
        private static readonly ThreadLocal<IWebDriver> driver = new();

        public static IWebDriver Driver { get
            {
                return driver.Value ?? throw new Exception("Driver is not set");
            } 
        }

        public void SetDriver()
        {
            driver.Value = GetWebDriver(Hook.Config.Browser.ToLower());
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        public void LoginViaApi(LoginResponseModel model)
        {
            Driver.Navigate().GoToUrl(Hook.Config.Url);
            List<Cookie> cookies = new()
            {
                new (model.Session_Name, model.Json_Session_Id),
                new ("ck_login_forget", "1")
            };
            foreach (Cookie cookie in cookies)
            {
                Driver.Manage().Cookies.AddCookie(cookie);
            }
           
        }

        private IWebDriver GetWebDriver(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    return new ChromeDriver();
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
                default:
                    throw new Exception($"The browser {browser} is not currently implemented");
            }
        }
    }
}
