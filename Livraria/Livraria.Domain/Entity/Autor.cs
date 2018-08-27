using System;

namespace Livraria.Domain.Entity
{
    public class Autor : EntidadeBase
    {
        public Autor(string nome)
        {
            Id = new Guid();
            Nome = nome;
        }

        public Autor(string id, string nome) : this(nome)
        {
            if (!string.IsNullOrEmpty(id))
                Id = new Guid(id);
        }

        protected Autor() { }

        public string Nome { get; private set; }
    }
}