using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    public class jcWebPage
    {
        IWebDriver _driver;
        string _site;
        string _pageHandle;
        jcPageObjectAtlas _objectAtlas;

        public jcWebPage(IWebDriver driver, string site, string pageHandle)
        {
            _driver = driver;
            _site = site;
            _pageHandle = pageHandle;
            _objectAtlas = new jcPageObjectAtlas(_site, _pageHandle);
        }

    }
}
