using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Framework.FileReader.ExcelFluentReader
{
    public class PropertyConfiguration
    {
        public MemberInfo Member { get; set; }

        public string Name { get; set; }

        public string ColumnIndex { get; set; }

        public Func<string, decimal> DecimalFormatFunc { get; set; }

        public Func<string, string> StringFormatFunc { get; set; }

        public Func<string, DateTime> DateFormatFunc { get; set; }
        
        public PropertyConfiguration WithColumnName(string name)
        {
            Name = name;
            return this;
        }

        public PropertyConfiguration Format(Func<string, DateTime> Func)
        {
            DateFormatFunc = Func;
            return this;
        }

        public PropertyConfiguration Format(Func<string, Decimal> Func)
        {
            DecimalFormatFunc= Func;
            return this;
        }

        public PropertyConfiguration Format(Func<string, string> Func)
        {
            StringFormatFunc = Func;
            return this;
        }
    }
}
