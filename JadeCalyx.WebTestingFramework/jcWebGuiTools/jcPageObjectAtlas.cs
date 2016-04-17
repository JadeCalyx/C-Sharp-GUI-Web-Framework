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
        jcPageObjectRepository _repository;
        string _pageHandle;
        Dictionary<string, List<jcPageObjectLookupPair>> _pageObjectIndex;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPageObjectAtlas"/> class.
        /// Provides a way to access the object lookup info.
        /// </summary>
        /// <param name="repository">The page object repository for this site.</param>
        /// <param name="pageHandle">The page handle.</param>
        public jcPageObjectAtlas(jcPageObjectRepository repository, string pageHandle)
        {
            _repository = repository;
            _pageHandle = pageHandle;
            _pageObjectIndex = new Dictionary<string, List<jcPageObjectLookupPair>>();
            loadIndex(_pageHandle);
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
        private void loadIndex(string pageHandle)
        {
            var objectList = _repository.GetPageObjectInfo(pageHandle);
            foreach (var item in objectList)
            {
                if (item.Count.Equals(0))
                {
                    continue; //go to next item; no empty rows
                }

                var processFlag = item.First();

                if (processFlag.Equals("++"))
                {
                    if (item.Count > 1)
                    {
                        this.loadIndex(item[1]);
                    }
                }
                else
                {
                    if (processFlag.Equals("+"))
                    {
                        if ((item.Count % 2) != 0)
                        {
                            throw new Exception(String.Format("Line items on page {0} not in pairs",
                                pageHandle));
                        }
                        var currList = new List<jcPageObjectLookupPair>();
                        for (var i = 2; i < item.Count; i += 2)
                        {
                            currList.Add(new jcPageObjectLookupPair(item[i], item[i + 1]));
                        }
                        _pageObjectIndex.Add(item[1], currList);
                    }
                }
            }
        }

    }
}
