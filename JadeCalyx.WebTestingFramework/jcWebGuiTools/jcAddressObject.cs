using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace jcWebGuiTools
{
    public class jcAddressObject
    {
        String _segment;
        Regex _maskRegex;

        public jcAddressObject(String segment, string mask)
        {
            _segment = segment;
            _maskRegex = new Regex(mask);
        }
        
        public String Segment
        {
            get { return _segment; }
            set { }
        }

        public bool MatchesAddress(String url)
        {
            return _maskRegex.IsMatch(url);
        }
    }
}
