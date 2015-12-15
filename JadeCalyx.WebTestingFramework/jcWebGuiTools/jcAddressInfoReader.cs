using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace jcWebGuiTools
{
    /// <summary>
    /// Reads the address info from the file it is stored in.
    /// </summary>
    public class jcAddressInfoReader
    {
        string _site;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcAddressInfoReader"/> class.
        /// </summary>
        /// <param name="site">The site handle name.</param>
        public jcAddressInfoReader(string site)
        {
            _site = site;
        }
        /// <summary>
        /// Gets the address list.
        /// </summary>
        /// <param name="fileName">Name of the file without suffix. Defaults to "addresses". </param>
        /// <returns>A list of string arrays. 
        /// Each array is in the format of handle, segment, mask.</returns>
        public List<string[]> GetAddressList(string fileName = "addresses")
        {
            var returnList = new List<string[]>();
            string basepath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = String.Format(@"{0}\SiteInfo\{1}\AddressInfo\{2}.tsv",
                basepath, _site, fileName);
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split('\t').ToArray();
                if (parts.Length > 0)
                {
                    if (parts[0].Equals("+") && (parts.Length > 3))
                    {
                        returnList.Add(parts.Skip(1).ToArray());
                    }
                }
            }
            return returnList;
        }

    }
}
