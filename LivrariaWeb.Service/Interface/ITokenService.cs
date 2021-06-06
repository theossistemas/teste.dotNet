using LivrariaWeb.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Service.Interface
{
    public interface ITokenService
    {
        public Task<DtoResult<DtoPessoa>> Login(string email, string senha);
        public Task<DtoResult<DtoPessoa>> Cadastrar(string nome, string email, string senha);
    }
}
