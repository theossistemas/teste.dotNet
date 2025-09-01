
using System.ComponentModel.DataAnnotations;

namespace TheosLivraria.Domain.Models.Enum
{
    public enum RolesEnum
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "Public")]
        User = 2
    }
}
