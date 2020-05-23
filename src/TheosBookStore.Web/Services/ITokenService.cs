using TheosBookStore.Auth.App.Models;

namespace TheosBookStore.Web.Services
{
    public interface ITokenService
    {
        string GenerateToken(AuthenticatedUser user);
    }
}
