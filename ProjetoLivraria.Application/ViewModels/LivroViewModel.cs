using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLivraria.Application.ViewModels
{
    public class LivroViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ISBN obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Autor obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Autor")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Título obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Preço obrigatório")]
        [DisplayName("Preço")]
        public double Preco { get;  set; }

        [Required(ErrorMessage = "Data de publicação é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Publicação")]
        public DateTime Publicacao { get;  set; }

        public string ImagemCapa { get;  set; }
    }
}
