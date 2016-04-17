using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Reflection;

namespace jcWebGuiTools
{
    /// <summary>
    /// 
    /// </summary>
    public class jcExcelSiteInfoReader
    {
        private Dictionary<string, List<List<string>>> _worksheets = new Dictionary<string, List<List<string>>>();

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="wbPath">Path to the workbook to read.</param>
        public jcExcelSiteInfoReader(string wbPath)
        {
            loadExcelWorkbook(wbPath);
        }

        /// <summary>
        /// Returns the entire excel file stored as a dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<List<string>>> GetWorksheets()
        {
            return _worksheets;
        }

        /// <summary>
        /// Loads an excel xslx file into the _worksheets dictionary.
        /// </summary>
        /// <param name="wbPath"></param>
        private void loadExcelWorkbook(string wbPath)
        {
            Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            if (!File.Exists(wbPath))
            {
                throw new Exception(String.Format("Excel workbook file not found at: {0}",
                    wbPath));
            }
            byte[] file = File.ReadAllBytes(wbPath);
            var ms = new MemoryStream(file);
            using (var package = new ExcelPackage(ms))
            {
                if (package.Workbook.Worksheets.Count == 0)
                    Console.Write("*** Your Excel file does not contain any work sheets");
                else
                {
                    foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                    {
                        var worksheetRows = new List<List<string>>();
                        try
                        {
                            worksheet.Select(worksheet.Dimension.Address);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("*** Worksheet was empty");
                            _worksheets.Add(worksheet.Name.ToLower(), worksheetRows);
                            continue;
                        }
                        var range = worksheet.SelectedRange;

                        var rowCount = range.Rows;
                        var colCount = range.Columns;
                        for (var i = 1; i <= rowCount; i++)
                        {
                            var currRow = processCellsInARow(range, i, colCount);
                            if (currRow.Any())
                            {
                                worksheetRows.Add(currRow);
                            }
                        }
                        _worksheets.Add(worksheet.Name.ToLower(), worksheetRows);
                    }
                }
            }
        }

        /// <summary>
        /// Read one row from worksheet.
        /// If the first column is blank or "-" return an empty list.
        /// Cells containing the string BLANK are returned as String.Empty;
        /// </summary>
        /// <param name="range"></param>
        /// <param name="row"></param>
        /// <param name="colCount"></param>
        /// <returns>List<string> with each item in the row.</string></returns>
        private List<string> processCellsInARow(ExcelRange range, int row, int colCount)
        {
            var returnRow = new List<string>();

            for (var i = 1; i <= colCount; i++)
            {
                var currCell = range[row, i].Text.Trim();
                if (i == 1 && (currCell == "-" | currCell == String.Empty))
                {
                    return returnRow;
                }
                if (currCell == "BLANK")
                {
                    currCell = String.Empty;
                }
                if (currCell.Equals(string.Empty))
                {
                    continue; //do not add blank cells
                }
                returnRow.Add(currCell);
            }
            return returnRow;
        }
    }
}

