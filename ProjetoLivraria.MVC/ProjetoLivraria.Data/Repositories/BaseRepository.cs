using ProjetoLivraria.Data.Context;
using ProjetoLivraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLivraria.Data.Repositories
{
    public class BaseRepository<TEntity> :  IBaseRepository<TEntity> where TEntity : class
    {
        protected LivrariaContext Db = new LivrariaContext();


        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public TEntity FindById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
