namespace TheosLivraria.Application.UseCases.Book.GetById.DTOs
{
    public class GetBookByIdOutput
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
