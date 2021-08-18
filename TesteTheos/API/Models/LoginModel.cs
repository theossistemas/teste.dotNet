using System.ComponentModel.DataAnnotations;

namespace TesteTheos.API.Models
{
    public class LoginModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
