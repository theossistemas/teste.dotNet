using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Infra.CrossCutting.Identity.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Required()]
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "The password you entered do not match"),
        Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
    }
}
