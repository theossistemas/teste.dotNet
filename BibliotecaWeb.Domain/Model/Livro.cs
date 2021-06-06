using System;

namespace LivrariaWeb.Domain.Model
{
    public class Livro
    {
        public Livro()
        {

        }
        public Livro(string nomeLivro, string nomeAutor, DateTime dataPublicacao, int numeroPaginas)
        {
            NomeLivro = nomeLivro;
            NomeAutor = nomeAutor;
            DataPublicacao = dataPublicacao;
            NumeroPaginas = numeroPaginas;
        }
        public long Id { get; set; }
        public string NomeLivro { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int NumeroPaginas { get; set; }
    }

}