using Livraria.Domain;
using Livraria.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories
{
    public class LivroRepository : RepositoryBase<Livro>, ILivrosRepository
    {

        public async Task<IEnumerable<Livro>> BuscarTodosLivros()
        {
            return await _Context.Livros.ToListAsync();
        }

        public IEnumerable<Livro> BuscarPorNome(string Nome)
        {
            return _Context.Livros.Where(l => l.Nome == Nome);
        }
       
        public async Task Atualizar(Livro livro)
        {
            _Context.Update(livro);
            await _Context.SaveChangesAsync();
        }

        public async Task<Livro> BuscarPorId(int id)
        {
            var livro = await _Context.Livros.FirstOrDefaultAsync(x => x.Id == id);
            return livro;
        }

        public async Task Criar(Livro livro)
        {
            _Context.Livros.Add(livro);
            await _Context.SaveChangesAsync();
        }

        public async Task Excluir(Livro livro)
        {
            _Context.Remove(livro);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Livro>> ListarTodos()
        {
            return await _Context.Livros.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }

        Task<Livro> ILivrosRepository.BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
