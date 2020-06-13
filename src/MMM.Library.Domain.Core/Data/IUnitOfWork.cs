using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}