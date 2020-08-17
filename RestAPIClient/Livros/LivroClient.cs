using Models.DTO;
using RestAPIClient.Base;
using RestAPIClient.Configurations;
using RestAPIClient.Response;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestAPIClient.Livros
{
    public class LivroClient : ABaseClient, ILivroClient
    {
        private String BaseUrl { get; }

        public LivroClient()
        {
            this.BaseUrl = Config.GetUrl("livro");
        }

        public async Task<IApiResponse> Delete(Int64? id, UsuarioDTO usuario)
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    String url = $"{this.BaseUrl}/{id}";

                    HttpRequestMessage httpRequest = this.CriarMensagemRequisicaoComAutorizacaoBasica(HttpMethod.Delete, url, usuario);

                    HttpResponseMessage message = await client.SendAsync(httpRequest);

                    response.HttpStatusCode = message.StatusCode;

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }

        public async Task<IApiResponse> Find(Int64? id)
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    String url = $"{this.BaseUrl}/{id}";

                    HttpResponseMessage message = await client.GetAsync(url);

                    response = await this.TratarRetornoJson(response, message, typeof(LivroDTO));

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }

        public async Task<IApiResponse> FindAll()
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    HttpResponseMessage message = await client.GetAsync(this.BaseUrl);

                    response = await this.TratarRetornoJsonArray<LivroDTO>(response, message);

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }

        public async Task<IApiResponse> FindByTitle(String title)
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    String url = $"{this.BaseUrl}/titulos/{title}";

                    HttpResponseMessage message = await client.GetAsync(url);

                    response = await this.TratarRetornoJsonArray<LivroDTO>(response, message);

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }

        public async Task<IApiResponse> Save(LivroDTO livro, UsuarioDTO usuario)
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    HttpRequestMessage httpRequest = this.CriarMensagemRequisicaoComAutorizacaoBasica(HttpMethod.Post, this.BaseUrl, livro, usuario);

                    HttpResponseMessage message = await client.SendAsync(httpRequest);

                    response = await this.TratarRetornoJson(response, message, typeof(LivroDTO));

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }

        public async Task<IApiResponse> Update(Int64? id, LivroDTO livro, UsuarioDTO usuario)
        {
            IApiResponse response = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    String url = $"{this.BaseUrl}/{id}";

                    HttpRequestMessage httpRequest = this.CriarMensagemRequisicaoComAutorizacaoBasica(HttpMethod.Put, url, livro, usuario);

                    HttpResponseMessage message = await client.SendAsync(httpRequest);

                    response = await this.TratarRetornoJson(response, message, typeof(LivroDTO));

                    return response;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(response, ex);
            }
        }
    }
}
