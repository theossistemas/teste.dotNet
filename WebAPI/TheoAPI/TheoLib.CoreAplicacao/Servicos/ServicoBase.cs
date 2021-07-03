using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheoLib.Dominio.Modelo;

namespace TheoLib.CoreAplicacao.Servicos
{
    public class ServicoBase
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

        public ServicoBase(IMapper mapper, ILogger logger, IConfiguration configuration)
        {  
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }
        protected async Task<RespostaBase> ValidarRequest<TValidator, TItem>(TValidator validator, TItem item) where TValidator : AbstractValidator<TItem>
        {
            var validacao = validator.Validate(item);
            if (!validacao.IsValid)
                return await ObterCodigoDoStatus(string.Join(" | ", validacao.Errors), StatusCodes.Status400BadRequest);

            return default;
        }

        protected Task<RespostaBase> ObterCodigoDoStatus(string mensagem, int statusCode, dynamic conteudo = null, Exception ex = null)
        {
            var baseResp = new RespostaBase();
            baseResp.StatusCode = statusCode;
            baseResp.Mensagem = mensagem;
            baseResp.Conteudo = conteudo;

            if (statusCode == 200)
                _logger.LogInformation(mensagem); 
            else
                _logger.LogError($"{mensagem} ::: {ex}");


            return Task.FromResult(baseResp);
        } 

    }
}
