using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Dtos
{
    public class LoginInputDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}