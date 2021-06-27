using System;

namespace Api.Domain.Dto.Book
{
    public class BookDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Publishing { get; set; }
        public string Language { get; set; }
        public DateTime Year { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
