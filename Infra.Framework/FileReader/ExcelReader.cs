using Infra.Framework.FileReader.ExcelFluentReader;
using LinqToExcel;
using OfficeOpenXml;
using System.ComponentModel;
using System.Linq.Expressions;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Infra.Framework.FileReader
{
    public static class ExcelReader
    {
        public static IEnumerable<T> Read<T>(string path)
        {
            using (var factory = new ExcelQueryFactory(path)) 
            {
                return factory.Worksheet<T>(0);
            }
        }

        public static IEnumerable<T> Read<T>(string path, IDictionary<Expression<Func<T, object>>, string> mappings, string workSheetName = null) 
        {
            if (mappings == null || !mappings.Any())
            {
                throw new Exception("Invalid Collection");
            }

            using (var factory = new ExcelQueryFactory(path))
            {
                foreach (var mapping in mappings)
                {
                    factory.AddMapping<T>(mapping.Key, mapping.Value);
                }
                return string.IsNullOrWhiteSpace(workSheetName)
                     ? factory.Worksheet<T>(0)
                     : factory.Worksheet<T>(workSheetName);
            }
        }

        public static IEnumerable<TDomain> ReadRecords<TDomain>(string path, int headerIndex = 1, string sheet = null, Type type = null) 
            where TDomain : new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = string.IsNullOrWhiteSpace(sheet)
                              ? package.Workbook.Worksheets.FirstOrDefault()
                              : package.Workbook.Worksheets.FirstOrDefault(i => i.Name == sheet);
                var results = worksheet.Read<TDomain>(headerIndex, type);
                return results;
            }
        }

    }
}
