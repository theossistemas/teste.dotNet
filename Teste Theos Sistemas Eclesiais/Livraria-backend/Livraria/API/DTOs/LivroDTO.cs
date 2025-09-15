namespace Livraria.Api.DTOs
{
    public class LivroDTO
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; }
        public int NumeroPaginas { get; set; }
        public string Editora { get; set; }
        public Guid CategoriaId { get; set; }
        public string Idioma { get; set; }
        public decimal Preco { get; set; }
        public string Sinopse { get; set; }
    }
}
