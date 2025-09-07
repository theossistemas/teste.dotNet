
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Infrastructure.Data;

namespace TheosLivraria.Infrastructure.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
