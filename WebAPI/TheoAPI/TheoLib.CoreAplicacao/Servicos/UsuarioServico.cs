using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheoLib.CoreAplicacao.Utilidades;
using TheoLib.CoreAplicacao.Validadores;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Contratos.Servicos;
using TheoLib.Dominio.Entidade;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.UsuarioModelo;

namespace TheoLib.CoreAplicacao.Servicos
{
    public class UsuarioServico : ServicoBase, IUsuarioServico
    {
        protected readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IMapper mapper, ILogger<UsuarioServico> logger, IConfiguration configuration) : base (mapper, logger, configuration)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public async Task<RespostaBase> Atualizar(RequisicaoUsuario model)
        {
            try
            {
                _logger.LogInformation("Inicio do UPDATE do usuário.");

                var valid = await ValidarRequest(new UsuarioValidador(), model);
                if (valid != null)
                    return valid;

                if (await _usuarioRepositorio.UsuarioEstaCadastrado (model.Id))
                {
                    var entity = await _usuarioRepositorio.ObterPorId(model.Id);
                    var usuario = _mapper.Map<Usuario>(model);

                    if (!string.IsNullOrEmpty(model.Senha))
                        usuario.Senha = CriptDecript.Encrypt(model.Senha);

                    var response = await ObterCodigoDoStatus("Usuário atualizado com sucesso.", StatusCodes.Status200OK, _mapper.Map<RespostaUsuario>(await _usuarioRepositorio.Atualizar(usuario)));

                    _logger.LogInformation("Fim do UPDATE do usuário.");

                    return response;
                }
                else
                {
                    return await ObterCodigoDoStatus("Não foi encontrado o Usuário.", StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro ao atualizar o usuário", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<RespostaBase> AutenticarUsuario(RequisicaoLoginUsuario request)
        {
            try
            {
                _logger.LogInformation("Autenticando usuário");

                var senhaEncrypt = CriptDecript.Encrypt(request.Senha);

                var usuario = await _usuarioRepositorio.ObterUsuario(request.Email, senhaEncrypt); 

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

                RespostaLoginUsuario response = new RespostaLoginUsuario();
                response.IdUsuario = usuario.Id;
                response.Token = token.RawData;

                _logger.LogInformation("Autenticação concluida OK");

                return await ObterCodigoDoStatus("Autenticação concluida com sucesso.", StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro ao autenticar o usuário", StatusCodes.Status400BadRequest, null, ex);
            }

        }

        public async Task<RespostaBase> Inserir(RequisicaoUsuario model)
        {
            try
            {
                _logger.LogInformation("Iniciando cadastro do usuário.");

                var valid = await ValidarRequest(new UsuarioValidador(), model);
                if (valid != null)
                    return valid;


                var entity = _mapper.Map<Usuario>(model);
                entity.Senha = CriptDecript.Encrypt(model.Senha);

                var response = await ObterCodigoDoStatus("Usuário cadastrado com sucesso.", StatusCodes.Status201Created, _mapper.Map<RespostaUsuario>(await _usuarioRepositorio.Inserir(entity)));

                _logger.LogInformation("Fim do usuário do livro.");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro ao usuário o livro", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<RespostaBase> ObterLista()
        {
            try 
            {
                _logger.LogInformation("Iniciando a consulta em lista");

                var response =  await ObterCodigoDoStatus("Lista de produto carregada com sucesso.",  StatusCodes.Status200OK, _mapper.Map<List<RespostaUsuario>>(await _usuarioRepositorio.ObterLista()) );

                _logger.LogInformation("Fim da consulta em lista");

                return response;
            }
            catch(Exception ex) {
                return await ObterCodigoDoStatus("Erro na consulta em lista", StatusCodes.Status400BadRequest, null, ex);
            }
        }

        public async Task<RespostaBase> ObterPorId(long Id)
        {
            try
            {
                _logger.LogInformation("Inicio obter por ID");

                var usuario = _mapper.Map<RespostaUsuario>(await _usuarioRepositorio.ObterPorId(Id)); 
                var response = usuario != null ?
                    await ObterCodigoDoStatus("sucesso.", StatusCodes.Status200OK, usuario) :
                    await ObterCodigoDoStatus("não encontrado.", StatusCodes.Status400BadRequest);

                _logger.LogInformation("Fim obter por ID");

                return response;
            }
            catch (Exception ex)
            {
                return await ObterCodigoDoStatus("Erro obter por ID", StatusCodes.Status400BadRequest, null, ex);
            } 
        }
    }
}
