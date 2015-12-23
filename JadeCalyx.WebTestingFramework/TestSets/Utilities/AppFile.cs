using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestSets.Utilities
{
    /// <summary>
    /// A wrapper for the app.config file.
    /// </summary>
    public class AppFile
    {
        /// <summary>
        /// Gets the web prefix.
        /// </summary>
        /// <value>
        /// The web prefix.
        /// </value>
        public string WebPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["WebPrefix"];
            }
            set { }
        }

    }
}
