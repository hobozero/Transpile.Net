using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transpilation
{
    public static class StringExtensions
    {

        // Convert the string to Pascal case.
        public static string ToPascalCase(this string the_string)
        {
            TextInfo info = Thread.CurrentThread.CurrentCulture.TextInfo;
            the_string = info.ToTitleCase(the_string);
            string[] parts = the_string.Split(new char[] { },
                StringSplitOptions.RemoveEmptyEntries);
            string result = String.Join(String.Empty, parts);
            return result;
        }

        public static string ToCamelCase(this string the_string)
        {
            return the_string.Substring(0, 1).ToLower() +
                the_string.Substring(1);
        }
    }
}
