using System;

namespace ProjetoLivraria.Domain.Entities
{
    public class Livro : Entity
    {
        public string Isbn { get; private set; }
        public string Autor { get; private set; }
        public string Titulo { get; private set; }
        public double Preco { get; private set; }
        public DateTime Publicacao { get; private set; }
        public string ImagemCapa { get; private set; }
    }
}
