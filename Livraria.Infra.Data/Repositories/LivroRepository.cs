using Livraria.Domain;
using Livraria.Domain.Interfaces;
using Livraria.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : ILivrosRepository
    {
        public readonly Context _Context;

        public LivroRepository(Context context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Livro>> BuscarTodosLivros()
        {
            return await _Context.Livros.ToListAsync();
        }

        async Task ILivrosRepository.Criar(Livro livro)
        {
            _Context.Livros.Add(livro);
            await _Context.SaveChangesAsync();
        }

        async Task ILivrosRepository.Atualizar(Livro livro)
        {
            _Context.Update(livro);
            await _Context.SaveChangesAsync();
        }

        async Task ILivrosRepository.Excluir(Livro livro)
        {
            _Context.Remove(livro);
            await _Context.SaveChangesAsync();
        }

        Task<Livro> ILivrosRepository .BuscarPorId(int id)
        {
            var livro = _Context.Livros.FirstOrDefaultAsync(x => x.Id == id);
            return livro;
        }

        async Task<Livro> ILivrosRepository.BuscarPorNome(string nome)
        {
            var livro = await _Context.Livros.FirstOrDefaultAsync(x => x.Nome == nome);
            return livro;
        }

        async Task<IEnumerable<Livro>> ILivrosRepository.ListarTodos()
        {
            return await _Context.Livros.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }
        void IRepositoryBase<Livro>.Dispose()
        {
            throw new NotImplementedException();
        }


        public void Add(Livro obj)
        {
            throw new NotImplementedException();
        }

        public Livro GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Livro> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Livro obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Livro obj)
        {
            throw new NotImplementedException();
        }
    }
}
