using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Domain.Interface
{
    public interface IUnitOfWork
    {
        ILivroRepository LivroRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        ILogRepository LogRepository { get; }
        Task<int> Commit();
        Task Rollback();
        Task BeginTransaction();
        void Dispose();

    }
}
