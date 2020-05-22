using System.Collections.Generic;
using System.Linq;

using TheosBookStore.LibCommon.Entities;
using TheosBookStore.Stock.Domain.Validations;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; protected set; }
        public ISBN ISBN { get; protected set; }
        public int PageCount { get; protected set; }
        public Publisher Publisher { get; protected set; }
        public int YearPublication { get; protected set; }
        public int Edition { get; protected set; }
        public string City { get; protected set; }
        public IEnumerable<Author> Authors { get => _authors; }
        private readonly IList<Author> _authors;
        public Book()
        {
            _authors = new List<Author>();
        }
        public Book(string title, ISBN isbn, ICollection<Author> authors,
            int pageCount, Publisher publisher, int yearPublication, int edition,
            string city) : this()
        {
            Title = title;
            ISBN = isbn;
            PageCount = pageCount;
            Publisher = publisher;
            YearPublication = yearPublication;
            Edition = edition;
            City = city;

            if (authors != null)
                authors.ToList().ForEach(author => _authors.Add(author));
        }

        public void AddAuthor(Author newAuthor)
        {
            _authors.Add(newAuthor);
        }

        public override bool IsValid()
        {
            _validationResult = new BookValidations().Validate(this);
            return _validationResult.IsValid;
        }
    }
}
