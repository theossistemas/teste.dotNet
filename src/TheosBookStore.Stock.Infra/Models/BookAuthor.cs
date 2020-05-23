namespace TheosBookStore.Stock.Infra.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public virtual BookModel Book { get; set; }

        public int AuthorId { get; set; }
        public virtual AuthorModel Author { get; set; }
    }
}
