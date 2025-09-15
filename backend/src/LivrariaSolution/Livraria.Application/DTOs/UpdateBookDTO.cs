namespace Livraria.Application.DTOs
{
    public class UpdateBookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
    }
}
