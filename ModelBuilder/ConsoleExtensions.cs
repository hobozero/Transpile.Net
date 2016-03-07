using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBuilder
{
   
    static class ConsoleExtensions
    {
        public static Dictionary <string, string> ValidateArguments(this string[] args, List<string> required){
            var argDict = args.ToDictionary(arg => arg.Split('=')[0],
                                                     arg => arg.Split('=')[1]);
            var missing = required.Except(argDict.Keys).ToArray();
            if (missing.Length > 0)
            {
                throw new MissingConsoleArgumentException(missing);
            }

            return argDict;
        }
    }
}
