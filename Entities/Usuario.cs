using System;
using Entities.Base;
using Enumerators;

namespace Entities
{
    public class Usuario : IEntity
    {
        public Int64? Id {get;set;}

        public String Login {get;set;}

        public String Senha {get;set;}

        public Permissao Permissao {get;set;}
    }
}