using Domain;
using Repository;
using Repository.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LivrosService : IService<Livros>
    {
        #region Properties
        private readonly IRepository<Livros> _repository;
        private readonly IRelatorioHelper _relatorioHelper;
        private readonly ILivrosLogs _logs;
        #endregion

        #region Constants
        private const string LIVRO_CREATE = "Livro criado com sucesso!";
        private const string LIVRO_UPDATE = "Livro atualizado com sucesso!";
        private const string LIVRO_ERROR = "Erro ao executar função!";
        private const string LIVRO_DELETEDO = "Livro deletado!";
        #endregion

        #region Constructor
        public LivrosService(IRepository<Livros> repository, IRelatorioHelper relatorioHelper, ILivrosLogs logs)
        {
            _repository = repository;
            _relatorioHelper = relatorioHelper;
            _logs = logs;
        }
        #endregion

        #region Methods
        public async Task<IList<Livros>> FindAllAsync()
        {
            return await _repository.FindAllAsync();
        }

        public async Task<Livros> FindAsync(int key)
        {
            return await _repository.FindAsync(key);
        }

        public async Task InsertAsync(Livros model)
        {
            try
            {
                _repository.Insert(model);
                await _relatorioHelper.GerarRelatorio(_logs.ToLivroLog(model, LIVRO_CREATE));
            }
            catch (Exception)
            {
                await _relatorioHelper.GerarRelatorio(_logs.ToLivroLog(model, LIVRO_ERROR));
                throw new Exception("Não foi possível salvar no banco de dados");
            }
        }

        public async Task RemoveAsync(int key)
        {
            var livro = FindAsync(key).Result;
            await _relatorioHelper.GerarRelatorio(_logs.ToLivroLog(livro, LIVRO_DELETEDO));
            await _repository.RemoveAsync(livro);
        }

        public async Task UpdateChangesAsync(Livros model)
        {
            try
            {
                await _repository.UpdateChangesAsync(model);
                await _relatorioHelper.GerarRelatorio(_logs.ToLivroLog(model, LIVRO_UPDATE));
            }
            catch (Exception)
            {
                await _relatorioHelper.GerarRelatorio(_logs.ToLivroLog(model, LIVRO_ERROR));
                throw new Exception("Não foi possível salvar no banco de dados");
            }

        }
        #endregion      
    }
}
