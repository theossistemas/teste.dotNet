using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Service.Interfaces
{
    public interface ILivroDeleteService
    {
        //fazendo a alteração de acordo com o INTERFACE SEGREGATION PRINCIPLE (ISP)

        Task<bool> DeleteLivroAsync(int id);
    }
}
