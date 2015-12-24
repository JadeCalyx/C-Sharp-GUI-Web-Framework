using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcWebGuiTools;
using NUnit.Framework;
using TestSets.Utilities;


namespace TestSets.Tests
{
    class SearchTests
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
        [Category("Search")]
        [Description("This test performs a valid search on the main page.")]
        [TestCase("firefox")]
        [TestCase("chrome")]
        public void PerformValidMainPageSearch(string browserType)
        {
            _browser = _browserFactory.GetBrowser(browserType);
            _browser.Maximize();
            _browser.GotoPage("main-page");
            var currPage = _browser.GetPage();
            currPage.SetText("search-box", "archery");
            currPage.Click("search-button");
            var pageChanged = _browser.WaitForPageChange();
            Assume.That(pageChanged, "Page never changed from main search page");
            var newPage = _browser.GetPage();
            Assert.That(newPage.Handle.Equals("archery-page"), "error message");
        }

    }
}
