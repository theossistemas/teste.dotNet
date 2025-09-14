namespace LivrariaTheos.Domain.Entities
{
    // Para criação (POST)
    public record CriarLivroDto(
        string Titulo,
        string Autor,
        int? AnoPublicacao,
        string? ISBN,
        string? Editora,
        DateOnly DataPublicacao,
        decimal Preco,
        int Estoque
    );
}