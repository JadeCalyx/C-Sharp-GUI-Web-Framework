using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestSets.Utilities
{
    public class AppFile
    {

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
