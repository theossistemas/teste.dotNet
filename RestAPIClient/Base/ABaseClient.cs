using Models.DTO;
using Newtonsoft.Json;
using RestAPIClient.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace RestAPIClient.Base
{
    public abstract class ABaseClient
    {
        public virtual HttpRequestMessage CriarMensagemRequisicaoComAutorizacaoBasica(HttpMethod method, String uri, UsuarioDTO usuario)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(method, uri);

            Byte[] bytes = Encoding.UTF8.GetBytes($"{usuario.Login}:{usuario.Senha}");

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            return httpRequest;
        }

        public virtual HttpRequestMessage CriarMensagemRequisicaoComAutorizacaoBasica(HttpMethod method, String uri, Object body, UsuarioDTO usuario)
        {
            HttpRequestMessage httpRequest = this.CriarMensagemRequisicaoComAutorizacaoBasica(method, uri, usuario);

            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return httpRequest;
        }

        public virtual async Task<IApiResponse> TratarRetorno(IApiResponse response, HttpResponseMessage message)
        {
            response.HttpStatusCode = message.StatusCode;

            String content = await message.Content.ReadAsStringAsync();

            response.ResponseBody = JsonConvert.DeserializeObject(content, typeof(LivroDTO));

            return response;
        }

        public virtual IApiResponse TratarExcecaoCliente(IApiResponse apiResponse, Exception ex)
        {
            ex.GravarLog();

            apiResponse.SetError(ex);

            return apiResponse;
        }

        public virtual async Task<IApiResponse> TratarRetornoJson(IApiResponse response, HttpResponseMessage message, Type type)
        {
            response.HttpStatusCode = message.StatusCode;

            String content = await message.Content.ReadAsStringAsync();

            response.ResponseBody = JsonConvert.DeserializeObject(content, type);

            return response;
        }

        public virtual async Task<IApiResponse> TratarRetornoJsonArray<T>(IApiResponse response, HttpResponseMessage message)
        {
            response.HttpStatusCode = message.StatusCode;

            String content = await message.Content.ReadAsStringAsync();

            response.ResponseBody = ((T[])JsonConvert.DeserializeObject(content, typeof(T[]))).ToList<T>();

            return response;
        }
    }
}
