using System.Collections.Generic;

namespace livraria.Domain.interfaces.common
{
    public interface IRepository<T>
    {
        void Create(T obj);
        void Update(T obj, int id);
        void Delete(int id);
        IList<T> GetAll();
        T GetById(int id);
    }
}
