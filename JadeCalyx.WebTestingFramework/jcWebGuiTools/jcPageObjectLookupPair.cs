using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    public class jcPageObjectLookupPair
    {
        string _lookupMethod;
        string _lookupValue;

        public jcPageObjectLookupPair(string lookupMethod, string lookupValue)
        {
            _lookupMethod = lookupMethod;
            _lookupValue = lookupValue;
        }

        public string Method
        {
            get { return _lookupMethod; }
            set { }
        }

        public string Details
        {
            get { return _lookupValue; }
            set { }
        }
    }
}
