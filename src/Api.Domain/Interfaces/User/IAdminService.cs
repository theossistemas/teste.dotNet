using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.User
{
    public interface IAdminService
    {
        Task<bool> IsAdmin(Guid id);
    }
}
