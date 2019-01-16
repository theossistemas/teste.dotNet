using System.ComponentModel.DataAnnotations;

namespace livraria_backend.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int quantidade { get; set; }

    }
}