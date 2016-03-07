using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModelBuilder.IO;
using Transpilation;
using Transpilation.Transpilers;

namespace ModelBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 3)
            {
                var argDict = args.ValidateArguments(
                    new List<string>(){"outFile", "transpileAssembly", "parentAssembly"});

                string builderPath = Assembly.GetExecutingAssembly().FullPath();
                var options = TranspileOptions.None;

                var typesBesidesITranspilableToBeCompiled = new List<Type>();
                if (argDict.ContainsKey("parentType"))
                {
                    var parentAssembly = Assembly.LoadFile(
                        Path.Combine(builderPath, argDict["parentAssembly"]));

                    typesBesidesITranspilableToBeCompiled.Add(parentAssembly.GetType(argDict["parentType"]));
                }

                var transpiler = new ES5Transpiler(typesBesidesITranspilableToBeCompiled.ToArray());

                if (argDict.ContainsKey("namespace"))
                {
                    transpiler.ModelGlobalNamespace = argDict["namespace"];
                    options = TranspileOptions.UseGlobalNamespace;
                }
                if (argDict.ContainsKey("camelCase") && argDict["camelCase"].ToLower() != "false")
                {
                    options = options | TranspileOptions.CamelCaseModelNames;
                }

                var transcompileAssembly = Assembly.LoadFile(
                    Path.Combine(builderPath, argDict["transpileAssembly"]));
                
                string model = transpiler.GenerateCode(transcompileAssembly, options); 

                File.WriteAllText(argDict["outFile"], model);
            }

        }
    }
}
