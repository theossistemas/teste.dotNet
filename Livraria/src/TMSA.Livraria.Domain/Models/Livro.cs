using System;

namespace TMSA.Livraria.Domain.Models
{
    public class Livro
    {
        public Guid LivroId { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int QuantidadeDePaginas { get; set; }
        public string NomeDoAutor { get; set; }
    }
}
