
using System;

namespace ProjetoLivraria.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
