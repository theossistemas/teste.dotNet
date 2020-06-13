using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Infra.CrossCutting.Identity.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
