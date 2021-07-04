using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Test.RepositorioMock.Cadastros
{
    public class LivroRepositorioMock : ILivroRepositorio
    {

        private readonly List<Livro> _registros = new List<Livro>();

        public Task Create(Livro entity)
        {
            return new Task(() => { _registros.Add(entity); });
        }

        public Task DeleteAll()
        {
            return new Task(() => { _registros.RemoveRange(0, _registros.Count()); });
        }

        public Task Delete(int id)
        {
            var livro = _registros.Where(x => x.Id == id).FirstOrDefault();
            return new Task(() =>
            {
                _registros.Remove(livro);
            });
        }

        public Task Delete(Livro entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Livro> GetAll()
        {
            return _registros.AsQueryable();
        }

        public async Task<Livro> GetById(int id)
        {
            return await Task.FromResult(_registros.Where(x => x.Id == id).FirstOrDefault());
        }

        public Task<Livro> GetByTituloEGenero(string titulo, int generoId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Livro entity)
        {
            return new Task(() =>
            {
                var livro = _registros.Where(x => x.Id == entity.Id).First();
                livro.Autor = entity.Autor;
                livro.GeneroId = entity.GeneroId;
                livro.Titulo = entity.Titulo;
            });
        }
    }
}
