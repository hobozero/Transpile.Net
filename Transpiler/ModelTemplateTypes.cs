using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation
{
    public enum EmbeddedTemplateType
    {
        [EmbeddedTemplateFileNameWithoutExtension("Master")]
        MasterFileContainingModels,
        [EmbeddedTemplateFileNameWithoutExtension("Model")]
        ModelsToRenderInMasterTemplate
    }


    /// <summary>
    /// Indicated the file extension for the OutputLanguage.
    /// </summary>
    public class EmbeddedTemplateFileNameWithoutExtensionAttribute : Attribute
    {
        public string FileNameWithoutExtension { get; set; }

        public EmbeddedTemplateFileNameWithoutExtensionAttribute(string fileNameWithoutExtension)
        {
            this.FileNameWithoutExtension = fileNameWithoutExtension;
        }
    }

    internal static class ModelFileNameExtensions
    {
        public static string GetFileNameWithoutExtension(this EmbeddedTemplateType templateType)
        {
            var embeddedTemplateName = templateType.GetType()
                .GetField(templateType.ToString())
                .GetCustomAttributes(typeof(EmbeddedTemplateFileNameWithoutExtensionAttribute), false)
                .SingleOrDefault() as EmbeddedTemplateFileNameWithoutExtensionAttribute;

            return embeddedTemplateName.FileNameWithoutExtension ?? templateType.ToString();
        }
    }
}
