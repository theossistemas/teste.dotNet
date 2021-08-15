using LivrariaTheos.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Generos
{
    public class ArmazenadorDeGenero
    {
        private readonly IGeneroRepository _generoRepositorio;

        public ArmazenadorDeGenero(IGeneroRepository livroRepositorio)
        {
            _generoRepositorio = livroRepositorio;

        }

        public async Task<Genero> Armazenar(Genero genero)
        {
            var generoExistente = await GeneroJaExistente(genero);

            if (generoExistente != null && generoExistente.Id != genero.Id)
            {
                throw new DomainException("Gênero já cadastrado.");
            }

            if (genero.Id == 0)
            {
                genero.Validar();

                _generoRepositorio.Adicionar(genero);
            }
            else
            {
                genero = await AlterarGenero(genero);

                _generoRepositorio.Atualizar(genero);
            }

            await _generoRepositorio.UnitOfWork.Commit();

            return genero;
        }

        public async Task Excluir(int id)
        {
            var genero = await _generoRepositorio.ObterPorId(id);

            if (genero == null)
                throw new DomainException("Genero não encontrado.");

            _generoRepositorio.Excluir(genero);

            await _generoRepositorio.UnitOfWork.Commit();
        }

        private async Task<Genero> AlterarGenero(Genero generoAlterado)
        {
            var genero = await _generoRepositorio.ObterPorId(generoAlterado.Id);
            genero.AlterarNome(generoAlterado.Nome);           
            genero.AlterarUsuarioAlteracao("Administrador");

            return genero;
        }

        private async Task<Genero> GeneroJaExistente(Genero genero)
        {
            var generos = await _generoRepositorio.ObterPorNome(genero.Nome);

            return generos.FirstOrDefault(x => x.Ativo);
        }
    }
}