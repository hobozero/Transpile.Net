using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelBuilder
{
    [Serializable]
    internal class MissingConsoleArgumentException : Exception
    {
        public MissingConsoleArgumentException()
        {
        }

        static string Message(string[] missingArguments)
        {
            return string.Format("You are missing the following console arguments",
                String.Join(", ", missingArguments));
        }
        public MissingConsoleArgumentException(string[] missingArguments)
            : base(Message(missingArguments))
        {
        }

        public MissingConsoleArgumentException(string[] missingArguments, Exception innerException)
            : base(Message(missingArguments), innerException)
        {
        }

        protected MissingConsoleArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    } 
}
