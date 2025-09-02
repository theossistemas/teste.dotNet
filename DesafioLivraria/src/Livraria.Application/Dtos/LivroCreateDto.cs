using System.ComponentModel.DataAnnotations;

namespace Livraria.Application.Dtos
{
    public class LivroCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O título não pode ser maior do que 200 caracteres.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do autor não pode ser maior do que 100 caracteres.")]
        public string Autor { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "O ano de publicação deve ser positivo.")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "O ISBN é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O ISBN não pode ser maior do que 20 caracteres.")]
        public string Isbn { get; set; } = null!;
    }
}
