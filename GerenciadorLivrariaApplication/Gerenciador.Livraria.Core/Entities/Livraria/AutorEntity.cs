namespace Gerenciador.Livraria.Core.Entities.Livraria
{
    public class AutorEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<LivroEntity> Livros { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public bool Ativo { get; set; }
    }
}
