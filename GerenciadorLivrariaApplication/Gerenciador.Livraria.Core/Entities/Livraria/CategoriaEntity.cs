namespace Gerenciador.Livraria.Core.Entities.Livraria
{
    public class CategoriaEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public ICollection<LivroEntity> Livros { get; set; }
        public bool Ativo { get; set; }
    }
}
