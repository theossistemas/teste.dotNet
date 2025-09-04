using System;

namespace TheosLivraria.Domain.Entidades
{
    public class Livro : EntidadeBase<int>
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Isbn { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public decimal Preco { get; private set; }
        public int Estoque { get; private set; }

        
        protected Livro() { }

        
        public Livro(string titulo, string autor, string isbn, DateTime dataPublicacao, decimal preco, int estoque)
        {
            Validar(titulo, autor, isbn, dataPublicacao, preco, estoque);
            Titulo = titulo;
            Autor = autor;
            Isbn = isbn;
            DataPublicacao = dataPublicacao;
            Preco = preco;
            Estoque = estoque;
        }

        public void AlterarDados(string titulo, string autor, string isbn, DateTime dataPublicacao, decimal preco, int estoque)
        {
            Validar(titulo, autor, isbn, dataPublicacao, preco, estoque);
            Titulo = titulo;
            Autor = autor;
            Isbn = isbn;
            DataPublicacao = dataPublicacao;
            Preco = preco;
            Estoque = estoque;
            DataAtualizacao = DateTime.UtcNow;
        }

        private static void Validar(string titulo, string autor, string isbn, DateTime dataPublicacao, decimal preco, int estoque)
        {
            if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 2)
                throw new ArgumentException("Título deve ter pelo menos 2 caracteres.");
            if (string.IsNullOrWhiteSpace(autor) || autor.Length < 2)
                throw new ArgumentException("Autor deve ter pelo menos 2 caracteres.");
            if (string.IsNullOrWhiteSpace(isbn) || isbn.Length != 13 || !isbn.All(char.IsDigit))
                throw new ArgumentException("ISBN deve ter exatamente 13 dígitos numéricos.");
            if (dataPublicacao > DateTime.UtcNow)
                throw new ArgumentException("Data de publicação não pode ser no futuro.");
            if (preco < 0)
                throw new ArgumentException("Preço não pode ser negativo.");
            if (estoque < 0)
                throw new ArgumentException("Estoque não pode ser negativo.");
        }
    }
}