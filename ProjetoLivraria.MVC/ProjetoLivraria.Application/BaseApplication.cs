using ProjetoLivraria.Application.Interface;
using ProjetoLivraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLivraria.Application
{
    public class BaseApplication<TEntity> : IDisposable, IBaseApplication<TEntity> where TEntity : class
    {
        private readonly IBaseService<TEntity> _serviceBase;

        public BaseApplication(IBaseService<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return FindAll();
        }

        public TEntity FindGetById(int id)
        {
            return _serviceBase.FindById(id);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }
    }
}
