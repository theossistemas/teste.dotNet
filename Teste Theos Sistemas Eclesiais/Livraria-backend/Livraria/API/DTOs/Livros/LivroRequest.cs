namespace Livraria.API.DTOs.Livros
{
    public class LivroRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int NumeroPaginas { get; set; }
        public string Editora { get; set; }
        public Guid CategoriaId { get; set; }
        public string Idioma { get; set; }
        public decimal Preco { get; set; }
        public string Sinopse { get; set; }
    }
}