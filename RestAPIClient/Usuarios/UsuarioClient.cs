using Models.DTO;
using RestAPIClient.Base;
using RestAPIClient.Configurations;
using RestAPIClient.Response;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestAPIClient.Usuarios
{
    public class UsuarioClient : ABaseClient, IUsuarioClient
    {
        private String BaseUrl { get; }

        public UsuarioClient()
        {
            this.BaseUrl = Config.GetUrl("usuario");
        }

        public async Task<IApiResponse> ValidarLogin(string login, string senha)
        {
            IApiResponse apiResponse = ApiResponse.Create();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    String url = $"{this.BaseUrl}/{login}/{senha}";

                    HttpResponseMessage message = await client.PostAsync(url, null);

                    apiResponse = await this.TratarRetornoJson(apiResponse, message, typeof(Boolean));

                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                return this.TratarExcecaoCliente(apiResponse, ex);
            }
        }
    }
}
