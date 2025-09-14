namespace LivrariaTheos.Domain.Entities;

public class Livro
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int? AnoPublicacao { get; private set; }
    public string? ISBN { get; private set; }
    public string? Editora { get; private set; }
    public DateOnly DataPublicacao { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    private Livro() { } // EF Core

    public Livro(
        string titulo,
        string autor,
        int? anoPublicacao,
        string? isbn,
        string? editora,
        DateOnly dataPublicacao,
        decimal preco,
        int estoque)
    {
        Update(titulo, autor, anoPublicacao, isbn, editora, dataPublicacao, preco, estoque);
    }

    public void Update(
        string titulo,
        string autor,
        int? anoPublicacao,
        string? isbn,
        string? editora,
        DateOnly dataPublicacao,
        decimal preco,
        int estoque)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("O campo 'Título' é obrigatório");
        if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("O campo 'Autor' é obrigatório");
        if (anoPublicacao.HasValue && (anoPublicacao < 1450 || anoPublicacao > DateTime.UtcNow.Year))
            throw new ArgumentException("O campo 'Ano de publicação' é inválido");
        if (preco < 0) throw new ArgumentException("O campo 'Preço' não pode ser negativo");
        if (estoque < 0) throw new ArgumentException("O campo 'Estoque' não pode ser negativo");
        if (dataPublicacao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("O campo 'Data de publicação' não pode ser futura.");

        Titulo = titulo.Trim();
        Autor = autor.Trim();
        AnoPublicacao = anoPublicacao;
        ISBN = string.IsNullOrWhiteSpace(isbn) ? null : isbn.Trim();
        Editora = string.IsNullOrWhiteSpace(editora) ? null : editora.Trim();
        Preco = preco;
        Estoque = estoque;
    }

}