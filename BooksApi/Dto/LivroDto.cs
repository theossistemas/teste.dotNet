using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dto
{
    public class LivroDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Titulo do livro não pode ser nulo.")]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "Titulo deve ter entre 6 a 80 caracteres")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "Isbn do livro não pode ser nulo.")]
        public string Isbn { get; set; }
        
        [Required(ErrorMessage = "Autor do livro não pode ser nulo.")]
        public string Autor { get; set; }
        
        [Range(25,1000, ErrorMessage = "Quantidade de paginas deve ser de 25 a 1000 ")]
        public int TotalPagina { get; set; }
        
        public bool Promocao { get; set; }
        
        public decimal Valor { get; set; }
        
        public decimal ValorPromocao { get; set; }
        
        public string ImagemUrl { get; set; }
        
        [StringLength(250, ErrorMessage = "O resumo deve ter no máximo 250 caracteres")]
        public string  Resumo { get; set; }
    }
}