using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Queries.Livros
{
    public class LivroGridDTO(Guid id, string titulo, string autor, string isbn, decimal preco, int estoque)
    {
        public Guid Id { get; set; } = id;
        public string Titulo { get; set; } = titulo;
        public string Autor { get; set; } = autor;
        public string Isbn { get; set; } = isbn;
        public decimal Preco { get; set; } = preco;
        public int Estoque { get; set; } = estoque;
    }
}
