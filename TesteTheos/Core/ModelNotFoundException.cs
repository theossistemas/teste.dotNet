using System;

namespace TesteTheos
{
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException() { }

        public ModelNotFoundException(string message)
            : base(message) { }
    }
}
