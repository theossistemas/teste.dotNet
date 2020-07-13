using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

    }
}