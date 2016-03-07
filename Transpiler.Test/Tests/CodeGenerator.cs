using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelBuilder.IO;
using Transpilation.Test.Models;
using Transpilation.Transpilers;

namespace Transpilation.Test.Tests
{
    [TestClass]
    public class CodeGenerator
    {
        /// <summary>
        /// Creates JavaScript models based on C# classes inheriting from ITranspilable
        /// </summary>
        /// <returns></returns>
        protected string Transpile()
        {
            //Arrange
            var transpiler = new ES5Transpiler(new Type[] { typeof(ICanBeTranspiled) });
            transpiler.ModelGlobalNamespace =  "sveModel";
            var transcompileAssembly = Assembly.GetExecutingAssembly();
            var parentAssembly = Assembly.GetExecutingAssembly();

            string model = transpiler.GenerateCode(transcompileAssembly, TranspileOptions.CamelCaseModelNames | TranspileOptions.UseGlobalNamespace);

            return model;
        }

        [TestMethod]
        public void CodeGenerator_Should_DefaultToExecutingAssemblyPath()
        {
            //Arrange
            string thisAssemblyPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:///", string.Empty));

            //Act
            var assemblyPath = string.Empty.DefaultToExecutingAssembly();

            //Assert
            Assert.AreEqual(assemblyPath, thisAssemblyPath);
        }

        /// <summary>
        /// Creates a JavaSCript class in the JavaScript Test Project
        /// </summary>
        [TestMethod]
        public void CodeGenerator_Should_GenerateJavaScript()
        {
            //Arrange

            //Act
            string model = Transpile();
            
            string jasmineTestProjectOutputPath = Path.Combine(AssemblyDirectory, @"..\..\..", @"Transpiler.JavaScript.Test\Transpiled", "jsmodel.js");

            File.WriteAllText(jasmineTestProjectOutputPath, model);
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
