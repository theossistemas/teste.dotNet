namespace Livraria.API.DTOs.Livros
{
    public class    LivroResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        // Inclui informações da categoria associada
        public Guid CategoriaId { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
    }
}
    