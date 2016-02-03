using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcWebGuiTools;
using NUnit.Framework;
using TestSets.Utilities;
using Common;
using System.Threading;



namespace TestSets.Tests
{
    class HistoryTests
    {
        private AppFile _appFile;
        private jcBrowser _browser;
        private jcBrowserFactory _browserFactory;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _appFile = new AppFile();
            _browserFactory = new jcBrowserFactory("Wikipedia", _appFile.WebPrefix);
        }

        [OneTimeTearDown]
        public void ClassTeardown()
        {
        }

        [SetUp]
        public void TestSetup()
        {

        }

        [TearDown]
        public void TestTeardown()
        {
            _browser.Close();
        }

        /// <summary>
        /// Validates that the main page seach performs as expected.
        /// Stories: Wiki-101, Wiki-153
        /// Bugs: Wiki-937
        /// </summary>
        [Test]
        [Category("GUI")]
        [Category("History")]
        [Description("This test determines if the history link takes you to the correct page.")]
        [TestCase("firefox")]
        [TestCase("chrome")]
        public void OpenPageHistory(string browserType)
        {
            _browser = _browserFactory.GetBrowser(browserType);
            _browser.Maximize();
            _browser.GotoPage("main-page");
            var currPage = _browser.GetPage();
            currPage.Click("view-history-anchor");
            _browser.WaitForPageChange();
            var newPage = _browser.GetPage();
            Assert.That(newPage.Handle.Equals("view-history-page"),
                String.Format("Landed on wrong page: {0}"), newPage.Handle);
        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        [TestCase("chrome", "main-page", "limit-to-20-anchor", 20)]
        [TestCase("chrome", "archery-page", "limit-to-50-anchor", 50)]
        [TestCase("chrome", "main-page", "limit-to-100-anchor", 100)]
        [TestCase("chrome", "archery-page", "limit-to-250-anchor", 250)]
        [TestCase("chrome", "main-page", "limit-to-500-anchor", 500)]
        [TestCase("firefox", "archery-page", "limit-to-20-anchor", 20)]
        [TestCase("firefox", "main-page", "limit-to-50-anchor", 50)]
        [TestCase("firefox", "archery-page", "limit-to-100-anchor", 100)]
        [TestCase("firefox", "main-page", "limit-to-250-anchor", 250)]
        [TestCase("firefox", "archery-page", "limit-to-500-anchor", 500)]
        public void FilterHistoryPageListEntries(string browser, 
            string subjectPage, string anchorToClick, int expectedCount)
        {
            //open browser
            _browser = _browserFactory.GetBrowser(browser);
            _browser.Maximize();
            //go to expected page
            _browser.GotoPage(subjectPage);
            //click history tab
            _browser.GetPage().Click("view-history-anchor");
            //click a limit amount
            var currPage = _browser.GetPage();
            currPage.Click(anchorToClick);
            //count limit amount displayed rows
            var listItems = currPage.GetWebList("page-history-list");
            var count = listItems.Count;
            Assert.That(count == expectedCount, String.Format("Wrong number of items. Expected {0}, Found {1}",
                expectedCount, count));
        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        public void HistoryOrderOldest()
        {
            _browser = _browserFactory.GetBrowser("chrome");
            _browser.Maximize();
            _browser.GotoPage("main-page");
            _browser.GetPage().Click("view-history-anchor");
            var page = _browser.GetPage();

            var preClickHashCode = page.GetElement("page-history-list").GetHashCode();
            page.Click("oldest-anchor");
            var changed = page.WaitTillHashChanges(preClickHashCode, "page-history-list");
            Assume.That(changed, "History list did not change after click");
            var listItems = page.GetWebList("page-history-list");

            var dateList = new List<DateTime>();
            foreach (var el in listItems)
            {
                var d = el.FindSubElement("a[class=\"mw-changeslist-date\"]").GetElementText();
                dateList.Add(DateHelper.ConvertWikipediaHistoryDateString(d));
            }
            var error = "";
            for(var i = 1; i < dateList.Count; i++)
            {
                var compare = DateTime.Compare(dateList[i - 1], dateList[i]);
                if (compare > 0)
                    error += String.Format("Dates out of order. upper date: {0}, lower date {1}",
                        dateList[i - 1], dateList[i]);
            }
            Assert.That(error.Length.Equals(0), error);
        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        public void HistoryOrderNewest()
        {
            _browser = _browserFactory.GetBrowser("chrome");
            _browser.Maximize();
            _browser.GotoPage("main-page");
            _browser.GetPage().Click("view-history-anchor");
            var currPage = _browser.GetPage();
            var listItems = currPage.GetWebList("page-history-list");
            var dateList = new List<DateTime>();
            foreach (var el in listItems)
            {
                var d = el.FindSubElement("a[class=\"mw-changeslist-date\"]").GetElementText();
                dateList.Add(DateHelper.ConvertWikipediaHistoryDateString(d));
            }
            var error = "";
            for (var i = 1; i < dateList.Count; i++)
            {
                var compare = DateTime.Compare(dateList[i - 1], dateList[i]);
                if (compare < 0)
                    error += String.Format("Dates out of order. upper date: {0}, lower date {1}",
                        dateList[i - 1], dateList[i]);
            }
            Assert.That(error.Length.Equals(0), error);
        }
    }
}
