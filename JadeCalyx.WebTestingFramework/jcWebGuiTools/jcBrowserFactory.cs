using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    /// <summary>
    /// A factory to create browser instances.
    /// </summary>
    public class jcBrowserFactory
    {
        string _site;
        string _prefix;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowserFactory"/> class.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="prefix">The prefix.</param>
        public jcBrowserFactory(string site, string prefix)
        {
            _site = site;
            _prefix = prefix;
        }
        /// <summary>
        /// Creates a new instance of a browser and returns it.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public jcBrowser GetBrowser(string type)
        {
            return new jcBrowser(type, _site, _prefix);
        }

    }
}
