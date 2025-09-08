using Livraria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infrastructure.Interfaces
{
    public interface ILivroCreateRepository
    {
        //fazendo a alteração de acordo com o INTERFACE SEGREGATION PRINCIPLE (ISP)
        Task<Livro> AddAsync(Livro livro);
    }
}
