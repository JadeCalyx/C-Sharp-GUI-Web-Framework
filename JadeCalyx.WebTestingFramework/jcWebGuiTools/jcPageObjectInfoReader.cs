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
    /// 
    /// </summary>
    class jcPageObjectInfoReader
    {
        string _site;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public jcPageObjectInfoReader(string site)
        {
            _site = site;
        }
        /// <summary>
        /// Gets the object lookup list.
        /// </summary>
        /// <param name="pageHandle">The page handle.</param>
        /// <returns></returns>
        public List<string[]> GetObjectLookupList(string pageHandle)
        {
            var returnList = new List<string[]>();
            string basepath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = String.Format(@"{0}\SiteInfo\{1}\PageInfo\{2}.tsv",
                basepath, _site, pageHandle);
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
                    if (parts[0].Equals("++") && (parts.Length > 1))
                    {
                        returnList.AddRange(GetObjectLookupList(parts[1]));
                    }
                }
            }
            return returnList;
        }

    }
}
