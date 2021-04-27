using livraria_api.Models;
namespace livraria_api.Models
{
    public class UsuarioAutenticadoModels
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}