using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Events
{
    public class BookEventAdded : Event
    {
        public Guid Id { get; protected set; }
        public Guid CategoryId { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }
        public string Location { get; private set; }

        public BookEventAdded(Guid id, Guid categoryId, string title, int year, string language, string location)
        {
            AggregateId = id;
            Id = id;
            CategoryId = categoryId;
            Title = title;
            Year = year;
            Language = language;
            Location = location;
        }
    }
}
