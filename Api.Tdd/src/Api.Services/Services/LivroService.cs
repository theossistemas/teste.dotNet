using Api.Domain.Entities;
using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Interfaces.Services.CategoriaQuarto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LivroService : ILivroServices
    {
        private readonly IUnitOfWork<Livro> _unitOfWorky;
        private readonly IMapper _mapper;
        public LivroService(IUnitOfWork<Livro> repository, IMapper mapper)
        {
            _unitOfWorky = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWorky.Repository.DeleteLogicAsync(id);
        }

        public async Task<LivroDto> Get(int id)
        {
            var setor = await _unitOfWorky.Repository.SelectAsync(id);
            return _mapper.Map<LivroDto>(setor);
        }

        public async Task<IEnumerable<LivroDto>> GetAll()
        {
            var setor = await _unitOfWorky.Repository.SelectAsync();
            return _mapper.Map<IEnumerable<LivroDto>>(setor);
        }     

        public async Task<LivroDto> Create(LivroDto categoria)
        {
            var entidade = _mapper.Map<Livro>(categoria);
            entidade.Ativo = true;
            
            if (await _unitOfWorky.Repository.ExistAsync(p => p.Titulo.Equals(entidade.Titulo)))
                return null;

            var result = await _unitOfWorky.Repository.InsertAsync(entidade);

            return _mapper.Map<LivroDto>(result);
        }

        public async Task<LivroDto> Update(LivroDto categoria)
        {
            var entidade = _mapper.Map<Livro>(categoria);
            entidade.Ativo = true;
            var result = await _unitOfWorky.Repository.UpdateAsync(entidade);

            return _mapper.Map<LivroDto>(result);
        }
    }
}
