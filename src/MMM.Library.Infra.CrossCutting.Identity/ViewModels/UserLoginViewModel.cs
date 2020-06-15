using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Infra.CrossCutting.Identity.ViewModels
{
    public class UserLoginViewModel
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
    }
}
