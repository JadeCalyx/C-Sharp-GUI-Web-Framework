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

        public string Handle
        {
            get { return _pageHandle; }
            set { }
        }

        public jcWebPage SetText(string objectHandle, string textToSet)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo);
            el.Clear();
            el.SendKeys(textToSet);
            return this;
        }

        public jcWebPage Click(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo);
            el.Click();
            return this;
        }

        public IWebElement getElement(Stack<jcPageObjectLookupPair> lookupInfo)
        {
            var els = _driver.FindElements(By.CssSelector(lookupInfo.Peek().Value));
            return els.First();
        }

    }
}
