using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DateHelper
    {
        public static DateTime ConvertWikipediaHistoryDateString(string theDate)
        {
            var parts = theDate.Split(',');
            var hm = parts[0].Trim().Split(':');
            var dmy = parts[1].Trim().Split(' ');
            int m;
            switch (dmy[1].ToLower())
            {
                case "january": m = 1; break;
                case "february": m = 2; break;
                case "march": m = 3; break;
                case "april": m = 4; break;
                case "may": m = 5; break;
                case "june": m = 6; break;
                case "july": m = 7; break;
                case "august": m = 8; break;
                case "september": m = 9; break;
                case "october": m = 10; break;
                case "november": m = 11; break;
                case "december": m = 11; break;
                default: m = 0; break;
            }
            //     "00:06, 4 December 2015"‎
            return new DateTime(Int32.Parse(dmy[2]), m, Int32.Parse(dmy[0]),
                Int32.Parse(hm[0]), Int32.Parse(hm[1]), 0);
        }

    }
}
