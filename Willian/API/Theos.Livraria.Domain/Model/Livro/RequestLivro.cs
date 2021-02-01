using System;

namespace Theos.Livraria.Domain.Model.Livro
{
    public class RequestLivro
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public string Imagem { get; set; }
        public int Paginas { get; set; }
        public string Edicao { get; set; }
        public string Idioma { get; set; }
        public string Editora { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
        public int IdUsuario { get; set; } 
    }
}
