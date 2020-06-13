using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Domain.CQRS.Queries.ViewModels
{
    public class CategoryViewModelQueries
    {
        [Key]
        public Guid Id { get; set; }

        [Required()]
        public string CategoryName { get; set; }

        [Required()]
        public int Code { get; set; }
    }
}
