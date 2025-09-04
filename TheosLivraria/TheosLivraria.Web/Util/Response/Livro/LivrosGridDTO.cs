using Radzen.Blazor.Rendering;

namespace TheosLivraria.Web.Util.Response.Livro
{
    public class LivrosGridDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Isbn { get; set; } 
        public decimal Preco { get; set; }
        public int Estoque { get; set; } 
    }
}
