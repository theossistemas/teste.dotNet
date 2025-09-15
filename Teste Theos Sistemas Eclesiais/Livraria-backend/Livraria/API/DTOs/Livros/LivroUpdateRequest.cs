namespace Livraria.API.DTOs.Livros
{
    public class LivroUpdateRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public decimal Preco { get; set; }
    }
}
