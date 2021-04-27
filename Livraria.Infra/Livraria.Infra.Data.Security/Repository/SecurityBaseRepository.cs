using Livraria.Domain.Security.Interfaces;
using Livraria.Infra.Data.Security.Contex;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Infra.Data.Security.Repository
{
    public abstract class SecurityBaseRepository<TEntity> : ISecurityBaseRepository<TEntity> where TEntity : class
    {
        protected readonly SecurityDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public SecurityBaseRepository(SecurityDbContext context){
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

        public IList<TEntity> Select() => _dbSet.ToList();

        public TEntity Select(int id) => _dbSet.Find(id);
    }
}