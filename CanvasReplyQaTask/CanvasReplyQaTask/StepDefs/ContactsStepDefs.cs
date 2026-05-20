using BoDi;
using CanvasReplyQaTask.Models;
using CanvasReplyQaTask.Pages;
using CanvasReplyQaTask.Utils.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CanvasReplyQaTask.StepDefs
{
    [Binding]
    public class ContactsStepDefs
    {
        private readonly ContactsPage _contactPage;
        private readonly IObjectContainer _objectContainer;

        public ContactsStepDefs(IObjectContainer objectContainer)
        {
            _contactPage = new ContactsPage();
            _objectContainer = objectContainer;
        }

        [When(@"I create a new contact")]
        public void WhenICreateANewContact(Table table)
        {
            _contactPage.ClickLinkName("Create Contact");
            var model = table.CreateInstance<ContactModel>();
            if (model.Lastname == "@Random")
                model.Lastname = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
            _objectContainer.RegisterInstanceAs(model);
            _contactPage.EnterContactDetails(model);
            _contactPage.ClickSaveButton();
        }

        [Then(@"the contact details has successfully saved")]
        public void ThenTheContactDetailsHasSuccessfullySaved()
        {
            var actual = _contactPage.GetContactDetails();
            actual.Categories = actual.Categories.Replace("\r\n", ",");
            var expected = _objectContainer.Resolve<ContactModel>();
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

    }
}
