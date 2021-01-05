using Livraria.Domain.ManyToMany;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Pessoas
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<AutorLivro> Livros { get; set; }
    }
}
