using System;
using System.Collections.Generic;

namespace TesteTheos
{
    public class BadRequestException : Exception
    {
        public Dictionary<string, IEnumerable<string>> ModelState { get; private set; }

        public BadRequestException()
            : base() { }

        public BadRequestException(string message)
            : base(message) { }

        public BadRequestException(string message, string error, string description)
            : this(message, new Dictionary<string, IEnumerable<string>> { { error, new string[] { description } } }) { }

        public BadRequestException(string error, string description)
            : this(new Dictionary<string, IEnumerable<string>> { { error, new string[] { description } } }) { }

        public BadRequestException(string message, Dictionary<string, IEnumerable<string>> models)
            : this(message)
            => ModelState = models;

        public BadRequestException(Dictionary<string, IEnumerable<string>> models)
            : this()
            => ModelState = models;
    }
}
