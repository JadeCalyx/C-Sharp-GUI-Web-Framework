using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    /// <summary>
    /// 
    /// </summary>
    public class jcPageObjectRepository
    {
        private Dictionary<string, List<List<string>>> _pagesInfo = new Dictionary<string, List<List<string>>>();

        /// <summary>
        /// Provides access to page object info.
        /// Meant to eventually be flexible as to contianers.
        /// Currently only handles excel files.
        /// </summary>
        /// <param name="wbPath">Path to the excel workbook.
        /// Must end with fully qualifed file name.</param>
        public jcPageObjectRepository(string wbPath)
        {
            loadWorksheets(wbPath);
        }
        /// <summary>
        /// Get a sheet as a list of lists form the stored page object store.
        /// </summary>
        /// <param name="pageHandle">Name of the page to get.
        ///  default is addresses.
        /// For an excel worksheet it is the tab sheet name.</param>
        /// <returns></returns>
        public List<List<string>> GetAddressInfo(String addressType = "addresses")
        {
            return getPageInfo(addressType);
        }
        /// <summary>
        /// Gets a sheet from the page object store as a list of lists.
        /// </summary>
        /// <param name="pageHandle">Sheet name to get.</param>
        /// <returns></returns>
        public List<List<string>> GetPageObjectInfo(String pageHandle)
        {
            return getPageInfo(pageHandle);
        }
        /// <summary>
        /// Gets a sheet name form the stored workbook info. If sheet is not found, returns null.
        /// </summary>
        /// <param name="pageHandle"></param>
        /// <returns></returns>
        private List<List<string>> getPageInfo(string pageHandle)
        {
            if (!_pagesInfo.ContainsKey(pageHandle.ToLower()))
            {
                Console.WriteLine(String.Format("Worksheet {0} not found, returning empty list", pageHandle));
                return null;
            }
            return _pagesInfo[pageHandle.ToLower()];
        }
        /// <summary>
        /// Makes the call to load the file into memory.
        /// </summary>
        /// <param name="wbPath">Path to where workbook is stored. Must end in fully qualified
        /// file name.</param>
        private void loadWorksheets(string wbPath)
        {
            var type = wbPath.Split('.').LastOrDefault();

            switch (type.ToLower())
            {
                case "xlsx":
                    loadFromExcel(wbPath);
                    break;
                default:
                    throw new Exception(String.Format("Cannot identify file type from filename {0}",
                        wbPath));
            }

        }
        /// <summary>
        /// Calls the reader to load an excel workbook, and stores the results locally.
        /// </summary>
        /// <param name="wbPath">Path to the workbook.</param>
        private void loadFromExcel(String wbPath)
        {
            _pagesInfo = new jcExcelSiteInfoReader(String.Format(wbPath)).GetWorksheets();
        }
    }
}



