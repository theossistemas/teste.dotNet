using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteDotNet.Business.Models;

namespace TesteDotNet.Business.Interfaces
{
    public interface ILivroService: IDisposable
    {
        Task<bool> Adicionar(Livro livro);
    }
}
