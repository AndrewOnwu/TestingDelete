
using CanvasReplyQaTask.Utils.Selenium;
using CanvasReplyQaTask.Utils.Waits;
using OpenQA.Selenium;
using System.Data;

namespace CanvasReplyQaTask.Pages
{
    public class CommonFragments : BasePage
    {
        private static readonly By ShortCutsSideMenus = By.ClassName("sidebar-item-link-basic");
        private static readonly By MetaProfile = By.ClassName("meta-profile-wrap");
        private static readonly By FilterText = By.Id("filter_text");
        private static readonly By TableTextHeaders = By.CssSelector(".listHead > th");
        private static readonly By TableTextRows = By.CssSelector("tr[class^='listViewRow']");
        private static readonly By TableRowsCheckBox = By.CssSelector("td[class^='listViewTd'] > div > input");
        private static readonly By Buttons = By.CssSelector("span[class='input-label'], div[class$='input-label ']");
        public CommonFragments()
        {

        }

        public DataTable GetTable(int maxRows = 0)
        {
            DataTable table = new DataTable();
            var headerElements = SeleniumUtils.GetWebElements(TableTextHeaders);
            foreach (var headerElement in headerElements)
            {
                table.Columns.Add(headerElement.Text);
            }
            var rowElements = SeleniumUtils.GetWebElements(TableTextRows);
            for (int i = 0; i < rowElements.Count; i++)
            {
                var columnValues = rowElements[i]
                    .FindElements(By.CssSelector("td[class^='listViewTd']"))
                    .Select(x => x.Text).ToArray<object>();
                table.Rows.Add(columnValues);
                if (maxRows > 0 && maxRows-1 == i)
                    break;
            }

            return table;
        }

        public bool UserIsLoggedIn()
            => DriverWait.WaitUntilElementIdDisplayed(MetaProfile);

        public void EnterFilterText(string text)
        {
            SeleniumUtils.SendKeys(text + Keys.Enter, FilterText);
            WaitForAjax();
        }

        public DataTable SelectFirstItemsInTable(int numOfItems)
        {
            if (numOfItems < 1)
                throw new Exception($"The number {numOfItems} is not valid in current context");
            var table = GetTable(numOfItems);
            if (table.Rows.Count < 1)
                throw new Exception($"There are no results on table");
            var rowCheckBoxes = SeleniumUtils.GetWebElements(TableRowsCheckBox);

            if (numOfItems >= rowCheckBoxes.Count)
                throw new Exception($"There are not enough items in table to select");

            var itemsToSelect = rowCheckBoxes.Take(numOfItems);
            foreach (var item in itemsToSelect)
            {
                SeleniumUtils.Check(item);
            }
            return table;
        }

        public void ClickArrowButtons(string mainText, string innerText)
        {
            SeleniumUtils.Click(Buttons, mainText);
            ClickWithoutAjaxAwait(Buttons, innerText);
            if(innerText is "Delete")
                AcceptAlert();
        }

    }
}