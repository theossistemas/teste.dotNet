using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheosLivraria.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public int ISBN { get; set; }

        [Display(Name = "Nome do Livro")]
        public string NomeLivro { get; set; }

        [Display(Name = "Nome do Autor")]
        public string NomeAutor { get; set; }

        public string Editora { get; set; }

        [Display(Name = "Ano de Lançamento")]
        public int AnoLancamento { get; set; }

        [Display(Name = "Edição")]
        public byte Edicao { get; set; }
    }
}
