using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Test.RepositorioMock.Administracao
{
    public class LogRepositorioMock : ILogRepositorio
    {
        private readonly List<Log> _registros = new List<Log>();

        public Task Create(Log entity)
        {
            return new Task(() => { _registros.Add(entity); });
        }

        public Task DeleteAll()
        {
            return new Task(() => { _registros.RemoveRange(0, _registros.Count()); });
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Log entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Log> GetAll()
        {
            return _registros.AsQueryable();
        }

        public Task<Log> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Log entity)
        {
            throw new NotImplementedException();
        }
    }
}
