using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int TotalPagina { get; set; }
        public string Isbn { get; set; }
        public string Autor { get; set; }
        public bool Promocao { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPromocao { get; set; }
        public string ImagemUrl { get; set; }
        public string Resumo { get; set; }

    }
}