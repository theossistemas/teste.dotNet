namespace TheosLivraria.Application.UseCases.User.Auth.DTOs
{
    public class AuthUserOutput
    {
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
