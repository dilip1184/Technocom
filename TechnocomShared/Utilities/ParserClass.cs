using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TechnocomShared.Utilities
{
    public static class ParserClass
    {
        public static DateTime? GetDate(object objValue)
        {
            try
            {
                string DayValue = string.Empty;
                string MonthValue = string.Empty;
                string DateValue = string.Empty;

                String[] substrings = objValue.ToString().Split('/');

                if (substrings[0].Length == 1)
                {
                    DayValue = "0" + substrings[0].ToString();
                }
                else
                {
                    DayValue = substrings[0].ToString();
                }
                if (substrings[1].Length == 1)
                {
                    MonthValue = "0" + substrings[1].ToString();
                }
                else
                {
                    MonthValue = substrings[1].ToString();
                }

                DateValue = DayValue + "/" + MonthValue + "/" + substrings[2].ToString();

                DateTime parsedDate;
                string format = "dd/MM/yyyy";
                parsedDate = DateTime.ParseExact(DateValue, format, CultureInfo.InvariantCulture);
                return parsedDate;

            }
            catch
            {
                return null;
            }
        }
    }
}
