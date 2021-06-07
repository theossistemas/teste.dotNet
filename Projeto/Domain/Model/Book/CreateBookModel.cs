using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Book
{
    public class CreateBookModel
    {
        [Required(ErrorMessage = "Book Title is required.")]
        [StringLength(50, ErrorMessage = "The max length of book title is 50 chars.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "The max length of genre is 50 chars.")]
        public string Genre { get; set; }
    }
}
