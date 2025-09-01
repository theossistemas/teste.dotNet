
using System.ComponentModel.DataAnnotations;

namespace TheosLivraria.Domain.Models.Enum
{
    public enum StatusEnum
    {
        [Display(Name = "Disponivel")]
        Available = 1,

        [Display(Name = "Emprestado")]
        Rented = 2,

        [Display(Name = "Reservado")]
        Reserved = 3,
    }
}
