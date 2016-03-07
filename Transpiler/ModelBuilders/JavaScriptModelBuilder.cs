using System;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Transpilation.Exceptions;

namespace Transpilation.ModelBuilders
{
    public class JavaScriptModelBuilder:IBuildModels
    {
        public string BuildModel(object instance, TranspileOptions options)
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            if (options.HasFlag(TranspileOptions.CamelCaseModelNames))
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (instance.GetType().GetConstructor(Type.EmptyTypes) == null)
            {
                throw new TranspileException("Type must have a parameterless constructor");
            }

            return JsonConvert.SerializeObject(instance, settings);
        }
    }
}
