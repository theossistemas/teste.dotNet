using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaiaraBookstore.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Título do Livro obrigatório")]
        public String Titulo { get; set; }
    }
}
