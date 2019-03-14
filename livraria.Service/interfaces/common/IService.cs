
using System.Collections.Generic;

namespace livraria.Service.interfaces.common
{
    public interface IService<T>
    {
        void Create(T obj);
        void Update(T obj, int id);
        void Delete(int id);
        IList<T> GetAll();
        T GetById(int id);
    }
}
