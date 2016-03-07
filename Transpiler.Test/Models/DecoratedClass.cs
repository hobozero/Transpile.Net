using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Transpilation.Test.Models
{
    internal class DecoratedClass : ICanBeTranspiled
    {
        [JsonIgnore]
        public string IgnoreProperty { get; set; }

        [JsonProperty(PropertyName = "NewName")]
        public string RenameProperty { get; set; }
    }
}
