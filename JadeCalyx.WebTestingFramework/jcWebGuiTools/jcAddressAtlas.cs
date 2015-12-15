using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    /// <summary>
    /// Holds the addresses and the handles they map to for the site being tested
    /// </summary>
    public class jcAddressAtlas
    {
        Dictionary<string, jcAddressObject> _addressIndex;
        string _prefix;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcAddressAtlas"/> class.
        /// </summary>
        /// <param name="prefix">The base url of the site being tests. </param>
        /// <param name="site">The site handle. This will be used to lookup the 
        /// file containing the addresses to load.</param>
        public jcAddressAtlas(string prefix, string site) 
        {
            _prefix = prefix.TrimEnd('/');
            loadIndex(site);
        }
        /// <summary>
        /// Gets the URL that maps to the handle.
        /// </summary>
        /// <param name="handle">The handle of the url to return</param>
        /// <returns>string</returns>
        public string GetUrl(string handle)
        {
            var segment = _addressIndex[handle].Segment;
            var url = String.Format("{0}/{1}", _prefix, segment.TrimStart('/'));
            return url;
        }
        /// <summary>
        /// Gets the page handle from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>string: the first handle with a regex mask that matches the passed url string.</returns>
        public string GetPageHandleFromUrl(string url)
        {
            string returnKey = "dummy";
            foreach (var item in _addressIndex)
            {
                if (item.Value.MatchesAddress(url))
                {
                    returnKey = item.Key;
                    break;
                }
            }
            return returnKey;
        }
        /// <summary>
        /// Loads the index.
        /// </summary>
        /// <param name="site">The site name handle.</param>
        private void loadIndex(string site)
        {
            _addressIndex = new Dictionary<string, jcAddressObject>();
            int handle = 0;
            int segment = 1;
            int mask = 2;
            var addresses = new jcAddressInfoReader(site).GetAddressList();
            foreach (var address in addresses)
            {
                _addressIndex.Add(address.ElementAt(handle),
                    new jcAddressObject(address.ElementAt(segment),
                    address.ElementAt(mask)));
            }
        }
    }
}
