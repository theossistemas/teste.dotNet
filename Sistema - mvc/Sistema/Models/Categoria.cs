using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Models
{
   public class Categoria
   {
      public int CategoriaID { get; set; }
      [Required (ErrorMessage = "Campo obrigatório!")]
      [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo descrição deve conter de 3 a 50 caracteres")]
      [Display(Name ="Descrição")]
      public string Descricao { get; set; }
      public IEnumerable<Livros> Livros { get; set;}
    }
}
