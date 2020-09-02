using Livraria.Data.Context;
using Livraria.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LivrariaContext _context;

        public UnitOfWork(LivrariaContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
