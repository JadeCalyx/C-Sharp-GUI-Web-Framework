using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    /// <summary>
    /// A helper object to store info on object location.
    /// </summary>
    public class jcPageObjectAtlas
    {
        string _site;
        string _pageHandle;
        Dictionary<string, List<jcPageObjectLookupPair>> _pageObjectIndex;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPageObjectAtlas"/> class.
        /// Provides a way to access the object lookup info.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="pageHandle">The page handle.</param>
        public jcPageObjectAtlas(string site, string pageHandle)
        {
            _site = site;
            _pageHandle = pageHandle;
            _pageObjectIndex = new Dictionary<string, List<jcPageObjectLookupPair>>();
            loadIndex();
        }
        /// <summary>
        /// Gets the looukup information.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public Stack<jcPageObjectLookupPair> GetLooukupInfo(string objectHandle)
        {
            var returnStack = new Stack<jcPageObjectLookupPair>();
            var lookupList = expandHandles(objectHandle);
            for (var i = (lookupList.Count - 1); i > -1; i--)
            {
                returnStack.Push(lookupList[i]);
            }
            return returnStack;
        }
        /// <summary>
        /// Expands the handles. Takes the objects lookups that have a definition of
        /// handle and replaces them with the detailed lookup.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        private List<jcPageObjectLookupPair> expandHandles(string objectHandle)
        {
            var returnList = new List<jcPageObjectLookupPair>();
            var lookupList = _pageObjectIndex[objectHandle];
            foreach (var lookupItem in lookupList)
            {
                if (lookupItem.Method.Equals("css"))
                {
                    returnList.Add(lookupItem);
                }
                if (lookupItem.Method.Equals("handle"))
                {
                    returnList.AddRange(expandHandles(lookupItem.Details));
                }
            }
            return returnList;
        }
        /// <summary>
        /// Loads the object index.
        /// </summary>
        private void loadIndex()
        {
            var objectList = new jcPageObjectInfoReader(_site).GetObjectLookupList(_pageHandle);
            foreach (var item in objectList)
            {
                var currList = new List<jcPageObjectLookupPair>();
                for (var i = 1; i < item.Length; i += 2)
                {
                    currList.Add(new jcPageObjectLookupPair(item[i], item[i+1]));
                }
                _pageObjectIndex.Add(item[0], currList);
            }
        }

    }
}
