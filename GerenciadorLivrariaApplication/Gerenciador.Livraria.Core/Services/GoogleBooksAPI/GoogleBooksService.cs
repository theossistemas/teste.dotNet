using Gerenciador.Livraria.Core.Interfaces.ServicesInterface;
using Gerenciador.Livraria.DTOs.DTOs.GoogleBooks;
using Gerenciador.Livraria.DTOs.DTOs.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Services.GoogleBooksAPI
{
    public class GoogleBooksService : IGoogleBooksService
    {
        #region Variaveis
        private string MENSAGEM_RESULTADO_NAO_ENCONTRADO = "Nenhum resultado encontrado.";
        private string MENSAGEM_ERRO_DE_SERVIDOR = "Houve um erro ao realizar esta requisição. Por favor, tente novamente.";
        private string MENSAGEM_ERRO_DE_REQUISICAO = "Ocorreu um erro ao processar a requisição.";
        #endregion

        private readonly HttpClient _httpClient;

        public GoogleBooksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpHelperResponseDTO> BuscarObraPeloTitulo(string titulo)
        {
            return await IntegrarConsultaComGoogleBooksAPI($"q=intitle:{titulo}");
        }

        public async Task<HttpHelperResponseDTO> BuscarObrasDoAutor(string autor)
        {
            return await IntegrarConsultaComGoogleBooksAPI($"q=inauthor:{autor}");
        }

        public async Task<HttpHelperResponseDTO> BuscarObrasPorCategoria(string categoria)
        {
            return await IntegrarConsultaComGoogleBooksAPI($"q=subject:{categoria}");
        }

        private async Task<HttpHelperResponseDTO> IntegrarConsultaComGoogleBooksAPI(string query)
        {
            var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?{query}");
            var responseDto = new HttpHelperResponseDTO
            {
                CodigoDeStatus = response.StatusCode,
                Mensagem = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                responseDto.Dados = await response.Content.ReadAsStringAsync();
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        responseDto.Mensagem = MENSAGEM_RESULTADO_NAO_ENCONTRADO;
                        break;
                    case HttpStatusCode.InternalServerError:
                        responseDto.Mensagem = MENSAGEM_ERRO_DE_SERVIDOR;
                        break;
                    default:
                        responseDto.Mensagem = MENSAGEM_ERRO_DE_REQUISICAO;
                        break;
                }
                responseDto.Dados = null;
            }

            return responseDto;
        }
    }
}
