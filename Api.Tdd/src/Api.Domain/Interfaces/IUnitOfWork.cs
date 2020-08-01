using Api.Domain.Entities;
using Api.Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork<T> where T : BaseEntity
    {
        IRepository<T> Repository { get; }  
        Task<int> CommitAsync();
    }
}
