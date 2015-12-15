using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace jcWebGuiTools
{
    /// <summary>
    /// Object to hold one address info object
    /// </summary>
    public class jcAddressObject
    {
        String _segment;
        Regex _maskRegex;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcAddressObject"/> class.
        /// </summary>
        /// <param name="segment">The segment of the url to be added to the base url of the site.</param>
        /// <param name="mask">The regex mask used to match the url to a handle.</param>
        public jcAddressObject(String segment, string mask)
        {
            _segment = segment;
            _maskRegex = new Regex(mask);
        }
        /// <summary>
        /// Gets or sets the url segment.
        /// Set in turned off.
        /// </summary>
        public String Segment
        {
            get { return _segment; }
            set { }
        }
        /// <summary>
        /// Determines if the passed url string matches the 
        /// regex mask for this address.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>True if matches, false if no match.</returns>
        public bool MatchesAddress(String url)
        {
            return _maskRegex.IsMatch(url);
        }
    }
}
