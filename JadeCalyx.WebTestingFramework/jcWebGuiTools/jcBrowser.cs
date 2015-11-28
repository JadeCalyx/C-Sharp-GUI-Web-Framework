using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace jcWebGuiTools
{
    public class jcBrowser
    {
        IWebDriver _driver;
        jcAddressAtlas _addressAtlas;
        string _site;
        jcPageFactory _pageFactory;
        jcPage _currPage = null;
         
        public jcBrowser(string driverType, string site, string urlPrefix)
        {
            _site = site;
            _addressAtlas = new jcAddressAtlas(urlPrefix, site);
            setrDriver(driverType);
            _pageFactory = new jcPageFactory(_driver, _addressAtlas, _site);
        }

        public void Close()
        {
            _driver.Quit();
        }

        public jcPage GetPage()
        {
            _currPage = _pageFactory.GetPage(_currPage);
            return _currPage;
        }

        public void GotoPage(string handle)
        {
            var url = _addressAtlas.GetUrl(handle);
            _driver.Navigate().GoToUrl(url);
        }

        public void GotoUrl(String url)
        {
            _driver.Navigate().GoToUrl(url);
        }

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
