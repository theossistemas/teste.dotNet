using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Theos.Livraria.Application.Validators;
using Theos.Livraria.Domain.Entity; 
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theos.Livraria.Domain.Interface.Repository; 
using Theos.Livraria.Domain.Model.Usuario;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Theos.Livraria.Application.Utils;

namespace Theos.Livraria.Application.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        protected readonly IUsuarioRepository _usuarioRepository; 
        public UsuarioService(
                IUsuarioRepository usuarioRepository, 
                IMapper mapper, 
                ILogger<UsuarioService> logger, 
                IConfiguration configuration) : 
            base (mapper, logger, configuration)
        {
            _usuarioRepository = usuarioRepository; 
        }


        public async Task<BaseResponse> Inserir(RequestUsuario request)
        {
            try
            {
                _logger.LogInformation("Iniciando cadastro do usuário.");

                var valid = await ValidarRequest(new UsuarioValidator(), request);
                if (valid != null)
                    return valid;


                var entity = _mapper.Map<Usuario>(request);
                entity.Senha = HelpExtensions.Encrypt(request.Senha);
                entity.DataCadastro = DateTime.Now;
                entity.PrimeiroAcesso = true;

                var response = await ObterStatusCode(
                    "Usuário cadastrado com sucesso.",
                    StatusCodes.Status201Created,
                     _mapper.Map<ResponseUsuario>(await _usuarioRepository.Inserir(entity)));

                _logger.LogInformation("Fim do usuário do livro.");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao usuário o livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<BaseResponse> Atualizar(RequestUsuario request)
        {
            try
            {
                _logger.LogInformation("Iniciando atualização do usuário.");

                var valid = await ValidarRequest(new UsuarioValidator(), request);
                if (valid != null)
                    return valid;

                if (await _usuarioRepository.UsuarioCadastrado(request.Id))
                {
                    var entity = await _usuarioRepository.ObterPorId(request.Id);
                    var usuario = _mapper.Map<Usuario>(request);
                    usuario.PrimeiroAcesso = false;
                    usuario.DataCadastro = entity.DataCadastro;
                    usuario.DataAlteracao = DateTime.Now;

                    if (!string.IsNullOrEmpty(request.Senha))
                        usuario.Senha = HelpExtensions.Encrypt(request.Senha);

                    var response = await ObterStatusCode(
                         "Usuário atualizado com sucesso.",
                         StatusCodes.Status200OK,
                         _mapper.Map<ResponseUsuario>(await _usuarioRepository.Atualizar(usuario)));

                    _logger.LogInformation("Fim da atualização do usuário.");

                    return response;
                }
                else
                {
                    return await ObterStatusCode("Usuário informado não localizado.", StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao atualizar o usuário", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<BaseResponse> AutenticarUsuario(RequestLoginUsuario request)
        {
            try
            {
                _logger.LogInformation("Inicio autenticação do usuário");

                var senhaEncrypt = HelpExtensions.Encrypt(request.Senha);

                var usuario = await _usuarioRepository.ObterUsuario(request.Email, senhaEncrypt); 

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;

                _logger.LogInformation("Fim da autenticação do usuário");

                return await ObterStatusCode("Autenticação realizada com sucesso.", StatusCodes.Status200OK, token.RawData);
            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao autenticar o usuário", StatusCodes.Status400BadRequest, null, ex);
            } 
        }

        public async Task<BaseResponse> ObterLista()
        { 
            try 
            {
                _logger.LogInformation("Inicio busca lista de usuários");

                var response =  await ObterStatusCode(
                        "Lista de produto carregada com sucesso.", 
                        StatusCodes.Status200OK,
                         _mapper.Map<List<ResponseUsuario>>(await _usuarioRepository.ObterLista())
                        );

                _logger.LogInformation("Fim da busca lista de usuários");

                return response;
            }
            catch(Exception ex) {
                return await ObterStatusCode("Erro ao retornar lista de usuários", StatusCodes.Status400BadRequest, null, ex);
            } 
        }

        public async Task<BaseResponse> ObterPorId(long Id)
        {
            try
            {
                _logger.LogInformation("Inicio busca do usuário pelo Id");

                var usuario = _mapper.Map<ResponseUsuario>(await _usuarioRepository.ObterPorId(Id)); 
                var response = usuario != null ?
                                   await ObterStatusCode("Usuário carregado com sucesso.", StatusCodes.Status200OK, usuario) :
                                   await ObterStatusCode("Usuário informado não localizado.", StatusCodes.Status400BadRequest);

                _logger.LogInformation("Fim da busca do usuário por Id");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterStatusCode("Erro ao retornar o usuário", StatusCodes.Status400BadRequest, null, ex);
            } 
        }

         
    }
}
