using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation
{
    /// <summary>
    /// The available tempalte variables
    /// </summary>
    public enum TemplateVariableTypes
    {
        /// <summary>
        /// Where the top-level namespace for all your compiled models will be inserted
        /// </summary>
        GLOBAL_NAMESPACE,
        /// <summary>
        /// Where the models should be inserted
        /// </summary>
        MODELS,
        /// <summary>
        /// Where model's namespace will be inserted
        /// </summary>
        NAMESPACE,
        /// <summary>
        /// Where the model's identifier will be inserted.
        /// </summary>
        MODELNAME,
        /// <summary>
        /// Where the model should be inserted
        /// </summary>
        MODEL
    }
}
