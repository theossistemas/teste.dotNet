
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAll;

namespace TheosLivraria.Application.UseCases.Book.GetAll
{
    public interface IGetAllUseCase
    {
        Task<List<ResponseBookAllViewModel>> ExecuteAsync();
    }
}
