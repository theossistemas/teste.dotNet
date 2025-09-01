
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.CreateAdmin;

namespace TheosLivraria.Application.UseCases.User.CreateAdmin
{
    public interface ICreateAdminUserCase
    {
        Task<ResponseCreateAdminViewModel?> ExecuteAsync(RequestCreateAdminViewModel request);
    }
}
