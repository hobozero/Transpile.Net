using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation
{
    /// <summary>
    /// Bit flag capable transpile options.
    /// </summary>
    [Flags]
    public enum TranspileOptions
    {
        /// <summary>
        /// Models will be transpiled with in camelCase as opposed to the C# convention of PascalCase
        /// </summary>
        None = 0x01,
        CamelCaseModelNames = 0x02,
        UseGlobalNamespace = 0x04
    }
}
