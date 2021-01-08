using BooksApi.Data;

namespace BooksApi.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
       
       
    }
}