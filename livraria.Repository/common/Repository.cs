using System.Collections.Generic;
using System.Linq;
using livraria.Context;
using livraria.Domain.interfaces.common;

namespace livraria.Repository.common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LivrariaContext _context;

        public Repository(LivrariaContext context)
        {
            _context = context;
        }

        public virtual void Create(T obj)
        {
            var c = _context.Add(obj);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var t = _context.Find<T>(id);
            _context.Remove<T>(t);
            _context.SaveChanges();
        }

        public virtual IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Find<T>(id);
        }

        public virtual void Update(T obj, int id)
        {
            var t = _context.Find<T>(id);
            _context.Entry(t).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }
    }
}
