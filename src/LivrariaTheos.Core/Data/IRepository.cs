using LivrariaTheos.Core.DomainObjects;
using System;

namespace LivrariaTheos.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}