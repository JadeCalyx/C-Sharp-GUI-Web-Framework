﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;

namespace jcWebGuiTools
{
    /// <summary>
    /// An object which represents a displayed web page in the browser.
    /// </summary>
    public class jcPage
    {
        IWebDriver _driver;
        jcPageObjectRepository _repository;
        string _pageHandle;
        jcPageObjectAtlas _objectAtlas;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPage"/> class.
        /// </summary>
        /// <param name="driver">The selenium driver.</param>
        /// <param name="repository">The page object repository for this site.</param>
        /// <param name="pageHandle">The page handle.</param>
        public jcPage(IWebDriver driver, jcPageObjectRepository repository, string pageHandle)
        {
            _driver = driver;
            _repository = repository;
            _pageHandle = pageHandle;
            _objectAtlas = new jcPageObjectAtlas(_repository, _pageHandle);
        }
        /// <summary>
        /// Gets the page handle. Setting is turned off.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        public string Handle
        {
            get { return _pageHandle; }
            set { }
        }
        /// <summary>
        /// Determines whether the passed handle is the same as the current handle.
        /// </summary>
        /// <param name="handleToCompare">The handle to compare.</param>
        /// <returns></returns>
        public bool IsCurrentHandle(string handleToCompare)
        {
            return handleToCompare.Equals(_pageHandle);
        }
        /// <summary>
        /// Sets the text of a page object.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <param name="textToSet">The text to set.</param>
        /// <returns></returns>
        public jcPage SetText(string objectHandle, string textToSet)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.ThrowIfNotFound(objectHandle).Clear().SetText(textToSet);
            return this;
        }
        /// <summary>
        /// Clicks the specified object.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public jcPage Click(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.ThrowIfNotFound(objectHandle).Click();
            return this;
        }
        /// <summary>
        /// Gets the web list.
        /// If element is type of web list (ul or ol) then returns as list of li items.
        /// Otherwise, throws error.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public List<jcElementWrapper> GetWebList(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            return el.ThrowIfNotFound(objectHandle).GetAsList();
        }
        /// <summary>
        /// Waits the till hash changes.
        /// </summary>
        /// <param name="baseHash">The base hash.</param>
        /// <param name="objectHandle">The object handle.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool WaitTillHashChanges(int baseHash, string objectHandle, int timeout = 30)
        {
          
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            var newHash = el.GetPageHashCode(baseHash);
            while ((baseHash == newHash) && (timeout-- > 0))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                newHash = getElement(lookupInfo, null).GetPageHashCode(baseHash);
            }
            if (baseHash == newHash)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public jcElementWrapper GetElement(string objectHandle)
        { 
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            return el.ThrowIfNotFound(objectHandle);
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="lookupInfo">The lookup information.</param>
        /// <param name="currElement">The element that starts the search.</param>
        /// <returns></returns>
        private jcElementWrapper getElement(Stack<jcPageObjectLookupPair> lookupInfo, IWebElement currElement)
        {
            if (lookupInfo.Count < 1)
            {
                return new jcElementWrapper(currElement);
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
                string err = String.Format("Unable to find element: '{0} {1}'", 
                    currLookup.Method,
                    currLookup.Details);
                return new jcElementWrapper(null).SetErrorMsg(err);
            }
            else
            {
                return getElement(lookupInfo, elements.First());
            }
        }

    }
}
