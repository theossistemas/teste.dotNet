using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Dtos
{
    public class UserInputDto
    {
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Username { get; set; }
    }
}