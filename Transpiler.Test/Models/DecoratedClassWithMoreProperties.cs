using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Transpilation.Test.Models
{
    internal class DecoratedClassWithMoreProperties : ICanBeTranspiled
    {
        [JsonIgnore]
        public string IgnoreProperty { get; set; }

        [DefaultValue(99.9)]
        public double FloatProperty { get; set; }

        [DefaultValue("defaultValue")]
        public string StringProperty { get; set; }

        public List<string> StringArrayProperty { get; set; }
    }
}
