public class Livro
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int AnoPublicacao { get; private set; }
    public string ISBN { get; private set; }
    public int NumeroPaginas { get; private set; }
    public string Editora { get; private set; }
    public Guid CategoriaId { get; private set; }
    public string Idioma { get; private set; }
    public decimal Preco { get; private set; }
    public string Sinopse { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public bool Disponivel { get; private set; }

    // Construtor vazio apenas para ORM
    public Livro() { }

    //Contrutor para servir o endpoint update. Em um app real, usaria Automapp para transformar Entidade Livro em LivroUpdateRequest
    public Livro(string titulo, string autor, int ano, decimal preco) 
    {
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = ano;
        Preco = preco;
    }

    // Construtor completo (ex: cadastro inicial)
    public Livro(string titulo, string autor, int ano, string isbn, int paginas, string editora, Guid categoriaId,
                 string idioma, decimal preco, string sinopse)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = ano;
        ISBN = isbn;
        NumeroPaginas = paginas;
        Editora = editora;
        CategoriaId = categoriaId;
        Idioma = idioma;
        Preco = preco;
        Sinopse = sinopse;
        DataCadastro = DateTime.UtcNow;
        Disponivel = true;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero.");
        Preco = novoPreco;
    }

    public void MarcarComoIndisponivel() => Disponivel = false;
}
