using LibraryStore.Core.Data.Entities;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> ExistsByTitleAsync(string title);
    }
}