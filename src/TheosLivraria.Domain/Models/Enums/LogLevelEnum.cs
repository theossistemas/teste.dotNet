
using System.ComponentModel.DataAnnotations;

namespace TheosLivraria.Domain.Models.Enums
{
    public enum LogLevelEnum
    {
        [Display(Name = "Info")]
        Info = 1,
        [Display(Name = "Warning")]
        Warning = 2,
        [Display(Name = "Error")]
        Error = 3
    }
}
