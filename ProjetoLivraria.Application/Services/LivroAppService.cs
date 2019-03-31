using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProjetoLivraria.Application.Interfaces;
using ProjetoLivraria.Application.ViewModels;
using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Repositories.Interfaces;

namespace ProjetoLivraria.Application.Services
{
    public class LivroAppService : ILivroAppService
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _livroRepository;

        public LivroAppService(IMapper mapper, ILivroRepository livroRepository)
        {
            _mapper = mapper;
            _livroRepository = livroRepository;
        }

        public IEnumerable<LivroViewModel> GetAll()
        {
            return _livroRepository.GetAll().ProjectTo<LivroViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<LivroViewModel> GetAllOrderByTitle()
        {
            return _livroRepository.GetAllOrderByTitle().ProjectTo<LivroViewModel>(_mapper.ConfigurationProvider);
        }

        public LivroViewModel GetById(Guid id)
        {
            return _mapper.Map<LivroViewModel>(_livroRepository.GetById(id));
        }

        public void Register(LivroViewModel livroViewModel)
        {
            _livroRepository.Add(_mapper.Map<Livro>(livroViewModel));
            if (!(_livroRepository.SaveChanges() > 0))
            {
                throw new ApplicationException("Erro ao cadastrar livro!");
            }
        }

        public void Update(LivroViewModel livroViewModel)
        {
            _livroRepository.Update(_mapper.Map<Livro>(livroViewModel));
            if (!(_livroRepository.SaveChanges() > 0))
            {
                throw new ApplicationException("Erro ao atualizar livro!");
            }
        }

        public void Remove(Guid id)
        {
            _livroRepository.Remove(id);
            if (!(_livroRepository.SaveChanges() > 0))
            {
                throw new ApplicationException("Erro ao remover livro!");
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
