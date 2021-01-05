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
    [ApiController]
    public class UsuarioController : ControllerBase
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
            model.Senha = EncriptarSenha(model.Senha);

            var user = _contexto.Usuarios.FirstOrDefault(u => u.Login == model.Login && u.Senha == model.Senha);

            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UsuarioModel>(user));
        }

        [HttpPut, Route("cadastrar")]
        public ActionResult AtualizarUsuario (UsuarioModel model)
        {
            Usuario existente = _contexto.Usuarios.FirstOrDefault(u => u.Login == model.Login && u.Senha == EncriptarSenha(model.Senha));
            Usuario usuario = _mapper.Map<Usuario>(model);


            return Ok();
        }

        public string EncriptarSenha(string value)
        {
            byte[] salt = Encoding.UTF8.GetBytes(value);
            byte[] senhaByte = Encoding.UTF8.GetBytes(value);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
