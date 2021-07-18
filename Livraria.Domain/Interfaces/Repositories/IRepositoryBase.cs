using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repositories
{
   public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public void Add(TEntity obj);

        public TEntity GetById(int id);

        public IEnumerable<TEntity> GetAll();

        public void Update(TEntity obj);

        public void Remove(TEntity obj);

        public void Dispose();

    }
}
