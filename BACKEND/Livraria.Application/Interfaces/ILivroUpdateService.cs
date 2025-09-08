using Livraria.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Service.Interfaces
{
    public interface ILivroUpdateService
    {
        //fazendo a alteração de acordo com o INTERFACE SEGREGATION PRINCIPLE (ISP)

        Task<LivroDTO?> UpdateLivroAsync(int id, LivroDTO livro);
    }
}
