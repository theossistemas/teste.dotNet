using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theos.Model.Model
{
   public class Livro
    {
        public int LivroId { get; set; }
        [StringLength(250)]
        public string NomeLivro { get; set; }
    }
}
