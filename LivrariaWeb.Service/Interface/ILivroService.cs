using LivrariaWeb.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Service.Interface
{
    public interface ILivroService
    {
        public Task<IEnumerable<DtoLivro>> GetAll();
        public Task<DtoResult<DtoLivro>> Cadastrar(DtoLivro dtoLivro);
        public Task<DtoResult<DtoLivro>> GetLivroById(long idLivro);
        public Task<DtoResult<DtoLivro>> UpdateLivro(DtoLivro dtoLivro);
        public Task<DtoResult<DtoLivro>> DeleteCurso(long idLivro);

    }
}
