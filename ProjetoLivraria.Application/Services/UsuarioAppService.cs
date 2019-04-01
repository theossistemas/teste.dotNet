using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjetoLivraria.Application.Interfaces;
using ProjetoLivraria.Application.ViewModels;
using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Repositories.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProjetoLivraria.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;

        public UsuarioAppService(IMapper mapper, IUsuarioRepository usuarioRepository, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings.Value;
        }

        public UsuarioViewModel Add(UsuarioViewModel item)
        {
            item.Password = CalculaHash(item.Password);
            var usuario = _usuarioRepository.Add(_mapper.Map<Usuario>(item));
            if (!(usuario.Id > 0))
            {
                throw new ApplicationException("Erro ao inserir o usuário");
            }

            var authUser = Authenticate(item.Username, item.Password);
            return authUser;
        }

        public UsuarioViewModel Authenticate(string login, string password)
        {
            CalculaHash(password);
            var usuario = _usuarioRepository.Authenticate(login, password);

            if (usuario == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuario.Token = tokenHandler.WriteToken(token);

            usuario.Password = null;

            return _mapper.Map<UsuarioViewModel>(usuario);
        }

        public IQueryable<UsuarioViewModel> GetAll()
        {
            return _usuarioRepository.GetAll().ProjectTo<UsuarioViewModel>(_mapper.ConfigurationProvider);
        }

        public UsuarioViewModel GetById(Guid id)
        {
            return _mapper.Map<UsuarioViewModel>(_usuarioRepository.GetById(id));
        }

        public void Remove(Guid id)
        {
            if (!(_usuarioRepository.Remove(id) > 0))
            {
                throw new ApplicationException("Erro ao remover usuário");
            }
        }

        public UsuarioViewModel Update(UsuarioViewModel item)
        {
            var usuario = _usuarioRepository.Update(_mapper.Map<Usuario>(item));
            if (!(usuario.Id > 0))
            {
                throw new ApplicationException("Erro ao atualizar usuário");
            }

            var authUser = Authenticate(item.Username, item.Password);
            return authUser;
        }

        public static string CalculaHash(string Senha)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(Senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
