using Gerenciador.Livraria.DTOs.DTOs.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Interfaces.BusinessInterface
{
    public interface IAutorBusiness
    {
        Task<AutorDTO> CadastrarNovoAutor(AutorDTO autorDTO);
        string PesquisarAutorPeloId(int id);
        Task<AutorDTO> AtualizarDadosDoAutor(AutorDTO autorDTO);
        Task<bool> ExcluirRegistroFisicoDoAutor(int id);
        Task<bool> ExcluirRegistroLogicoDoAutor(int id);
    }
}
