using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Domain.CQRS.Queries.ViewModels
{
    public class BookAndCategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required()]
        public string Title { get; set; }
        [Required()]
        public int Year { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

        public CategoryViewModelQueries Category { get; set; }
    }
}