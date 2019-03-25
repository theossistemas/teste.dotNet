using System.ComponentModel.DataAnnotations;

namespace Theos.Library.Api.Models.Login
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
