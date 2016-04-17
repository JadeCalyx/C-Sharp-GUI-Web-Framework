using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace jcWebGuiTools
{
    /// <summary>
    /// An instance of specific browser.
    /// Provides wrapper for Selenium functions.
    /// </summary>
    public class jcBrowser
    {
        IWebDriver _driver;
        jcAddressAtlas _addressAtlas;
        jcPageObjectRepository _repository;
        jcPageFactory _pageFactory;
        jcPage _currPage = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowser"/> class.
        /// </summary>
        /// <param name="driverType">Type of the driver. Firefox or Chrome. </param>
        /// <param name="repository">The page object repository for this site.</param>
        /// <param name="urlPrefix">The URL prefix. The base url to be prefixed onto any address.</param>
        public jcBrowser(string driverType, jcPageObjectRepository repository, string urlPrefix)
        {
            _repository = repository;
            _addressAtlas = new jcAddressAtlas(urlPrefix, _repository);
            setrDriver(driverType);
            _pageFactory = new jcPageFactory(_driver, _addressAtlas, _repository);
        }
        /// <summary>
        /// Closes the browser.
        /// Performs a Selenium webdriver.Quit(). 
        /// </summary>
        public void Close()
        {
            _driver.Quit();
        }
        /// <summary>
        /// Gets the current page the browser is displaying.
        /// </summary>
        /// <returns>jcPage object representing curretly displayed page.</returns>
        public jcPage GetPage()
        {
            _currPage = _pageFactory.GetPage(_currPage);
            return _currPage;
        }
        /// <summary>
        /// Waits for the page to change.
        /// </summary>
        /// <returns></returns>
        public bool WaitForPageChange()
        {
            var currHandle = "";
            if (_currPage != null)
            {
                currHandle = _currPage.Handle;
            }
            var timeout = 30;
            this.GetPage();
            while ((_currPage.Handle == currHandle) && (timeout-- > 0))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                this.GetPage();
            }
            if (timeout > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Goes to the web page reprsented by the passed page handle.
        /// </summary>
        /// <param name="handle">The page handle.</param>
        /// <returns>jcBrowser: this browser object.</returns>
        public jcBrowser GotoPage(string handle)
        {
            var url = _addressAtlas.GetUrl(handle);
            _driver.Navigate().GoToUrl(url);
            return this;
        }
        /// <summary>
        /// Goes to a specified url.
        /// </summary>
        /// <param name="url">The full URL to open in the browser.</param>
        /// <returns>jcBrowser: this browser object.</returns>
        public jcBrowser GotoUrl(String url)
        {
            _driver.Navigate().GoToUrl(url);
            return this;
        }
        /// <summary>
        /// Maximizes this browser instance.
        /// </summary>
        /// <returns>jcBrowser: this object instance.</returns>
        public jcBrowser Maximize()
        {
            _driver.Manage().Window.Maximize();
            return this;
        }
        /// <summary>
        /// Set the Selenium web driver type.
        /// </summary>
        /// <param name="driverType">Type of the driver, such as firefox.</param>
        /// <exception cref="System.Exception">Throws an exception if passed an unknown driver name.</exception>
        private void setrDriver(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "firefox": _driver = new FirefoxDriver();
                    break;
                default: throw new Exception(String.Format("Invalid browser type of {0} specified.", driverType));
            }
        }
    }
}
