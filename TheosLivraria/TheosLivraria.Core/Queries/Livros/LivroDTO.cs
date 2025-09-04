using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Queries.Livros
{
   public class LivroDTO(Guid Id, string titulo, string autor, string isbn, DateTime dataPublicacao, decimal preco, int estoque)
    {
        public Guid Id { get; set; } = Id;
        public string Titulo { get; set; } = titulo;
        public string Autor { get; set; } = autor;
        public string Isbn { get; set; } = isbn;
        public DateTime DataPublicacao { get; set; } = dataPublicacao;
        public decimal Preco { get; set; } = preco;
        public int Estoque { get; set; } = estoque;
    }
}
