using System.Collections.Generic;

namespace Livraria.Domain.Entidades
{
    public class Autor : Entity<Autor>
    {
        public string Nome { get; set; }
        public virtual ICollection<Livro> Livros { get; private set; }

        public override bool Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
