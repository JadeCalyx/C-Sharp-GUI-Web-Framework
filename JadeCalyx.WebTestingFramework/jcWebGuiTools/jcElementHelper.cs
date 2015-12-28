using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    /// <summary>
    /// Performs utility functions for elements
    /// </summary>
    static class jcElementHelper
    {
        
        public static List<jcElementWrapper> TransformWebListToCSharpList(IWebElement theList)
        {
            var returnList = new List<jcElementWrapper>();
            var elements = theList.FindElements(By.CssSelector("li"));
            foreach (var el in elements)
            {
                returnList.Add(new jcElementWrapper(el));
            }
            return returnList;
        }

    }
}
