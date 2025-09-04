using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Livros;
using TheosLivraria.Infra.Repository.Logs;
using TheosLivraria.Infra.Repository.Usuarios;

namespace TheosLivraria.Infra.Repository.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly TheosApplicationDbContext _context;
        protected IDbContextTransaction _transaction;


        private readonly Lazy<IUsuarioRepository> _lazyUsuarioRepository;
        private readonly Lazy<ILivroRepository> _lazyLivroRepository;
        private readonly Lazy<ILogRepository> _lazyLogRepository;

        public UnitOfWork(TheosApplicationDbContext context)
        {
            _context = context;
            _lazyUsuarioRepository = new Lazy<IUsuarioRepository>(() => new UsuarioRepository(context));
            _lazyLivroRepository = new Lazy<ILivroRepository>(() => new LivroRepository(_context));
            _lazyLogRepository = new Lazy<ILogRepository>(() => new LogRepository(_context));
        }

        public ILivroRepository LivroRepository => _lazyLivroRepository.Value;

        public IUsuarioRepository UsuarioRepository => _lazyUsuarioRepository.Value;

        public ILogRepository LogRepository => _lazyLogRepository.Value;

        public async Task BeginTransaction()
        {
            if(_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> Commit()
        {
            var result = await _context.SaveChangesAsync();
            if(_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            return result;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Rollback()
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
