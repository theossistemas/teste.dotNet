using System.Collections.Generic;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.App.Models
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public List<AuthorDTO> Authors { get; set; }
        public int PageCount { get; set; }
        public PublisherDTO Publisher { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public string City { get; set; }
    }
}
