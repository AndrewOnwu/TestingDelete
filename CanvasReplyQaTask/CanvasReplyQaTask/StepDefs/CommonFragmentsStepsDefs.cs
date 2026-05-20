using BoDi;
using CanvasReplyQaTask.Pages;
using CanvasReplyQaTask.Utils.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Data;
using TechTalk.SpecFlow;

namespace CanvasReplyQaTask.StepDefs
{
    [Binding]
    public class CommonFragmentsStepsDefs
    {
        private readonly IObjectContainer _objectContainer;
        private readonly CommonFragments _commonFragments;

        public CommonFragmentsStepsDefs(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _commonFragments = new CommonFragments();
        }

        [Then(@"I have successfully logged in")]
        public void ThenIHaveSuccessfullyLoggedIn()
            => Assert.That(_commonFragments.UserIsLoggedIn());


        [Given(@"I navigate to ""([^""]*)"" and select ""([^""]*)""")]
        public void GivenINavigateToAndSelect(string headerNav, string headerSubMenu)
        {
            _commonFragments.ClickLinkName(headerNav);
            _commonFragments.ClickLinkName(headerSubMenu);
        }

        [When(@"I search for ""([^""]*)""")]
        public void WhenISearchFor(string text)
            => _commonFragments.EnterFilterText(text);

        [Then(@"the results are successfully returned")]
        public void ThenTheSearchIsSuccessfullyReturned()
        {
            var datable = _commonFragments.GetTable();
            Assert.That(datable.Rows.Count > 0);
            _objectContainer.RegisterInstanceAs(datable);
        }

        [When(@"I select ""([^""]*)"" from search table")]
        public void WhenISelectFromSearchTable(string name)
            => _commonFragments.ClickLinkName(name);

        [When(@"I click button ""([^""]*)""")]
        public void WhenIClickButton(string buttonName)
            => _commonFragments.ClickButton(buttonName);

        [When(@"I select first (\d+) items in the table")]
        public void WhenISelectFirstItemsInTheTable(int numOfItems)
           => _objectContainer.RegisterInstanceAs(_commonFragments.SelectFirstItemsInTable(numOfItems));

        [When(@"click ""([^""]*)"" -> ""([^""]*)""")]
        public void WhenClick_(string actions, string delete)
            => _commonFragments.ClickArrowButtons(actions, delete);

        [Then(@"the items were successfully deleted")]
        public void ThenTheItemsWereSuccessfullyDeleted()
        {
            var preResultsTable = _objectContainer.Resolve<DataTable>();
            var postResultsTable = _commonFragments.GetTable();
            var commonValues = preResultsTable.Rows
                .Cast<DataRow>()
                .Intersect(postResultsTable.Rows.Cast<DataRow>());
            Assert.That(0, Is.EqualTo(commonValues.Count()), "All items were not deleted");

        }
    }
}
