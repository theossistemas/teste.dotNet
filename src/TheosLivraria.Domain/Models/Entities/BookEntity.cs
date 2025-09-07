
namespace TheosLivraria.Domain.Models.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
