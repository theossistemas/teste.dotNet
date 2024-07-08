using Gerenciador.Livraria.DTOs.DTOs.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Interfaces.BusinessInterface
{
    public interface ICategoriaBusiness
    {
        Task<CategoriaDTO> CadastrarNovaCategoria(CategoriaDTO categoriaDTO);
        Task<List<CategoriaDTO>> ListarCategorias();
        string PesquisarCategoriaPeloId(int id);
        Task<CategoriaDTO> AtualizarCategoria(CategoriaDTO categoriaDTO);
        Task<bool> ExcluirCategoria(int id);
    }
}
