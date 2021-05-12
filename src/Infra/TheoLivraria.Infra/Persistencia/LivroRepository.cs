using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLivraria.Dominio.Entidades;
using TheoLivraria.Dominio.IRepositories;
using TheoLivraria.Infra.Contexto;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheoLivraria.Infra.Persistencia
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Atualizar(Livro livro)
        {
            _dataContext.Update(livro);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Livro> BuscarPorId(int id)
        {
            var livro = await _dataContext.Livros.FirstOrDefaultAsync(x => x.Id == id);
            return livro;
        }

        public async Task<Livro> BuscarPorNome(string nome)
        {
            var livro = await _dataContext.Livros.FirstOrDefaultAsync(x => x.Nome == nome);
            return livro;
        }

        public async Task Criar(Livro livro)
        {
            _dataContext.Livros.Add(livro);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Excluir(Livro livro)
        {
            _dataContext.Remove(livro);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Livro>> ListarTodos()
        {
            return await _dataContext.Livros.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
