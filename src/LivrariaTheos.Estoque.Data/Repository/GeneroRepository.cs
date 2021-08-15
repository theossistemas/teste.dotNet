using LivrariaTheos.Core.Data;
using LivrariaTheos.Estoque.Domain.Generos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Data.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly EstoqueContext _context;

        public GeneroRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Genero> ObterPorId(int id)
        {
            return await _context.Generos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Genero>> ObterPorNome(string nome)
        {
            return await _context.Generos.AsNoTracking().Where(c => c.Nome.Equals(nome)).ToListAsync();
        }

        public async Task<IEnumerable<Genero>> ObterTodosAtivos()
        {
            return await _context.Generos.AsNoTracking().Where(c => c.Ativo).ToListAsync();
        }

        public async Task<IEnumerable<Genero>> ObterTodos()
        {
            return await _context.Generos.AsNoTracking().ToListAsync();
        }

        public void Adicionar(Genero genero)
        {
            _context.Add(genero);
        }

        public void Atualizar(Genero genero)
        {
            _context.Update(genero);
        }        

        public async void Excluir(Genero genero)
        {
            _context.Remove(genero);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}