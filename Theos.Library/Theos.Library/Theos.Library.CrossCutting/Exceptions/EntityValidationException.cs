using System;
using System.Runtime.Serialization;

namespace Theos.Library.CrossCutting.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException()
        {
        }

        public EntityValidationException(string message) : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
