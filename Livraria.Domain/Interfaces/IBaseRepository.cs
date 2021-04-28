using System.Collections.Generic;
namespace Livraria.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        IList<TEntity> Select();
        TEntity Select(int id);
         
    }
}