using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.App.Models
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator PublisherDTO(Publisher publisher)
        {
            if (publisher == null) return null;

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }
    }
}
