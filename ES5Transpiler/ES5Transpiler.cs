using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Transpilation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Transpilation.Exceptions;
using Transpilation.Interfaces;
using System.Text.RegularExpressions;
using Transpilation.ModelBuilders;
using Transpilation.Scaffold;

namespace Transpilation.Transpilers
{
    /// <summary>
    /// Converts a .Net class into a namespaced schema object with default values.
    /// </summary>
    public class ES5Transpiler : TranspilerBase
    {
        public ES5Transpiler(Type[] typesBesidesITranspilableToBeCompiled = null)
            : base(
                new ScaffoldingBuilder(), //Poor man's DI
                new JavaScriptModelBuilder(),
            OutputLanguage.JavaScript, 
            () => new Dictionary<string, string>
            {
                {"TIME_CREATED", DateTime.Now.ToLongDateString()}
            },
            typesBesidesITranspilableToBeCompiled
            )
        {
        }

    }
}
