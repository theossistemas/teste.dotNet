using System.Collections.Generic;
using Livraria.Domain.Security.Entities;

namespace Livraria.Domain.Security.Interfaces
{
    public interface ISecurityBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        IList<TEntity> Select();
        TEntity Select(int id);
         
    }
}