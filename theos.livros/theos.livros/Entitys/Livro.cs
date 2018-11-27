using System.ComponentModel.DataAnnotations;

namespace theos.livros.Entitys
{
    public class Livro
    {
        [Key]
        public int IdLivro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
    }
}