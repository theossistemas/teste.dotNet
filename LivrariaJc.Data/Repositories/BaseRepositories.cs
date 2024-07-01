using LivrariaJc.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;
using LivrariaJc.Data.Context;
using LivrariaJc.Domain.Entidades;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace LivrariaJc.Data.Repository
{
    public class BaseRepositories<T> : IBaseRepositories<T> where T : BaseEntidade
    {
        protected readonly LivrariaJcContext _context;
        public BaseRepositories(LivrariaJcContext context)
        {
            _context = context;
        }
        public async Task<bool> ExcluirAsync(int id)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }

                _context.Remove(result);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InserirAsync(T item)
        {
            try
            {
                if (item.Id == 0)
                {
                    item.DataCriacao = DateTime.Now;
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public async Task<T> SelecionarAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public Task<IQueryable<T>> SelecionarTodosAsync()
        {
            try
            {
                return Task.FromResult(_context.Set<T>().AsQueryable());
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<T> AtualizarAsync(T item)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                {
                    return null;
                }

                item.DataAtualizacao = DateTime.Now;
                item.DataCriacao = result.DataCriacao;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public int UltimoId()
        {
            int? codigo = _context.Set<T>().OrderByDescending(s => s.Id).FirstOrDefault()?.Id;

            return codigo == null ? 1 : codigo.Value + 1;
        }
    }
}
