using System.Threading.Tasks;
using BooksApi.Models;

namespace BooksApi.Repositories
{
    public interface IRepositoryUsuario : IRepository
    {
        Task<Usuario> GetUsuario(string username, string password);
        bool UsuarioExist(string email);
    }
}