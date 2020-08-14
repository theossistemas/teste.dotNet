using System;
using Entities.Base;

namespace Entities
{
    public class Livro : IEntity
    {
        public Int64? Id {get;set;}

        public String Titulo {get;set;}

        public String Descricao {get;set;}
    }
}