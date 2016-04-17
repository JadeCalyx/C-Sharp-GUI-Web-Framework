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
        jcPageObjectRepository _objectRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowserFactory"/> class.
        /// </summary>
        /// <param name="site">The site.
        /// If storing page object data in an Excel file, this it should be the
        /// fully qualified path to the .xlsx file.</param>
        /// <param name="prefix">The prefix. 
        /// This is the address prefix for urls.</param>
        public jcBrowserFactory(string site, string prefix)
        {
            _site = site;
            _prefix = prefix;
            _objectRepository = new jcPageObjectRepository(_site);
        }
        /// <summary>
        /// Creates a new instance of a browser and returns it.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public jcBrowser GetBrowser(string type)
        {
            return new jcBrowser(type, _objectRepository, _prefix);
        }

    }
}
