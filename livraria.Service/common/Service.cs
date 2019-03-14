using System.Collections.Generic;
using livraria.Domain.interfaces.common;
using livraria.Service.interfaces.common;

namespace livraria.Service.common
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Create(T obj)
        {
            _repository.Create(obj);
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        public virtual IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual void Update(T obj, int id)
        {
            _repository.Update(obj, id);
        }
    }
}
