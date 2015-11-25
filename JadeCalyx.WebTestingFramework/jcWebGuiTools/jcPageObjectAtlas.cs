using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    public class jcPageObjectAtlas
    {
        string _site;
        string _pageHandle;
        Dictionary<string, List<jcPageObjectLookupPair>> _pageObjectIndex;

        public jcPageObjectAtlas(string site, string pageHandle)
        {
            _site = site;
            _pageHandle = pageHandle;
            _pageObjectIndex = new Dictionary<string, List<jcPageObjectLookupPair>>();
            loadIndex();
        }
    
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
