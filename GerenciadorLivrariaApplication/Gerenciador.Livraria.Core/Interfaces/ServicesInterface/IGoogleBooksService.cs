using Gerenciador.Livraria.DTOs.DTOs.GoogleBooks;
using Gerenciador.Livraria.DTOs.DTOs.Helpers;

namespace Gerenciador.Livraria.Core.Interfaces.ServicesInterface
{
    public interface IGoogleBooksService
    {
        Task<HttpHelperResponseDTO> BuscarObraPeloTitulo(string titulo);
        Task<HttpHelperResponseDTO> BuscarObrasDoAutor(string autor);
        Task<HttpHelperResponseDTO> BuscarObrasPorCategoria(string categoria);
    }
}