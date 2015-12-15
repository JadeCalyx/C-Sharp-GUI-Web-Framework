using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

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
        string _site;
        jcPageFactory _pageFactory;
        jcPage _currPage = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowser"/> class.
        /// </summary>
        /// <param name="driverType">Type of the driver. Firefox or Chrome. </param>
        /// <param name="site">The site handle or name.</param>
        /// <param name="urlPrefix">The URL prefix. The base url to be prefixed onto any address.</param>
        public jcBrowser(string driverType, string site, string urlPrefix)
        {
            _site = site;
            _addressAtlas = new jcAddressAtlas(urlPrefix, site);
            setrDriver(driverType);
            _pageFactory = new jcPageFactory(_driver, _addressAtlas, _site);
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
                case "firefox": _driver = new FirefoxDriver();
                    break;
                default: throw new Exception(String.Format("Invalid browser type of {0} specified.", driverType));
            }
        }
    }
}
