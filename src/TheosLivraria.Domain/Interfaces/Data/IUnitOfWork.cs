
namespace TheosLivraria.Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
