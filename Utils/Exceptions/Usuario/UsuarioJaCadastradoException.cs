using System;
using System.Runtime.Serialization;

namespace Utils.Exceptions.Usuario
{
    public class UsuarioJaCadastradoException : Exception
    {
        public UsuarioJaCadastradoException()
        {
        }

        public UsuarioJaCadastradoException(string message) : base(message)
        {
        }

        public UsuarioJaCadastradoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsuarioJaCadastradoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
