using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface ILogErrorService
    {
        Task<object> AddError(LogErrorDto error);
    }
}
