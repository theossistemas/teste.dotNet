using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly MSSqlContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(MSSqlContext context){
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Insert(TEntity obj)
        {
            _dbSet.Add(obj);
            SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);            
            SaveChanges();
        }

        public void Delete(int id)
        {
             _dbSet.Remove(_dbSet.Find(id));
            SaveChanges();
        }

        public void SaveChanges(){
             _context.SaveChanges();
        }

        public IList<TEntity> Select() =>
           _dbSet.ToList();

        public TEntity Select(int id) =>
            _dbSet.Find(id);
    }
}