using Livraria.Domain.ManyToMany;

using System.Collections.Generic;

namespace Livraria.Domain.Livros
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Setor { get; set; }

        public virtual ICollection<LivroTema> Temas { get; set; }
        public virtual ICollection<AutorLivro> Autores { get; set; }
    }
}
