using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Models
{
    public class Livros
    {
      public int id { get; set; }
      [Required(ErrorMessage = "Campo obrigatório!")]
      [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo descrição deve conter de 3 a 50 caracteres")]
      [Display(Name = "Descrição")]
      public string Descricao { get; set; }
      [DataType(DataType.Currency)]
      public decimal valor { get; set; }
      public Categoria Categoria { get; set; }
   }
}
