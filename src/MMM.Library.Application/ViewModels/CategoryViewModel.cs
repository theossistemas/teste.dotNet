using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Application.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome da Categoria é obrigatório")]
        public string CategoryName { get; set; }

        public int Code { get; set; }
    }
}
