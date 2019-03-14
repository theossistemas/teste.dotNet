using System.Collections.Generic;
using livraria.Application.interfaces.common;
using livraria.Service.interfaces.common;

namespace livraria.Application.common
{
    public class Application<T> : IApplication<T> where T : class
    {
        private readonly IService<T> _service;

        public Application(IService<T> service)
        {
            _service = service;
        }

        public void Create(T obj)
        {
            _service.Create(obj);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }

        public IList<T> GetAll()
        {
            return _service.GetAll();
        }

        public T GetById(int id)
        {
            return _service.GetById(id);
        }

        public void Update(T obj, int id)
        {
            _service.Update(obj, id);
        }
    }
}
