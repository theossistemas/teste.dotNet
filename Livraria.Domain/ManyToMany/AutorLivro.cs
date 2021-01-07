using Livraria.Domain.Livros;
using Livraria.Domain.Pessoas;

namespace Livraria.Domain.ManyToMany
{
    public class AutorLivro
    {
        public int IdAutor { get; set; }
        public int IdLivro { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Pessoa Autor { get; set; }
    }
}
