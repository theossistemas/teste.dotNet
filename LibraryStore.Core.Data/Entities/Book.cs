using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(400)]
        public string UrlImage { get; set; }
    }
}