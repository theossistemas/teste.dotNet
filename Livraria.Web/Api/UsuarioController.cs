using AutoMapper;

using Livraria.Domain.Contexto;
using Livraria.Domain.Usuarios;
using Livraria.Web.Models.Usuarios;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Livraria.Web.Api
{
    [Route("api/usuario")]
    public class UsuarioController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public UsuarioController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpPost, Route("logar"), AllowAnonymous]
        public ActionResult Logar(UsuarioModel model)
        {
            model.Senha = EncriptarSenha(model.Login, model.Senha);

            Usuario user = _contexto.Usuarios.FirstOrDefault(u => u.Login == model.Login && u.Senha == model.Senha);

            if (user == null)
                return NotFound();

            UsuarioModel usuarioModel = _mapper.Map<UsuarioModel>(user);
            usuarioModel.Token = TokenService.GenerateToken(usuarioModel);

            return Ok(usuarioModel);
        }

        [HttpPut, Route("cadastrar")]
        public ActionResult AtualizarUsuario(UsuarioModel model)
        {
            Usuario existente = _contexto.Usuarios.FirstOrDefault(u => u.Login == model.Login && u.Senha == EncriptarSenha(model.Login, model.Senha));
            Usuario usuario = _mapper.Map<Usuario>(model);

            if (existente != null)
            {
                usuario.Senha = existente.Senha;
                usuario.Id = existente.Id;
                usuario.IdPessoa = existente.IdPessoa;
                _contexto.Usuarios.Update(usuario);
            }

            else
                _contexto.Usuarios.Add(usuario);


            _contexto.SaveChanges();
            return Ok();
        }

        public string EncriptarSenha(string login, string senha)
        {
            byte[] salt = Encoding.UTF8.GetBytes(login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(senha);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
