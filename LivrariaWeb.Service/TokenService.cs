using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Domain.Model;
using LivrariaWeb.Dto;
using LivrariaWeb.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _itokenRepository;
        private readonly ILogger<TokenService> _logger;

        public TokenService(ITokenRepository itokenRepository, ILogger<TokenService> logger)
        {
            _itokenRepository = itokenRepository;
            _logger = logger;
        }
        public async Task<DtoResult<DtoPessoa>> Login(string email, string senha)
        {
            DtoResult<DtoPessoa> dtoResult = new DtoResult<DtoPessoa>();

            try
            {

                bool cadastroExiste = _itokenRepository.VerificaSeCadastroExiste(email, senha);
                if (cadastroExiste)
                {
                    var dtoToken = await _itokenRepository.GetCadastro(email, senha);
                    dtoResult.Result = new DtoPessoa
                    {
                        Email = dtoToken.Email,
                        Senha = dtoToken.Senha
                    };
                    dtoResult.Message = "Login efetuado com sucesso.";
                    return dtoResult;
                }
                else
                    dtoResult.Message = "Login ou Senha Invalido.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return dtoResult;
            }
        }

        public async Task<DtoResult<DtoPessoa>> Cadastrar(string nome, string email, string senha)
        {
            DtoResult<DtoPessoa> dtoResult = new DtoResult<DtoPessoa>();

            try
            {
                bool cadastroExiste = _itokenRepository.VerificaSeCadastroExiste(email, senha);
                if (!cadastroExiste)
                {
                    Pessoa pessoa = new Pessoa(nome, email, senha);

                    _itokenRepository.Add(pessoa);

                    dtoResult.Result = new DtoPessoa
                    {
                        Id = pessoa.Id,
                        Email = pessoa.Email,
                        Nome = pessoa.Nome,
                        Senha = "******"
                    };
                    dtoResult.Message = "Cadastro realizado com sucesso.";
                    return dtoResult;
                }
                else
                    dtoResult.Message = "Cadastro já consta em sua base de dados.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return dtoResult;
            }
        }
    }
}
