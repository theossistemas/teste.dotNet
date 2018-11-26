using LC.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Persistence
{
    public class BaseRepository<TEntity> : IRepositoryGeneric<TEntity>
       where TEntity : BaseEntity
    {

        private readonly DataBaseContext _dbContext;

        private DbSet<TEntity> entities;

        public BaseRepository(DataBaseContext context)
        {
            _dbContext = context;
            entities = context.Set<TEntity>();
        }

        public TEntity Get(object[] key)
        {
            return entities.Find(key);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await entities.ToListAsync();
        }

        public IEnumerable<TEntity> Get(int pageSize, int page)
        {
            var pageIndex = page < 1 ? 0 : page - 1;
            return entities.AsNoTracking().Skip(pageIndex * page).Take(pageSize);
        }

        public bool Remove(TEntity entity)
        {
            var IsRemove = entities.Remove(entity);
            _dbContext.SaveChanges();
            return IsRemove != null;
        }

        public TEntity Save(TEntity entity)
        {
            var dbEntity = Get(entity.GetKey());

            if (dbEntity == null)
            {
                entities.Add(entity);
            }
            else
            {
                Update(entity);
            }

            _dbContext.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entities.Update(entity);

            return entity;
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public void Begin()
        {
            _dbContext.Database.BeginTransaction();
        }
    }
}
