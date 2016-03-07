using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelBuilder.IO
{
    public static class FileUtil
    {
        static string currentPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:///", string.Empty));
        /// <summary>
        /// Returns the full path to the file.  If the full path is provided, it is returned unaltered.
        /// If only the name of the file is included, it defaults to the local directory of the executing assembly.
        /// </summary>
        /// <param name="fileOrPath">The file namn which may be fully qualified</param>
        /// <returns></returns>
        public static String DefaultToExecutingAssembly(this String fileOrPath)
        {
            return (fileOrPath.IndexOf('\\') > 0 || fileOrPath.IndexOf('/') > 0) ?
                    fileOrPath :
                    Path.Combine(currentPath, fileOrPath);
        }
    }
}
