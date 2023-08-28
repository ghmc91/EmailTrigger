using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Framework.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string value, CultureInfo cultureInfo = null, string format = null)
        {
            var dateValue = default(DateTime);
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.GetCultureInfo("pt-BR");
            }

            if (string.IsNullOrWhiteSpace(format)) 
            { 
                DateTime.TryParse(value, cultureInfo, DateTimeStyles.AssumeLocal, out dateValue);
            }
            else
            {
                DateTime.TryParseExact(value, format, cultureInfo, DateTimeStyles.None, out dateValue);
            }
            return dateValue;
        }

        public static decimal ToDecimal(this string value, CultureInfo cultureInfo = null)
        {
            var decimalValue = 0M;
            decimal.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out decimalValue);
            return decimalValue;
        }
    }
}
