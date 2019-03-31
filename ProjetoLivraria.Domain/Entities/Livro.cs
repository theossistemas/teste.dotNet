using System;

namespace ProjetoLivraria.Domain.Entities
{
    public class Livro : Entity
    {
        protected Livro() { }

        public Livro(Guid id, string isbn, string autor, string titulo, double preco, DateTime publicacao, string imagemCapa)
        {
            Id = Id;
            Isbn = isbn;
            Autor = autor;
            Titulo = titulo;
            Preco = preco;
            Publicacao = publicacao;
            ImagemCapa = imagemCapa;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        }

        public string Isbn { get; private set; }
        public string Autor { get; private set; }
        public string Titulo { get; private set; }
        public double Preco { get; private set; }
        public DateTime Publicacao { get; private set; }
        public string ImagemCapa { get; private set; }
    }
}
