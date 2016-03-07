using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation.Test.Models
{
    public class DecimalClass : ICanBeTranspiled
    {
        /// <summary>
        /// A floating point (8.0) cannot be converted to a decimal.  Will throw an InvalidCastException when attempting to set.
        /// </summary>
        [DefaultValue(8.0f)]
        public Decimal FakeDecimal { get; set; }
    }
}
