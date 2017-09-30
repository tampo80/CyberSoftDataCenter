using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Helper
{
    public class DateConverter
    {
        public static string ConvertDate(string Sdate)
        {
            DateTime sourceDate = DateTime.ParseExact(Sdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string formatted = sourceDate.ToString("yyyy-MM-dd");

            return formatted;
        }
    }
}
