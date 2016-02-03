using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace jcWebGuiTools
{
    /// <summary>
    /// Wrapper for selenium element
    /// </summary>
    public class jcElementWrapper
    {
        IWebElement _element;
        string _errorMsg = String.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcElementWrapper"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public jcElementWrapper(IWebElement element)
        {
            _element = element;
        }
        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="errorMsg">The error MSG.</param>
        /// <returns></returns>
        public jcElementWrapper SetErrorMsg(string errorMsg)
        {
            _errorMsg = errorMsg;
            return this;
        }
        /// <summary>
        /// Throws error if element is not found.
        /// </summary>
        /// <param name="throwMessage">The throw message.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public jcElementWrapper ThrowIfNotFound(string throwMessage)
        {
            if (_element == null)
            {
                throw new Exception(String.Format("Error in element wrapper: {0}, {1}", throwMessage, _errorMsg));
            }
            return this;
        }
        /// <summary>
        /// Clears the element.
        /// SeleniumElement.Clear(). Used to clear text boxes.
        /// </summary>
        /// <returns></returns>
        public jcElementWrapper Clear()
        {
            _element.Clear();
            return this;
        }
        /// <summary>
        /// Sets the text on the element.
        /// </summary>
        /// <param name="textToSet">The text to set.</param>
        /// <returns></returns>
        public jcElementWrapper SetText(string textToSet)
        {
            this.Clear();
            _element.SendKeys(textToSet);
            return this;
        }
        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <returns></returns>
        public jcElementWrapper Click()
        {
            _element.Click();
            return this;
        }
        
        public List<jcElementWrapper> GetAsList()
        {
            var returnList = new List<jcElementWrapper>();
            var elements = _element.FindElements(By.CssSelector("li"));
            foreach (var el in elements)
            {
                returnList.Add(new jcElementWrapper(el));
            }
            return returnList;
        }

        public jcElementWrapper FindSubElement(string css)
        {
            var theElement = _element.FindElements(By.CssSelector(css)).FirstOrDefault();
            return new jcElementWrapper(theElement);
        }

        public string GetElementText()
        {
            return _element.Text;
        }

        public int GetHashCode(int currHash)
        {
            if (_element == null)
            {
                return currHash;
            }
            return GetHashCode();
        }

        public int GetHashCode()
        {
            return _element.GetAttribute("outerHTML").GetHashCode();
        }
    }
}
