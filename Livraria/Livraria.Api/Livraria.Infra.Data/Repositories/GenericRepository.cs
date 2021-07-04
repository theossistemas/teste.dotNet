using Livraria.Domain.Entities;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories
{
    public class GenericRepository<TEntity> : BaseEntity, IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly LivrariaDataContext _dataContext;

        public GenericRepository(LivrariaDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public LivrariaDataContext GetDataContext()
        {
            return _dataContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dataContext.Set<TEntity>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dataContext.Set<TEntity>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dataContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dataContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(TEntity entity)
        {
            _dataContext.Set<TEntity>().Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
