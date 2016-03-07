using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation
{
    /// <summary>
    /// Language of the generated model.
    /// </summary>
    public enum OutputLanguage
    {
        [FileExtension("js")]
        JavaScript
    }

    /// <summary>
    /// Indicated the file extension for the OutputLanguage.
    /// </summary>
    public class FileExtensionAttribute : Attribute
    {
        public string Extension { get; set; }

        public FileExtensionAttribute(string extension)
        {
            this.Extension = extension;
        }
    }

    internal static class OutputLanguageExtensions
    {
        public static string FileExtension(this OutputLanguage outputLanguage)
        {
            var extension = outputLanguage.GetType()
                .GetField(outputLanguage.ToString())
                .GetCustomAttributes(typeof(FileExtensionAttribute), false)
                .SingleOrDefault() as FileExtensionAttribute;
            return extension.Extension ?? "txt";
        }
    }
}
