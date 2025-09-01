using TheosLivraria.Application.UseCases.ViewModels.Common.Response;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;

namespace TheosLivraria.Domain.Interfaces.UseCases
{
    public interface IGetAllPagedUseCase
    {
        Task<ResponsePagedViewModel<ResponseBookAllPagedViewModel>> ExecuteAsync(RequestBookAllPagedViewModel request);
    }
}
