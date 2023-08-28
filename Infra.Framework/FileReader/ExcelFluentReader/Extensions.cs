using Infra.Framework.Extensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Framework.FileReader.ExcelFluentReader
{
    public static class Extensions
    {
        public static IEnumerable<TDomain> Read<TDomain>(this ExcelWorksheet sheet, int headerIndex = 1, Type type = null)
            where TDomain: new()
        {
            var mapClass = type ?? AppDomain.CurrentDomain
                                            .GetAssemblies()
                                            //.Where(a => a.FullName.StartsWith(""))
                                            .SelectMany(i => i.GetTypes().Where(a => a.IsClass && typeof(BaseTypeConfiguration<TDomain>).IsAssignableFrom(a)))
                                            .FirstOrDefault();
            var instance = (BaseTypeConfiguration<TDomain>) Activator.CreateInstance(mapClass);
            var mappings = instance.GetMappings();

            var rows = sheet.Dimension.Rows;
            var cols = new List<(int index, string name, PropertyConfiguration propertyConfiguration)>();

            var results = new List<TDomain>();
            for (var i = 1; i <= sheet.Dimension.Columns; i++)
            {
                var col = mappings.FirstOrDefault(a => (!string.IsNullOrWhiteSpace(a.Name) && a.Name == (string)sheet.Cells[headerIndex, i].Value)
                                                    || (!string.IsNullOrWhiteSpace(a.ColumnIndex) && a.ColumnIndex == (string)sheet.Cells[headerIndex, i].Address));
                if (col != null)
                    cols.Add((index: i, name: col.Name, propertyConfiguration: col));
            }
            for (int i = ++headerIndex; i <= rows; i++)
            {
                var t = new TDomain();
                for (int j = 0; j < cols.Count; j++)
                {
                    var property = cols[j];
                    var propertyInfo = t.GetType().GetProperty(property.propertyConfiguration.Member.Name);
                    var value = Cast(sheet.Cells[i, property.index].Value, propertyInfo.PropertyType, property.propertyConfiguration);
                    if (propertyInfo.PropertyType.IsEnum && value != null)
                    {
                        propertyInfo.SetValue(t, Enum.Parse(propertyInfo.PropertyType, value.ToString()));
                    }
                    else
                    {
                        propertyInfo.SetValue(t, value);
                    }
                }
                results.Add(t);
            }
            return results;

        }

        private static object Cast(object value, Type propertyType, PropertyConfiguration propertyConfiguration)
        {
            if (value == null)
            {
                return value;
            }
            if (propertyType == typeof(int))
            {
                int.TryParse((propertyConfiguration.StringFormatFunc == null
                                                                       ? value.ToString()
                                                                       : propertyConfiguration.StringFormatFunc(value.ToString())),
                              out int result);
                return result;
            }
            else if (propertyType == typeof(short)) 
            {
                short.TryParse((propertyConfiguration.StringFormatFunc == null
                                                                       ? value.ToString()
                                                                       : propertyConfiguration.StringFormatFunc(value.ToString())),
                              out short result);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime))
            {
                if (propertyConfiguration.DateFormatFunc != null)
                {
                    return propertyConfiguration.DateFormatFunc(value.ToString());
                }
                return value;
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?)
                 ||  propertyType == typeof(decimal) || propertyType == typeof(decimal?)) 
            {
                if (propertyConfiguration.DecimalFormatFunc != null)
                {
                    return propertyConfiguration.DecimalFormatFunc(value.ToString());
                }
                return value.ToString().ToDecimal();
            }
            
            if (propertyConfiguration.StringFormatFunc != null)
                {
                    return propertyConfiguration.StringFormatFunc(value.ToString());
                }
            return value.ToString().Trim();
        }
    }
     
}
