using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ExceptionHandlingFirstCharOfString.Exceptions
{
    public class NoElementsException : Exception
    {
        public string TypeElements { get; }
        public NoElementsException()
        {
            TypeElements = "";
        }

        public NoElementsException(string message,string typeElements="") : base(message)
        {
            TypeElements = typeElements;
        }

        public NoElementsException(string message, Exception innerException, string typeElements = "") : base(message, innerException)
        {
            TypeElements = typeElements;
        }

        protected NoElementsException(SerializationInfo info, StreamingContext context, string typeElements = "") : base(info, context)
        {
            TypeElements = typeElements;
        }
    }
}
