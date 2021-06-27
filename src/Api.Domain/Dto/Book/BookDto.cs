using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Publishing { get; set; }
        public string Language { get; set; }
        public DateTime Year { get; set; }

        public Guid IncludedBy { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
