using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dto.Book
{
    public class BookDtoCreate
    {
        [Required(ErrorMessage = "Title is Required Field")]
        [StringLength(60, ErrorMessage = "Title must have a maximum of {1} characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is Required Field")]
        [StringLength(50, ErrorMessage = "Author must have a maximum of {1} characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Edition is Required Field")]
        [StringLength(50, ErrorMessage = "Edition must have a maximum of {1} characters")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "Publishing is Required Field")]
        [StringLength(60, ErrorMessage = "Publishing must have a maximum of {1} characters")]
        public string Publishing { get; set; }

        [Required(ErrorMessage = "Language is Required Field")]
        [StringLength(60, ErrorMessage = "Language must have a maximum of {1} characters")]
        public string Language { get; set; }

        public DateTime Year { get; set; }

        public Guid IncludedBy { get; set; }
    }
}
