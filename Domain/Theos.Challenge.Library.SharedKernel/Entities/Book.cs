using System.Collections.Generic;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.SharedKernel.Entities
{
    public class Book
    {
        public Book(string identifier, Cip cip, string author)
        {
            Identifier = identifier;
            Cip = cip;
            Author = author;
        }

        /// <summary>
        /// Identifier can be ISSN, ISBN, LCCN or Other
        /// </summary>
        /// <value></value>
        public string Identifier { get; private set; }   
        public Cip Cip { get; private set; }     
        public string Author {get; private set;}                
    }
}