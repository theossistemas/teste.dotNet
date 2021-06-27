using System.Threading.Tasks;
using Api.Domain.Dto;

namespace Api.Domain.Interfaces.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);
    }
}
