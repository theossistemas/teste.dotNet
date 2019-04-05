using ProjetoLivraria.Domain.Interfaces.Repositories;
using ProjetoLivraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;


namespace ProjetoLivraria.Domain.Services
{
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return _repository.FindAll();
        }

        public TEntity FindById(int id)
        {
            return _repository.FindById(id);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public void Update(TEntity obj)
        {
             _repository.Update(obj);
        }
    }
}
