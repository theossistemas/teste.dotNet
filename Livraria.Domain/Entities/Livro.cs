
namespace Livraria.Domain.Entities
{
    public class Livro : BaseEntity
    {
       public string Autor { get; set; }
        public string Nome { get; set; }
        public string Editora { get; set; }
        public string Sinopse { get; set; }        
        public string Genero { get; set; }
    }
}