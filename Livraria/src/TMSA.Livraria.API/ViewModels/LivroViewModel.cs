using System;
using System.ComponentModel.DataAnnotations;

namespace TMSA.Livraria.API.ViewModels
{
    public class LivroViewModel
    {
        public LivroViewModel()
        {
            LivroId = Guid.NewGuid();
        }

        [Key]
        public Guid LivroId { get; set; }
        [Required(ErrorMessage = "O campo ISBN é obrigatório")]
        [StringLength(13, ErrorMessage = "O campo ISBN precisa ter 13 caracteres", MinimumLength = 13)]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo título precisa ter entre 2 a 80 caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo gênero é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo gênero precisa ter entre 2 a 80 caracteres", MinimumLength = 2)]
        public string Genero { get; set; }
        [Required(ErrorMessage = "O campo quantidade de páginas é obrigatório")]
        public int QuantidadeDePaginas { get; set; }
        [Required(ErrorMessage = "O campo nome do autor é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo nome do autor precisa ter entre 2 a 80 caracteres", MinimumLength = 2)]
        public string NomeDoAutor { get; set; }
    }
}
