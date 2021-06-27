using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required field for login")]
        [EmailAddress(ErrorMessage = "Invalid format email.")]
        [StringLength(100, ErrorMessage = "Email must be a maximum of {1} characters.")]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
