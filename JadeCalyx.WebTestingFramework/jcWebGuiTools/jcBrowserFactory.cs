using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    public class jcBrowserFactory
    {
        string _site;
        string _prefix;

        public jcBrowserFactory(string site, string prefix)
        {
            _site = site;
            _prefix = prefix;
        }

        public jcBrowser GetBrowser(string type)
        {
            return new jcBrowser(type, _site, _prefix);
        }

    }
}
