namespace Livraria.API.DTOs.Login
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
    }
}
