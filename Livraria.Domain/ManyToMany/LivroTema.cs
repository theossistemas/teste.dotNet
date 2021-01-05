using Livraria.Domain.Livros;

namespace Livraria.Domain.ManyToMany
{
    public class LivroTema
    {
        public int IdLivro { get; set; }
        public int IdTema { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Tema Tema { get; set; }
    }
}
