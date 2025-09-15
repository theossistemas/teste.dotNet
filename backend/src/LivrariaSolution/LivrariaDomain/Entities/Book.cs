using Livraria.Domain.ExcMsg;

namespace Livraria.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int PublicationYear { get; private set; }

        private Book() { }

        public static Book Create(string title, string author, int publicationYear)
        {
            ValidateBook(title, author, publicationYear);

            return new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                PublicationYear = publicationYear
            };
        }

        public void Update(string title, string author, int publicationYear)
        {
            ValidateBook(title, author, publicationYear);

            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }

        private static void ValidateBook(string title, string author, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException(string.Format(DomainExcMsg.EXC0001, "título"));
            if (string.IsNullOrWhiteSpace(author))
                throw new InvalidOperationException(string.Format(DomainExcMsg.EXC0001, "autor(a)"));
            if (publicationYear < 1 || publicationYear > DateTime.Now.Year)
                throw new InvalidOperationException(string.Format(DomainExcMsg.EXC0002));
        }
    }
}