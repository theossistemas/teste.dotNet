namespace Gerenciador.Livraria.Core.Entities.Livraria
{
    public class LivroEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public int AutorId { get; set; }
        public AutorEntity? Autor { get; set; }
        public bool Ebook { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaEntity? Categoria { get; set; }
        public string? DataDePublicacao { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public bool Ativo { get; set; }
    }
}