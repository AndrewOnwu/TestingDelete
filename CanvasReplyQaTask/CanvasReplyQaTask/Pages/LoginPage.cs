using CanvasReplyQaTask.Utils.Selenium;
using OpenQA.Selenium;

namespace CanvasReplyQaTask.Pages
{
    public class LoginPage : CommonFragments
    {
        private static readonly By UserNameField = By.Id("login_user");
        private static readonly By PasswordField = By.Id("login_pass");
        private static readonly By LanguageDropDown = By.Id("login_lang");
        private static readonly By ThemeDropDown = By.Id("login_theme");
        private static readonly By LoginButton = By.Id("login_button");

        private void EnterUserName(string username)
           => SeleniumUtils.SendKeys(username, UserNameField);

        private void EnterPassword(string password)
            => SeleniumUtils.SendKeys(password, PasswordField);

        private void SelectLanguage(string language)
            => SeleniumUtils.SelectDropDownListByName(language, LanguageDropDown);

        private void SelectTheme(string theme)
            => SeleniumUtils.SelectDropDownListByName(theme, ThemeDropDown);

        public void ClickLoginButton()
            => SeleniumUtils.Click(LoginButton);

        public void EnterLoginDetails(string username, string password, string language, string theme)
        {
            EnterUserName(username);
            EnterPassword(password);
            SelectLanguage(language);
            SelectTheme(theme);
        }
    }
}
