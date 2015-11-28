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

        public jcElementWrapper(IWebElement element)
        {
            _element = element;
        }

        public jcElementWrapper SetErrorMsg(string errorMsg)
        {
            _errorMsg = errorMsg;
            return this;
        }

        public jcElementWrapper ThrowIfNotFound(string throwMessage)
        {
            if (_element == null)
            {
                throw new Exception(String.Format("Error in element wrapper: {0}, {1}", throwMessage, _errorMsg));
            }
            return this;
        }

        public jcElementWrapper Clear()
        {
            _element.Clear();
            return this;
        }

        public jcElementWrapper SetText(string textToSet)
        {
            this.Clear();
            _element.SendKeys(textToSet);
            return this;
        }

        public jcElementWrapper Click()
        {
            _element.Click();
            return this;
        }
    }
}
