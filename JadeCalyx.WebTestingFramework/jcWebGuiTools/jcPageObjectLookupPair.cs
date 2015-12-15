using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    /// <summary>
    /// An object to store a single page object lookup pair (method and details)
    /// </summary>
    public class jcPageObjectLookupPair
    {
        string _lookupMethod;
        string _lookupDetails;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPageObjectLookupPair"/> class.
        /// </summary>
        /// <param name="lookupMethod">The lookup method.</param>
        /// <param name="lookupDetails">The lookup details.</param>
        public jcPageObjectLookupPair(string lookupMethod, string lookupDetails)
        {
            _lookupMethod = lookupMethod;
            _lookupDetails = lookupDetails;
        }
        /// <summary>
        /// Gets the lookup method.
        /// Setting is disabled.
        /// </summary>
        public string Method
        {
            get { return _lookupMethod; }
            set { }
        }
        /// <summary>
        /// Gets the lookup details.
        /// Setting is disabled.
        /// </summary>
        public string Details
        {
            get { return _lookupDetails; }
            set { }
        }
    }
}
