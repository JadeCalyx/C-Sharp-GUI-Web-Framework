using jcWebGuiTools;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Threading;
using TestSets.Utilities;

namespace TestSets
{
    /// <summary>
    /// The Dev class is used for the development of tests. It is a type of sandbox.
    /// 
    /// This uses NUnit as the test runner. It should be installed as a NuGet package (look in
    /// packages.config, you should see both the NUnit and Nunit3TestAdapter packages). To run,
    /// build the solution, then click menu item Test--&gt;Windows--Test Explorer. The test explorer
    /// pane should open and have a list of tests you can run. Right click the HelloWorldTest and
    /// run it as an example of how to run a test and view its output.
    /// </summary>
    [TestFixture]
    public class Dev
    {
        private AppFile _appFile;
        private jcBrowser _br;
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

        [Test]
        public void HelloWorldTest()
        {
            Console.WriteLine("hello world");
            Assert.That(true, "test should have passed");
        }

        [Test]
        public void OpenBrowser()
        {
            var p = ConfigurationManager.AppSettings["WebPrefix"];
            _br = _browserFactory.GetBrowser("firefox");
            _br.Maximize();
            _br.GotoPage("main-page");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            _br.GetPage().SetText("search-box", "archery");
            
            _br.GetPage().Click("search-button");
            //_br.GetPage().Click("dummy-button");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.That(_br.GetPage().IsCurrentHandle("archery-page"), "not archery");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            _br.Close();
        }

        [SetUp]
        public void TestSetup()
        {
        }

        [TearDown]
        public void TestTeardown()
        {
            try
            {
                Console.WriteLine("Closing Browserr");
                _br.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error closing browser");
            }
        }
        [Test]
        public void TopBar()
        {
            var p = ConfigurationManager.AppSettings["WebPrefix"];
            _br = _browserFactory.GetBrowser("firefox");
            _br.GotoPage("main-page");
            _br.GetPage().Click("create-account-anchor");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            _br.GotoPage("main-page");
            _br.GetPage().Click("talk-anchor");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            _br.GotoPage("main-page");
            _br.GetPage().Click("contributions-anchor");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            _br.GotoPage("main-page");
            _br.GetPage().Click("login-anchor");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            _br.Close();
        }
    }
}