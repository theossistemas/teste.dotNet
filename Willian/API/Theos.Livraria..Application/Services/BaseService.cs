using AutoMapper; 
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Theos.Livraria.Domain.Model;
using Microsoft.Extensions.Configuration;
namespace Theos.Livraria.Application.Services
{
    public class BaseService
    {  
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

        protected BaseService(IMapper mapper, ILogger logger, IConfiguration configuration)
        {  
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        } 

        protected async Task<BaseResponse> ValidarRequest<TValidator, TItem>(TValidator validator, TItem item) where TValidator : AbstractValidator<TItem>
        {
            var validacao = validator.Validate(item);
            if (!validacao.IsValid)
                return await ObterStatusCode(string.Join(" | ", validacao.Errors), StatusCodes.Status400BadRequest);

            return default;
        }
         
        protected Task<BaseResponse> ObterStatusCode(string mensagem, int statusCode, dynamic conteudo = null, Exception ex = null)
        {
            var baseResponse = new BaseResponse();
            baseResponse.StatusCode = statusCode;
            baseResponse.Mensagem = mensagem;
            baseResponse.Conteudo = conteudo;

            if (statusCode == 200)
                _logger.LogInformation(mensagem); 
            else
                _logger.LogError(mensagem + ": " + ex);


            return Task.FromResult(baseResponse);
        } 
    }
}
