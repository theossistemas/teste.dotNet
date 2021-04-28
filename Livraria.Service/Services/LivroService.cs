using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Service.Validators;

namespace Livraria.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

         public LivroService(ILivroRepository livorRepository, IMapper mapper)
        {
            _livroRepository = livorRepository;            
            _mapper = mapper;
        }


        public IList<Livro> GetName(string name)
        {
            var entities = _livroRepository.GetName(name);
            var outputModels = _mapper.Map<IList<Livro>>(entities);
            return outputModels;
        }

        public Livro Insert(Livro obj)
        {
            Validate(obj, Activator.CreateInstance<LivroValidator>());
            _livroRepository.Insert(obj);            
            return obj;

        }

        public Livro Update(Livro obj)
        {
            Validate(obj, Activator.CreateInstance<LivroValidator>());
            _livroRepository.Update(obj);            
            return obj;
        }

        public void Delete(int id) => _livroRepository.Delete(id);
        public IList<Livro> Select()
        {
            var entities = _livroRepository.Select();

            var users = _mapper.Map<List<Livro>>(entities);
            return users;
        }
        public Livro Select(int id)
        {
            var entities = _livroRepository.Select(id);

            var user = _mapper.Map<Livro>(entities);

            return user;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Validate(Livro obj, AbstractValidator<Livro> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
      
    }
}