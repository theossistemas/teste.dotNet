using MMM.Library.Infra.CrossCutting.Identity.ViewModels;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Identity.Services
{
    public interface IIdentityService
    {
        Task<bool> Login(UserLoginViewModel loginUser);

        Task<bool> NewUser(UserRegistrationViewModel newUser);

        Task<LoginResponseViewModel> JwtGenerate(string email);

    }
}
