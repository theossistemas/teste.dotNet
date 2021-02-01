using Dapper.Contrib.Extensions;
using System;

namespace Theos.Livraria.Domain.Entity
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
        public string Imagem { get; set; }
        public int Paginas { get; set; }
        public string Edicao { get; set; }
        public string Idioma { get; set; }
        public string Editora { get; set; }
        public DateTime DataPublicacao { get; set; }   
        public long Estoque { get; set; }
        public bool Ativo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
