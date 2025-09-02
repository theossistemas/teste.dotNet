namespace Livraria.Domain.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public int AnoPublicacao { get; set; }
        public string Isbn { get; set; } = "";

        public Livro() { }

        public Livro(string titulo, string autor, int anoPublicacao, string isbn)
        {
            Titulo = titulo;
            Autor = autor;
            AnoPublicacao = anoPublicacao;
            Isbn = isbn;
        }
    }
}
