using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Transpilation.ModelBuilders;
using Transpilation.Scaffold;

namespace Transpilation
{
    public abstract class TranspilerBase : ITranspiler
    {
        #region members
        protected static Regex RegexFormatHolder = new Regex(@"/\*\[#([^\s]+)]\*/");
        protected static Regex ValidFolderName = new Regex(@"^[a-zA-Z0-9_-]*$");
        protected OutputLanguage OutputLanguage;
        protected string ModelTemplateName;
        protected string MasterTemplateName;
        protected string AssemblyFolderContainingTemplates;
        protected Func<Dictionary<string, string>> CreateExtraPlaceHolderNamesAndValues;
        protected Assembly AssemblyContainingTemplates;
        protected Type[] ParentTypesToTranspile;
        protected IBuildScaffolds ScaffoldingStrategy;
        protected IBuildModels ModelTranspiler;

        private const string _defaultTemplateFolderName = "Templates";
        #endregion

        #region Construction
        protected TranspilerBase(IBuildScaffolds scaffoldingStrategy, IBuildModels modelTranspilationStrategy, OutputLanguage outputLanguage, Func<Dictionary<string, string>> createExtraPlaceHolderNamesAndValues = null, Type[] parentTypesToTranspile = null, string assemblyFolderContainingTemplates = null, string embeddedMasterFileName = null, string embeddedModelFileName = null)
        {
            ScaffoldingStrategy = scaffoldingStrategy;
            ModelTranspiler = modelTranspilationStrategy;

            CreateExtraPlaceHolderNamesAndValues = createExtraPlaceHolderNamesAndValues ?? (() => new Dictionary<string, string>());

            AssemblyFolderContainingTemplates = string.IsNullOrEmpty(assemblyFolderContainingTemplates)
                ? _defaultTemplateFolderName
                : assemblyFolderContainingTemplates;

            OutputLanguage = outputLanguage;
            ModelGlobalNamespace = string.Empty;

            MasterTemplateName = (string.IsNullOrEmpty(embeddedMasterFileName)) ? GetDefaultResourceFileName(EmbeddedTemplateType.MasterFileContainingModels) : embeddedMasterFileName;
            ModelTemplateName = (string.IsNullOrEmpty(embeddedModelFileName)) ? GetDefaultResourceFileName(EmbeddedTemplateType.ModelsToRenderInMasterTemplate) : embeddedModelFileName;

            AssemblyContainingTemplates = Assembly.GetCallingAssembly();

            ParentTypesToTranspile = new Type[] { typeof(ITranspilable) }
                .Concat(parentTypesToTranspile ?? new Type[0])
                .Distinct()
                .ToArray();
        }
        #endregion

        #region Publics
        public string ModelGlobalNamespace { get; set; }

        public virtual string GenerateCode(Assembly modelAssembly, TranspileOptions options)
        {
            if (!ValidFolderName.IsMatch(AssemblyFolderContainingTemplates))
                throw new Exception(string.Format("Assembly folder {0} is not a valid folder name", AssemblyFolderContainingTemplates));

            var masterTemplate = AssemblyContainingTemplates.LoadResource(AssemblyFolderContainingTemplates, MasterTemplateName);

            var listOfTranspiledModels = GenerateModels(modelAssembly, options);

            var templateVariablesToBeReplaced = new Dictionary<string, string>()
                {
                    {
                        TemplateVariableTypes.GLOBAL_NAMESPACE.ToString(),
                        options.HasFlag(TranspileOptions.UseGlobalNamespace) && !string.IsNullOrEmpty(ModelGlobalNamespace)
                            ? ModelGlobalNamespace
                            : string.Empty
                    },
                    {TemplateVariableTypes.MODELS.ToString(), listOfTranspiledModels}
                };
            var extraVariablesDict = CreateExtraPlaceHolderNamesAndValues();

            if (null != extraVariablesDict)
            {
                //concat existing and new variable dictionaries
                templateVariablesToBeReplaced = templateVariablesToBeReplaced
                        .Concat(extraVariablesDict.Where((kvp => !templateVariablesToBeReplaced.ContainsKey(kvp.Key))))
                        .ToDictionary( x => x.Key, x => x.Value);
            }

            return ReplacePlaceholders(masterTemplate, templateVariablesToBeReplaced);
        }
        #endregion

        #region Internals

        protected string GenerateModels(Assembly modelAssembly, TranspileOptions options)
        {
            var childTypesToTranspile = new List<Type>();
            foreach (var typeToTranspile in ParentTypesToTranspile)
            {
                childTypesToTranspile.AddRange(modelAssembly.GetChildTypes(typeToTranspile));  
            }
            childTypesToTranspile = childTypesToTranspile.Distinct().ToList();

            var modelTemplate = AssemblyContainingTemplates.LoadResource(AssemblyFolderContainingTemplates, ModelTemplateName);
            var extraVariablesDict = CreateExtraPlaceHolderNamesAndValues();

            var sbModels = new StringBuilder();
            foreach (Type typeToTranspile in childTypesToTranspile)
            {
                var scaffolding = ScaffoldingStrategy.Scaffold(typeToTranspile, options, OutputLanguage);

                var defaultModelInstance = scaffolding.CreateInstanceWithPropertyDefaults();

                var templateVariablesToBeReplaced = new Dictionary<string, string>()
                {
                    {TemplateVariableTypes.NAMESPACE.ToString(), scaffolding.Namespace},
                    {TemplateVariableTypes.MODELNAME.ToString(), scaffolding.TypeName},
                    {TemplateVariableTypes.MODEL.ToString(), ModelTranspiler.BuildModel(defaultModelInstance, options)}
                };

                if (null != extraVariablesDict)
                {
                    //concat existing and new variable dictionaries
                    templateVariablesToBeReplaced = templateVariablesToBeReplaced.Concat(extraVariablesDict.Where((kvp => !templateVariablesToBeReplaced.ContainsKey(kvp.Key))))
                            .ToDictionary(x => x.Key, x => x.Value);
                }

                sbModels.Append(
                    this.ReplacePlaceholders(modelTemplate, templateVariablesToBeReplaced)
                );
            }

            return sbModels.ToString();
        }

        protected string GetDefaultResourceFileName(EmbeddedTemplateType type)
        {
            return this.GetType().Name + "." + type.GetFileNameWithoutExtension() + "." + OutputLanguage.FileExtension();
        }

        protected internal string ReplacePlaceholders(string model, Dictionary<string, string> replacements)
        {
            if (replacements == null)
                return model;

            return RegexFormatHolder.Replace(model,
                m =>
                {
                    if (m != null && m.Groups.Count > 1 && replacements.ContainsKey(m.Groups[1].Value))
                    {
                        return replacements[m.Groups[1].Value];
                    }
                    Console.WriteLine(string.Format("Template variable found but not replaced: {0}", m.Groups[1].Value));
                    return m.Value;
                });
        }
        #endregion
    }
}
