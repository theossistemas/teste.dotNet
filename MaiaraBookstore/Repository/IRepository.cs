using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiaraBookstore.Repository
{
    public interface IRepository<T>
    {
        T FindById(int id);

        T FindByTitulo(string titulo);

        void Save(T objeto);
    }
}
