using System;

namespace Livraria.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
