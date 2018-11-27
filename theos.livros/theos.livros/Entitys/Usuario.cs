using System.ComponentModel.DataAnnotations;

namespace theos.livros.Entitys
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Acesso { get; set; }
    }
}
