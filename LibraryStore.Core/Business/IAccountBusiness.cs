using LibraryStore.Core.Data.Dtos;

namespace LibraryStore.Core.Business
{
    public interface IAccountBusiness
    {
        System.Threading.Tasks.Task<dynamic> Authenticate(LoginInputDto dto);
    }
}