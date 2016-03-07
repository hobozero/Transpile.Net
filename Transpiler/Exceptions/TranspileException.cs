using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation.Exceptions
{
    [Serializable]
    public class TranspileException : Exception
    {
        public TranspileException()
        {
        }

        public TranspileException(string message) : base(message)
        {
        }

        public TranspileException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TranspileException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
