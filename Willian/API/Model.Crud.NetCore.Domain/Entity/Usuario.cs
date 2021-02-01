using Dapper.Contrib.Extensions;
using System;

namespace Theos.Livraria.Domain.Entity
{
    [Table("Usuario")]
    public class Usuario
    {
        [ExplicitKey]
        [Computed]
        public long Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }   
    }
}
