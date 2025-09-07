
using System.ComponentModel.DataAnnotations;

namespace TheosLivraria.Domain.Models.Enums
{
    public enum RolesEnum
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2
    }
}
