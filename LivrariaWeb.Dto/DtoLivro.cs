using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LivrariaWeb.Dto
{
    public class DtoLivro
    {
        public long Id { get; set; }
        public string NomeLivro { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int NumeroPaginas { get; set; }
    }
}