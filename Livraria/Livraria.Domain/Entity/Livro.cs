using System;

namespace Livraria.Domain.Entity
{
    public class Livro : EntidadeBase
    {
        public Livro(string nome, string descricao, Autor autor, Editora editora, int? edicao)
        {
            Id = new Guid();
            Nome = nome;
            Descricao = descricao;
            Autor = autor;
            Editora = editora;
            Edicao = edicao;
        }

        protected Livro() { }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Autor Autor { get; set; }
        public Editora Editora { get; set; }
        public int? Edicao { get; set; }
    }
}
