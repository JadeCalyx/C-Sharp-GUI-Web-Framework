using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    public class jcPageFactory
    {
        string _site;
        IWebDriver _driver;
        jcAddressAtlas _atlas;

        public jcPageFactory(IWebDriver driver, jcAddressAtlas atlas, string site)
        {
            _site = site;
            _driver = driver;
            _atlas = atlas;
        }

        public jcPage GetPage(jcPage currPage)
        {
            var url = _driver.Url.ToString();
            var pageHandle = _atlas.GetPageHandleFromUrl(url);
            return makePage(currPage, pageHandle);
        }

        private jcPage makePage(jcPage currPage, string newPageHandle)
        {
            if (currPage == null) {
                currPage = new jcPage(_driver, _site, newPageHandle);
            }
            if (!currPage.Handle.Equals(newPageHandle))
            {
                return new jcPage(_driver, _site, newPageHandle);
            }
            else
            {
                return currPage;
            }
        }

    }
}
