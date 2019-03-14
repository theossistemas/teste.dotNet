using System.Collections.Generic;

namespace livraria.Application.interfaces.common
{
    public interface IApplication<T>
    {
        void Create(T obj);
        void Update(T obj, int id);
        void Delete(int id);
        IList<T> GetAll();
        T GetById(int id);
    }
}
