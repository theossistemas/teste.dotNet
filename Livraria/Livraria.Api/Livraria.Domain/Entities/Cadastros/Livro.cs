using System.ComponentModel.DataAnnotations;

namespace Livraria.Domain.Entities.Cadastros
{
    public class Livro : BaseEntity
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Autor { get; set; }

        public Genero Genero { get; set; }
        public int GeneroId { get; set; }

    }
}
