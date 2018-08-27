using System;

namespace Livraria.Domain.Entity
{
    public class Editora : EntidadeBase
    {
        public Editora(string nome)
        {
            Id = new Guid();
            Nome = nome;
        }

        public Editora(string id, string nome) : this(nome)
        {
            if (!string.IsNullOrEmpty(id))
                Id = new Guid(id);
        }

        protected Editora() { }

        public string Nome { get; set; }

    }
}