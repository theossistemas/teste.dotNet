namespace TheosLivraria.Web.Request.Livros
{
    public class AlterarLivroRequest
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Isbn { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
}
