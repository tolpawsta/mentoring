using System;

namespace Reflection.DIContainer.Exceptions
{
    public class DiException:Exception
    {
        private Type _invalidType;
        public Type InvalidType => _invalidType;
        public DiException(string? message,Type? invalidType=null):base(message)
        {
            _invalidType = invalidType;
        }
        public DiException(string? message, Exception? innerException,Type? invalidType=null)
            : base(message, innerException)
        {
            _invalidType = invalidType;
        }
    }
}