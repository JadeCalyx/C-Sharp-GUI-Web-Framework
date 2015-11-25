using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    public class jcAddressAtlas
    {
        Dictionary<string, jcAddressObject> _addressIndex;
        string _prefix;

        public jcAddressAtlas(string prefix, string site) 
        {
            _prefix = prefix.TrimEnd('/');
            loadIndex(site);
        }

        public string GetUrl(string handle)
        {
            var segment = _addressIndex[handle].Segment;
            var url = String.Format("{0}/{1}", _prefix, segment.TrimStart('/'));
            return url;
        }

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
