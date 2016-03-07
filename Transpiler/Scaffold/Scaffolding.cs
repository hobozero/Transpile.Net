using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpilation;
using Transpilation.Exceptions;
using Transpilation.Interfaces;
using Transpilation;

namespace Transpilation.Scaffold
{
    public class Scaffolding
    {
        #region local
        private List<string> _propertyNames;
        Type _type;
        TranspileOptions _options;
        object _instance;
        private OutputLanguage _outputLangauge;
        #endregion

        public Scaffolding(Type type, TranspileOptions options, OutputLanguage outputLangauge)
        {
            _propertyNames = new List<string>();
            _type = type;
            _options = options;
            _outputLangauge = outputLangauge;
        }
        
        public string TypeName
        {
            get
            {
                return _options.HasFlag(TranspileOptions.CamelCaseModelNames) ?
                    _type.Name.ToCamelCase() :
                    _type.Name;
            }
        }

        public object CreateInstanceWithPropertyDefaults()
        {            
            if (_instance == null)
            {
                _instance = Activator.CreateInstance(_type);
                if (_instance != null)
                {
                    _instance.SetDefaults();
                }
                else
                {
                    throw new ModelInstantiationException(_type);
                }
            }
            return _instance;
        }

        public List<string> PropertyNames
        {
            get { return _propertyNames; }
            set { _propertyNames = value; }
        }

        public string Namespace
        {
            get
            {
                switch (_outputLangauge)
                {
                    case OutputLanguage.JavaScript:
                        return _options.HasFlag(TranspileOptions.CamelCaseModelNames) ?
                            string.Join(".",
                                    _type.Namespace.Split('.')
                                    .Select(n => n.ToCamelCase())) :
                            _type.Namespace;
                        break;
                    //Future refactoring.  May need to implement new rules here.
                    default:
                        return _options.HasFlag(TranspileOptions.CamelCaseModelNames) ?
                            string.Join(".",
                                    _type.Namespace.Split('.')
                                    .Select(n => n.ToCamelCase())) :
                            _type.Namespace;
                        break;
                }
            }
        }

    }
}
