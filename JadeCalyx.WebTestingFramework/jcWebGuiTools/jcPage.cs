using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    public class jcPage
    {
        IWebDriver _driver;
        string _site;
        string _pageHandle;
        jcPageObjectAtlas _objectAtlas;

        public jcPage(IWebDriver driver, string site, string pageHandle)
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

        public bool IsCurrentHandle(string handleToCompare)
        {
            return handleToCompare.Equals(_pageHandle);
        }

        public jcPage SetText(string objectHandle, string textToSet)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.Clear();
            el.SendKeys(textToSet);
            return this;
        }

        public jcPage Click(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.Click();
            return this;
        }

        public IWebElement getElement(Stack<jcPageObjectLookupPair> lookupInfo, IWebElement currElement)
        {
            if (lookupInfo.Count < 1)
            {
                return currElement;
            }
            IReadOnlyCollection<IWebElement> elements;
            var currLookup = lookupInfo.Pop();
            if (currElement == null)
            {
                elements = _driver.FindElements(By.CssSelector(currLookup.Details));
            }
            else
            {
                elements = currElement.FindElements(By.CssSelector(currLookup.Details));
            } 
            if (elements.Count < 1)
            {
                return null;
            }
            else
            {
                return getElement(lookupInfo, elements.First());
            }
        }

    }
}
