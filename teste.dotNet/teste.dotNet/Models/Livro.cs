using System.ComponentModel.DataAnnotations;

namespace teste.dotNet.Models
{
    public class Livro
    {
        [Required]
        public int Id { get; set; }
        
        [MinLength(2)]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Required]
        public float Preco { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
