using Livraria.Service.DTOs;
using System.Collections.Generic;

namespace Livraria.Service.Interfaces
{
    public interface ILivroService
    {
        void Incluir(LivroDTO livroDTO);
        void Alterar(LivroDTO livroDTO);
        void Excluir(string id);
        LivroDTO Consultar(string id);
        IList<LivroDTO> ListarOrdenadoPorNome();
        IList<LivroDTO> Listar();
    }
}
