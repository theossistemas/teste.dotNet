
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.INFRA.Context;

namespace TheosLivraria.INFRA.UnitOfWork
{
    public class UnitOfWork(TheosLivrariaDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
