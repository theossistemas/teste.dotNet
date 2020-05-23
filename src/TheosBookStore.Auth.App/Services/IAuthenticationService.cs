using TheosBookStore.Auth.App.Models;
using TheosBookStore.LibCommon.Services;

namespace TheosBookStore.Auth.App.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        AuthenticatedUser AuthenticateBy(string email, string password);
    }
}
