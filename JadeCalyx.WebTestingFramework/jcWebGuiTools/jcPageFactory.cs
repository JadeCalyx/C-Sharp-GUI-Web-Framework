using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    /// <summary>
    /// A factory method for creating page objects.
    /// </summary>
    public class jcPageFactory
    {
        string _site;
        IWebDriver _driver;
        jcAddressAtlas _atlas;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPageFactory"/> class.
        /// Factory for creating new page objects.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="atlas">The atlas.</param>
        /// <param name="site">The site.</param>
        public jcPageFactory(IWebDriver driver, jcAddressAtlas atlas, string site)
        {
            _site = site;
            _driver = driver;
            _atlas = atlas;
        }
        /// <summary>
        /// Gets the page displayed on the screen.
        /// </summary>
        /// <param name="currPage">The current or last page returned.</param>
        /// <returns></returns>
        public jcPage GetPage(jcPage currPage)
        {
            var url = _driver.Url.ToString();
            var pageHandle = _atlas.GetPageHandleFromUrl(url);
            return makePage(currPage, pageHandle);
        }
        /// <summary>
        /// Makes a new page instance.
        /// </summary>
        /// <param name="currPage">The curr page.</param>
        /// <param name="newPageHandle">The new page handle.</param>
        /// <returns></returns>
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
