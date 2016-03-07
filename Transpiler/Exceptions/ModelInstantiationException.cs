using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilation.Exceptions
{
    [Serializable]
    public class ModelInstantiationException : Exception
    {
        public ModelInstantiationException(Type type) : base(string.Format("Could not create model object of type {0}", type.Name)) {
            Type = type;
        }
        public ModelInstantiationException(Type type, Exception inner)
            : base(string.Format("Could not create model object of type {0}", type.Name), inner)
        {
            Type = type;
        }
        protected ModelInstantiationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public Type Type { get; set; }
    }
}
