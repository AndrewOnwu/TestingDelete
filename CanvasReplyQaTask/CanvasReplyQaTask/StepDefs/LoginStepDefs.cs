using CanvasReplyQaTask.ApiOperations;
using CanvasReplyQaTask.Hooks;
using CanvasReplyQaTask.Models;
using CanvasReplyQaTask.Pages;
using CanvasReplyQaTask.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CanvasReplyQaTask.StepDefs
{
    [Binding]
    public class LoginStepDefs
    {
        private readonly LoginPage _loginPage;
        private readonly LoginApiOperations _loginApiOperations;
        private readonly IWebDriver _driver;
        private readonly CommonFragments _commonFragments;
        private readonly ScenarioContext _scenarioContext;

        public LoginStepDefs(CommonFragments commonFragments, ScenarioContext scenarioContext)
        {
            _loginPage = new LoginPage();
            _loginApiOperations = new LoginApiOperations();
            _driver = Webdriver.Driver;
            _commonFragments = commonFragments;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the loginpage")]
        public void GivenIAmOnTheLoginPage()
            => _driver.Navigate().GoToUrl(Hook.Config.Url);

        [Given(@"I have successfully logged in")]
        public void GivenIHaveLoggedInViaApi()
        {
            _driver.Navigate().GoToUrl(Hook.Config.Url);
            if (_scenarioContext.ScenarioInfo.Tags.Any(tag => tag.Equals("apiLogin")))
            {
                var model = _loginApiOperations.GetLoginDetails(new RestRequest("json.php"));
                List<Cookie> cookies = new()
                {
                new (model.Session_Name, model.Json_Session_Id),
                new ("ck_login_forget", "1")
                };
                foreach (Cookie cookie in cookies)
                {
                    _driver.Manage().Cookies.AddCookie(cookie);
                }
                _driver.Navigate().GoToUrl(Hook.Config.ApiUrl);
            }
            else
            {
                var model = Hook.Config.LoginModel;
                _loginPage.EnterLoginDetails(model.Username, model.Password, "", "");
                _loginPage.ClickLoginButton();
            }
            Assert.That(_commonFragments.UserIsLoggedIn());
        }


        [When(@"I enter the following login details")]
        public void WhenIEnterTheFollowingLoginDetails(Table table)
        {
            var model = table.CreateInstance<LoginModel>();
            _loginPage.EnterLoginDetails(model.Username, model.Password, model.Language, model.Theme);
        }

        [When(@"I click Login button")]
        public void WhenIClickLoginButton()
            => _loginPage.ClickLoginButton();

    }
}
