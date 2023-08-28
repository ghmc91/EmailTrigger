using EmailTrigger.Domain.Interfaces.Repostiories;
using Infra.Framework.FileReader;
using Infra.Framework.FileSystem;

namespace EmailTrigger.Infra.Data.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        public BaseRepository()
        {

        }

        public IEnumerable<TResult> ReadFile<TResult, TMappings>(Func<TResult, bool> predicate, string fileName, int headerIndex = 1, bool deleteFile = false, string sheet = null)
            where TResult : class, new()
            where TMappings : class
        {
            var result = ExcelReader.ReadRecords<TResult>(fileName, headerIndex, sheet, typeof(TMappings))
                                    .Where(predicate)
                                    .ToArray();
            if (deleteFile)
            {
                File.Delete(fileName);
            }
            return result;
        }

        public IEnumerable<TResult> ReadRecentFile<TResult, TMappings>(Func<TResult, bool> predicate, string path, int headerIndex = 1, bool deleteFile = false, string sheet = null)
            where TResult : class, new()
            where TMappings : class
        {
            var fileName = FileSystemService.GetRecentFile(path, ".xlsx");
            if (fileName == null)
            {
                throw new Exception($"Arquivo {path} não encontrado na rede");
            }
            return ReadFile<TResult, TMappings>(predicate, fileName.FullName, headerIndex, deleteFile, sheet);
        }
    }
}
