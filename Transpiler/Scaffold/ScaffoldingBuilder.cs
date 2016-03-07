using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Transpilation.Scaffold
{
    public class ScaffoldingBuilder: IBuildScaffolds
    {
        public Scaffolding Scaffold(Type type, TranspileOptions options, OutputLanguage outputLangauge)
        {
            var elm = new Scaffolding(type, options, outputLangauge);
            var jsonProp = typeof(JsonPropertyAttribute);

            var propertyInfo = type.GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)));

            elm.PropertyNames = propertyInfo.Select(pi => Attribute.IsDefined(pi, jsonProp) ?
                    ((JsonPropertyAttribute)pi.GetCustomAttributes(true).ToDictionary(a => a.GetType(), a => a)[jsonProp]).PropertyName
                    : pi.Name)
                    .ToList();

            if (options.HasFlag(TranspileOptions.CamelCaseModelNames))
            {
                elm.PropertyNames = elm.PropertyNames.Select(p => p.ToCamelCase()).ToList();
            }
            
            return elm;
        }
    }
}
