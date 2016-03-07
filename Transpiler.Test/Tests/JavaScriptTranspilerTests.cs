using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using Newtonsoft.Json;
using Transpilation;
using Transpilation.Interfaces;
using Transpilation.Scaffold;
using Transpilation.Test.Models;
using System.Collections.Generic;
using Transpilation.Transpilers;

namespace Transpilation.Test
{
    [TestClass]
    public class JavaScriptTranspilerTests: BaseTest
    {

        [TestMethod]
        public void Transpiler_Should_ReplaceTemplateValues()
        {
            //Arrange
            string testTemplate = @"/*[#namespace]*/ \\Test";
            var transpiler = new ES5Transpiler(new Type[]{typeof(ICanBeTranspiled)});
            var dict = new Dictionary<string, string>()
            {
                { "namespace", "replaceVal" }
            };

            //Act
            string result = transpiler.ReplacePlaceholders(testTemplate, dict);

            //Assert
            Assert.AreEqual(result, @"replaceVal \\Test");
        }

        [TestMethod]
        //[ExpectedException(typeof(InvalidCastException))]
        public void Transpiler_ShouldNot_ThrowExceptionForDecimal()
        {
            System.Diagnostics.Trace.WriteLine("Transpiler_Should_ThrowExceptionForDecimal");
            //Arrange
            var objUnderTest = new DecimalClass();

            //Act

            try
            {
                objUnderTest.SetDefaults();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            Assert.IsNotNull(objUnderTest);
            Assert.AreEqual(objUnderTest.FakeDecimal, 8.0m);

            //Act & Assert

            //Retaining for methodology reference.  
            //  Was originally Transpiler_Should_ThrowExceptionForDecimal since decimals weren't supported
            //  Using extended MSTest assertions in lieu of method attributes for validating exceptions.
            //Assert.Throws<InvalidCastException>(() => { gen.SetDefaults(testClass); });
        }

        [TestMethod]
        public void Transpiler_ShouldIgnore_JsonIgnoreProperty()
        {
            //Arrange
            var gen = new ScaffoldingBuilder();
            
            //Act
            var elms = gen.Scaffold(typeof(DecoratedClass), TranspileOptions.CamelCaseModelNames | TranspileOptions.UseGlobalNamespace, OutputLanguage.JavaScript);

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(elms.PropertyNames.Any(p => p == "ignoreProperty"));
        }

        [TestMethod]
        public void Transpiler_ShouldContain_CamelCaseNameSpaces()
        {
            //Arrange
            var gen = new ScaffoldingBuilder();

            //Act
            var elms = gen.Scaffold(typeof(DecoratedClass), TranspileOptions.CamelCaseModelNames | TranspileOptions.UseGlobalNamespace, OutputLanguage.JavaScript);
            string cameled = string.Join(".",
                elms.CreateInstanceWithPropertyDefaults().GetType().Namespace.Split('.')
                .Select(n => n.ToCamelCase()));

            //Assert
            Assert.IsTrue(elms.Namespace == cameled);
        }

        [TestMethod]
        public void Transpiler_ShouldIgnore_JsonRenameProperty()
        {
            //Arrange
            var gen = new ScaffoldingBuilder();

            //Act
            var elms = gen.Scaffold(typeof(DecoratedClass), TranspileOptions.CamelCaseModelNames | TranspileOptions.UseGlobalNamespace, OutputLanguage.JavaScript);

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(elms.PropertyNames.Any(p => p == "newName"));
        }
    }
}
