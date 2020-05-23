using System.Collections.Generic;

namespace TheosBookStore.Stock.Infra.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public int publisherid { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public string City { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}
