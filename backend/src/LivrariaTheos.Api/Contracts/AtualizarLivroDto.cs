namespace LivrariaTheos.Api.Contracts
{
    // Para atualização (PUT) — espelha a entidade + Id como Guid
    public record AtualizarLivroDto(
        Guid Id,
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