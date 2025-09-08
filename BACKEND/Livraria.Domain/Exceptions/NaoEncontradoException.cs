using System;

namespace Livraria.Domain.Exceptions
{
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException() : base() { }
        public NaoEncontradoException(string message) : base(message) { }
    }
}