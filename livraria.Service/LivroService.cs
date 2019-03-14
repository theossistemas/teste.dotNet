using System.Collections.Generic;
using System.Linq;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Domain.interfaces.common;
using livraria.Service.common;
using livraria.Service.interfaces;

namespace livraria.Service
{
    public class LivroService : Service<Livro>, ILivroService
    {
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository):base(repository)
        {
            _repository = repository;
        }

        public override void Create(Livro obj)
        {
            if (!obj.IsValidCadastro)
                throw new System.Exception().TrataErros(obj.ValidationResult.Errors);;


            _repository.Create(obj);
        }

        public override void Update(Livro obj, int id)
        {
            if (!obj.IsValidAlteracao)
                throw new System.Exception().TrataErros(obj.ValidationResult.Errors);

            _repository.Update(obj, id);
        }

        public override IList<Livro> GetAll()
        {
            return _repository.GetAll().OrderBy(x=>x.Titulo).ToList();
        }
    }
}
