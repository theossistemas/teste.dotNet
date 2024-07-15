using System.ComponentModel.DataAnnotations;

namespace LivrariaWeb.Models
{
    public class LivroModel
    {
        public int Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Autor { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Preco { get; set; }
    }
}
