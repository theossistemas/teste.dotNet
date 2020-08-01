using Domain.Dtos.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LivroDto : BaseDto
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Emissora { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public DateTime DataLancamento { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public int Quantidade { get; set; }
    }    
}
