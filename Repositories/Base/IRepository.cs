using System;
using Entities.Base;
using System.Collections.Generic;

namespace Repositories.Base
{
    public interface IRepository<T> where T : IEntity
    {
         T Save(T t);

         T Find(Int64? id);

         void Delete(Int64? id);

         IList<T> FindAll();
    }
}