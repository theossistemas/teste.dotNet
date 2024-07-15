namespace LivrariaWeb.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(string username, bool isAdmin);
    }
}
