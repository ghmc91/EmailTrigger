using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Framework.FileSystem
{
    public static class FileSystemService
    {
        public static IEnumerable<FileSystemInfo> GetAllFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception(string.Format("{0} {1}", "Invalid Path", path));
            }
            return new DirectoryInfo(path).GetFileSystemInfos().OrderBy(x => x.Name);
        }

        public static IEnumerable<FileSystemInfo> GetFiles(string path, string extensions) 
        {
            if (!Directory.Exists(path))
            {
                throw new Exception(string.Format("{0} {1}", "Invalid Path", path));
            }
            return new DirectoryInfo(path).GetFileSystemInfos()
                                          .Where(x => x.Extension == extensions)
                                          .OrderBy(x => x.Name);   
        }

        public static FileSystemInfo GetRecentFile(string path, string extensions) 
        {
            if (!Directory.Exists(path))
            {
                throw new Exception(string.Format("{0} {1}", "Invalid Path", path));
            }
            return new DirectoryInfo(path).GetFileSystemInfos()
                                          .Where(x => x.Extension == extensions)
                                          .OrderByDescending(x => x.LastWriteTime)
                                          .FirstOrDefault();
        }

    }

    
}
