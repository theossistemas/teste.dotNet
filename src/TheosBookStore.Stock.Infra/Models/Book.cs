using System.Collections.Generic;

namespace TheosBookStore.Stock.Infra.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public int PublisherId { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public string City { get; set; }
        public virtual PublisherModel Publisher { get; set; }
        public virtual ICollection<AuthorModel> Authors { get; set; }
    }
}
