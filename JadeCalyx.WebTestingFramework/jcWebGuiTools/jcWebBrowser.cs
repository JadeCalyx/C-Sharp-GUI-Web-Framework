using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace jcWebGuiTools
{
    public class jcWebBrowser
    {
        IWebDriver _driver;
         
        public jcWebBrowser(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Close()
        {
            _driver.Quit();
        }

        public void GotoUrl(String url)
        {
            _driver.Navigate().GoToUrl(url);
        }

    }
}
