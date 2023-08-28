using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Framework.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal TruncateWithoutRound(this decimal number, int decimalPlaces) 
        {
            var pow = (decimal)Math.Pow(10, decimalPlaces);
            return Math.Truncate(number * pow) / pow;
        }
    }
}
