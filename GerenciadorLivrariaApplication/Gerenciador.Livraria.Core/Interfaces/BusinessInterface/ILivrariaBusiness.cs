using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Interfaces.BusinessInterface
{
    public interface ILivrariaBusiness
    {
        Task CadastrarObra(LivroDTO livroDTO);
        Task<List<LivroDTO>> ListarObras();
        Task<bool> ExcluirObra(int id);
        Task AtualizarObra(LivroDTO livroDTO);
    }
}