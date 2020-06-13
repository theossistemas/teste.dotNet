using MMM.Library.Domain.Core.Models;
using System;

namespace MMM.Library.Domain.Models
{
    public class Book : Entity
    {
        public Book(Guid id, Guid categoryId, string title, int year, string language, string location)
        {
            Id = id;
            CategoryId = categoryId;
            Title = title;
            Year = year;
            Language = language;
            Location = location;
        }

        public Guid CategoryId { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }
        public string Location { get; private set; }

        // EF properties
        public Category Category { get; private set; }
        public Book() { }

        public void UpdateBook(Guid categoryId, string title, int year, string language, string location)
        {
            CategoryId = categoryId;
            Title = title;
            Year = year;
            Language = language;
            Location = location;
        }
                
    }
}
