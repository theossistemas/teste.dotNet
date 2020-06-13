using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo Título do Livro é obrigatório")]
        public string Title { get; set; }
        
        // Exemplo de validações
        // CQRS Fast Fail Validation recusará datas fora do intervalo entre 1800 e Ano Atual
        [Required(ErrorMessage = "Ano do Livro é obrigatório", AllowEmptyStrings = false)]
        public int Year { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

        public Guid CategoryId { get; set; }        

        //public CategoryViewModel Category { get; private set; }
    }
}