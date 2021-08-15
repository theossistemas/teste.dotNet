using LivrariaTheos.Core.Data;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Autores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Data.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly EstoqueContext _context;

        public AutorRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Autor> ObterPorId(int id)
        {
            return await _context.Autores.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Autor>> ObterPorNome(string nome)
        {
            return await _context.Autores.AsNoTracking().Where(c => c.Nome.Equals(nome)).ToListAsync();
        }

        public async Task<IEnumerable<Autor>> ObterTodosAtivos()
        {
            return await _context.Autores.AsNoTracking().Where(c => c.Ativo).ToListAsync();
        }

        public async Task<IEnumerable<Autor>> ObterTodos()
        {
            return await _context.Autores.AsNoTracking().ToListAsync();
        }
      
        public async Task<IEnumerable<Autor>> ObterPorNacionalidade(int nacionalidadeAutor)
        {
            return await _context.Autores.AsNoTracking().Where(c => c.Nacionalidade == nacionalidadeAutor).ToListAsync();
        }

        public void Adicionar(Autor autor)
        {
            _context.Add(autor);
        }

        public void Atualizar(Autor autor)
        {
            _context.Update(autor);
        }

        public void Excluir(Autor autor)
        {
            _context.Remove(autor);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}