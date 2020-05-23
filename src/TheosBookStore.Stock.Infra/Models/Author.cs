using System.Collections.Generic;

namespace TheosBookStore.Stock.Infra.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookAuthor> Books { get; set; }
    }
}
