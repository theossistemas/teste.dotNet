using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Dtos
{
    public class BookInputDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public string UrlImage { get; set; }
    }
}