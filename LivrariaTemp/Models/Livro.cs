using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTemp.Models
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
    }
}
