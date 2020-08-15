using System;
using System.Runtime.Serialization;

namespace Utils.Exceptions.Usuario
{
    public class UsuarioSemPermissaoException : Exception
    {
        public UsuarioSemPermissaoException()
        {
        }

        public UsuarioSemPermissaoException(string message) : base(message)
        {
        }

        public UsuarioSemPermissaoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsuarioSemPermissaoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
