using System.Threading.Tasks;

namespace LivrariaTheos.Core.Data
{
    public interface IUnitOfWork 
    {
        Task<bool> Commit();
    }
}