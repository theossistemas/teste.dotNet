using LibraryStore.Core.Data.Entities;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> AutenticationAsync(string username, string password);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}