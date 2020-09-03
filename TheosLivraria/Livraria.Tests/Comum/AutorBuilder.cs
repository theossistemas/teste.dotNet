using Livraria.Common.Utils;
using Livraria.Domain.Entidades;

namespace Livraria.Tests.Comum
{
    public class AutorBuilder
    {
        private int _id;
        private string _nome;

        public static AutorBuilder Novo()
        {
            var fake = FakerBuilder.Novo().Build();
            return new AutorBuilder
            {
                _id = fake.Random.Int(1, 100),
                _nome = fake.Person.FullName
            };
        }

        public AutorBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public AutorBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public Autor Build()
        {
            var autor = new Autor(_nome);
            if (_id > Constantes.Zero)
                autor.DefinirId(_id);
            return autor;
        }
    }
}
