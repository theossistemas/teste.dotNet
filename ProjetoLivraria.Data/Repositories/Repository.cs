using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjetoLivraria.Domain.Repositories.Interfaces;

namespace ProjetoLivraria.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ProjetoLivrariaContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ProjetoLivrariaContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
            return obj;
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public TEntity Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
            return obj;
        }

        public int Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            return Db.SaveChanges();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
