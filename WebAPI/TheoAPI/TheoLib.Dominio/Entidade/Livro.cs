using Dapper.Contrib.Extensions;
using System;

namespace TheoLib.Dominio.Entidade
{
    [Table("Livro")]
    public class Livro
    {
        [ExplicitKey]
        [Computed]
        public long Id { get; set; } 
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }   
        public long Estoque { get; set; }
        public bool Ativo { get; set; }
        public int IdUsuario { get; set; }
    }
}
