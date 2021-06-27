using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using AutoMapper;

namespace Api.Service.Services
{
    public class LogErrorService : ILogErrorService
    {
        private readonly IRepository<LogErrorEntity> _repository;
        private readonly IMapper _mapper;

        public LogErrorService(IRepository<LogErrorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> AddError(LogErrorDto error)
        {
            var entity = _mapper.Map<LogErrorEntity>(error);
            return await _repository.InsertAsync(entity);
        }
    }
}
