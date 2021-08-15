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

        public void Adicionar(Autor Autor)
        {
            _context.Add(Autor);
        }

        public void Atualizar(Autor Autor)
        {
            _context.Update(Autor);
        }

        public async void Excluir(int id)
        {
            var Autor = await ObterPorId(id);

            if (Autor == null)
                throw new Exception("Autor não encontrado.");

            _context.Remove(Autor);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}