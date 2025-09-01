using TheosLivraria.Application.UseCases.ViewModels.Request.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.Login;

namespace TheosLivraria.Application.UseCases.User.GetLogin
{
    public interface ILoginUseCase
    {
        Task<ResponseLoginViewModel?> ExecuteAsync(RequestLoginViewModel request);
    }
}
