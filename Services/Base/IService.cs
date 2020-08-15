using System;
using System.Collections.Generic;

namespace Services.Base
{
    public interface IService<T> where T : class
    {
        T Save(T t);

        T Find(Int64? id);

        void Delete(Int64? id);

        IList<T> FindAll();
    }
}
