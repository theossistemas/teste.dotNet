using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteDotNet.Business.Models;

namespace TesteDotNet.Business.Interfaces
{
    public interface ILivroRepository: IRepository<Livro>
    {
        Task<IEnumerable<Livro>> ListarPorNome();
    }
}
