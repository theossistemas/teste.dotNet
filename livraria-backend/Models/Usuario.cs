using System.ComponentModel.DataAnnotations;

namespace livraria_backend.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }

    }
}