using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Transpilation
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get types from provided assembly that are available in the case of missing referenced assemblies
        /// </summary>
        /// <param name="assembly">The assembly for containing the types to return</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static IEnumerable<Type> GetChildTypes(this Assembly assembly, Type baseType)
        {
            var types = new List<Type>();

            try
            {
                types.AddRange(assembly
                    .GetLoadableTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract &&
                        (baseType.IsAssignableFrom(myType) || myType.IsSubclassOf(baseType)))
                    );
            }
            catch (System.Reflection.ReflectionTypeLoadException loader)
            {
                foreach (Exception ex in loader.LoaderExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return types;
        }

        public static string FullPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Returns the contents of an embedded resource.
        /// </summary>
        /// <param name="nameSpace">Dot-separated namespace matching the folder where the resource resides in the assembly.</param>
        /// <param name="resourceFileName">File name of the embedded resource with extension.<para /><para />  Templates are in the format:<para />
        /// TranspilerTypeName.Model.languageExtension. <para />
        /// e.g.  ES5Transpiler.Master.js
        /// </param>
        /// <returns></returns>
        public static string LoadResource(this Assembly assembly, string nameSpace, string resourceFileName)
        {
            var fullResourceName = string.Join(".", assembly.GetName().Name, nameSpace, resourceFileName);
            string result = "";

            try
            {
                using (var stream = assembly.GetManifestResourceStream(fullResourceName))
                {
                    if (null == stream)
                    {
                        var resourceNames = assembly.GetManifestResourceNames();

                        throw new EndOfStreamException("The stream for " + fullResourceName +
                                                       "could not be opened. Case matters! Available resources: \r\n" +
                                                        string.Join("\r\n", resourceNames));
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
