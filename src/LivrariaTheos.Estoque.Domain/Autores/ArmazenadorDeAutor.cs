using LivrariaTheos.Core.DomainObjects;
using LivrariaTheos.Estoque.Domain.Autores.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Autores
{
    public class ArmazenadorDeAutor
    {
        private readonly IAutorRepository _autorRepositorio;

        public ArmazenadorDeAutor(IAutorRepository autorRepositorio)
        {
            _autorRepositorio = autorRepositorio;

        }

        public async Task<Autor> Armazenar(Autor autor)
        {
            var generoExistente = await AutorJaExistente(autor);

            if (generoExistente != null && generoExistente.Id != autor.Id)
            {
                throw new DomainException("Autor já cadastrado.");
            }

            if (autor.Id == 0)
            {
                autor.Validar();

                _autorRepositorio.Adicionar(autor);
            }
            else
            {
                autor = await AlterarAutor(autor);

                _autorRepositorio.Atualizar(autor);
            }

            await _autorRepositorio.UnitOfWork.Commit();

            return autor;
        }

        public async Task Excluir(int id)
        {
            var autor = await _autorRepositorio.ObterPorId(id);

            if (autor == null)
                throw new DomainException("Autor não encontrado.");

            _autorRepositorio.Excluir(autor);

            await _autorRepositorio.UnitOfWork.Commit();
        }

        private async Task<Autor> AlterarAutor(Autor autorAlterado)
        {
            var genero = await _autorRepositorio.ObterPorId(autorAlterado.Id);
            genero.AlterarNome(autorAlterado.Nome);
            genero.AlterarInformacoesRelevantes(autorAlterado.InformacoesRelevantes);
            genero.AlterarNacionalidade(autorAlterado.Nacionalidade);
            genero.AlterarUsuarioAlteracao("Administrador");

            return genero;
        }

        private async Task<Autor> AutorJaExistente(Autor autor)
        {
            var autores = await _autorRepositorio.ObterPorNome(autor.Nome);

            return autores.FirstOrDefault(x => x.Ativo && autor.Nacionalidade == x.Nacionalidade);
        }
    }
}