using Livraria.Domain.ManyToMany;

using System.Collections.Generic;

namespace Livraria.Domain.Livros
{
    public class Tema
    {
        public int Id { get; set; }
        public string Valor { get; set; }

        public virtual ICollection<LivroTema> Livros { get; set; }
    }
}