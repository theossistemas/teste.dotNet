using Gerenciador.Livraria.DTOs.DTOs.GoogleBooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Helpers.GoogleBooks
{
    public class GoogleBooksMapper
    {
        public static IEnumerable<GoogleBooksDTO> MapearDadosDaObraEmJson(string json)
        {
            var obras = new List<GoogleBooksDTO>();
            var objetoJson = JObject.Parse(json);

            var conteudo = objetoJson["items"];

            if (conteudo is not null)
            {
                foreach (var item in conteudo)
                {
                    var obra = new GoogleBooksDTO
                    {
                        Id = item["id"]?.ToString(),
                        InformacoesDaObra = item["volumeInfo"] is not null ? new InformacoesDaObra
                        {
                            Titulo = item["volumeInfo"]["title"].ToString(),
                            Autores = item["volumeInfo"]["authors"]?.ToObject<List<string>>(),
                            Editora = item["volumeInfo"]["publisher"]?.ToString(),
                            DataDePublicacao = item["volumeInfo"]["publishedDate"]?.ToString(),
                            Descricao = item["volumeInfo"]["description"]?.ToString(),
                            QuantidadeDePaginas = item["volumeInfo"]["pageCount"]?.ToObject<int?>(),
                            Categorias = item["volumeInfo"]["categories"]?.ToObject<List<string>>(),
                            Idioma = item["volumeInfo"]["language"]?.ToString()
                        } : null
                    };

                    obras.Add(obra);
                }
            }

            return obras;
        }
    }
}
