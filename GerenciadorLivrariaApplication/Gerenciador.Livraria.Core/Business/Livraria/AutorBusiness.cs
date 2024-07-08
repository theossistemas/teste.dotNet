using AutoMapper;
using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.Core.Interfaces.BusinessInterface;
using Gerenciador.Livraria.Core.Interfaces.Logs;
using Gerenciador.Livraria.Core.Interfaces.Repositories;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Business.Livraria
{
    public class AutorBusiness : IAutorBusiness
    {
        private readonly IRepository<AutorEntity> _autorEntityRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _loggerHelper;

        public AutorBusiness(IRepository<AutorEntity> autorEntityRepository,
                             IMapper mapper,
                             ILoggerHelper loggerHelper)
        {
            _autorEntityRepository = autorEntityRepository;
            _mapper = mapper;
            _loggerHelper = loggerHelper;
        }

        public async Task<bool> VerificarSeAutorJaExiste(string nome)
        {
            return await _autorEntityRepository.Exists(c => c.Nome == nome);
        }

        public async Task<AutorDTO> CadastrarNovoAutor(AutorDTO autorDTO)
        {
            if (await VerificarSeAutorJaExiste(autorDTO.Nome))
            {
                await _loggerHelper.RegistrarLog($"Autor com o nome: '{autorDTO.Nome}' já existe.", "Error");
                throw new Exception("Autor já cadastrado.");
            }

            try
            {
                var novoAutor = _mapper.Map<AutorEntity>(autorDTO);

                novoAutor.Ativo = true;
                novoAutor.DataDeRegistro = DateTime.Now;

                await _autorEntityRepository.InsertAsync(novoAutor);
                await _loggerHelper.RegistrarLog($"Autor '{novoAutor.Nome}' cadastrado com sucesso.", "Success");

                return _mapper.Map<AutorDTO>(novoAutor);
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }
            
        }

        public string PesquisarAutorPeloId(int id)
        {
            var nomeDoAutor = _autorEntityRepository.GetByIdAsync(id);

            if (nomeDoAutor is null || nomeDoAutor.Result is null)
                throw new Exception("Nenhum autor encontrado.");

            return nomeDoAutor.Result.Nome;
        }

        public async Task<AutorDTO> AtualizarDadosDoAutor(AutorDTO autorDTO)
        {
            if (autorDTO.Id is null)
                throw new ArgumentNullException(nameof(autorDTO), "É necessário informar um autor (Id) válido para realizar essa operação");

            var autor = await _autorEntityRepository.GetByIdAsync((int)autorDTO.Id);

            if (autor is null)
                throw new Exception("Autor não encontrado");

            try
            {
                autor.Nome = autorDTO.Nome;
                autor.Descricao = autorDTO.Descricao;

                await _autorEntityRepository.UpdateAsync(autor);
                await _loggerHelper.RegistrarLog($"Dados do autor '{autor.Nome}' atualizados com sucesso.", "Success");

                return _mapper.Map<AutorDTO>(autor);
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }
            
        }

        public async Task<bool> ExcluirRegistroFisicoDoAutor(int id)
        {
            try
            {
                var autor = await _autorEntityRepository.GetByIdAsync(id);
                if (autor == null)
                {
                    throw new Exception("Autor não encontrado.");
                }

                await _autorEntityRepository.DeleteAsync(id);
                await _loggerHelper.RegistrarLog($"Autor '{autor.Nome}' exclúido com sucesso.", "Success");

                return true;
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex); await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }         
        }

        public async Task<bool> ExcluirRegistroLogicoDoAutor(int id)
        {
            try
            {
                var autor = await _autorEntityRepository.GetByIdAsync(id);
                if (autor == null)
                {
                    throw new Exception("Autor não encontrado.");
                }

                autor.Ativo = false;

                await _autorEntityRepository.UpdateAsync(autor);
                await _loggerHelper.RegistrarLog($"Autor '{autor.Nome}' exclúido com sucesso.", "Success");

                return true;
            }
            catch (Exception ex)
            {
                await _loggerHelper.RegistrarLogDeErro(ex); await _loggerHelper.RegistrarLogDeErro(ex);
                throw;
            }           
        }
    }
}
