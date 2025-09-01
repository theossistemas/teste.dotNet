
namespace TheosLivraria.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

    }
}
