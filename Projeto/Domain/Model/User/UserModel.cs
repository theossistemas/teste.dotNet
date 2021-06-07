using System.ComponentModel.DataAnnotations;

namespace Domain.Model.User
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username max length is 20 chars.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password max length is 20 chars.")]
        public string Password { get; set; }
    }
}
