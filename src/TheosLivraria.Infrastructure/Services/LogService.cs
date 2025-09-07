using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repositories;
using TheosLivraria.Domain.Models.Enums;

public class LogService : ILogService
{
    private readonly IBaseRepository<Log> _logRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogService(IBaseRepository<Log> logRepository, IUnitOfWork unitOfWork)
    {
        _logRepository = logRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SaveRequestAsync(string path, string method, string requestBody, string responseBody, int statusCode)
    {
        var log = new Log
        {
            RequestPath = path,
            Method = method,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            StatusCode = statusCode,
            Level = LogLevelEnum.Info,
            CreatedAt = DateTime.UtcNow
        };

        await _logRepository.AddAsync(log);
        await _unitOfWork.CommitAsync();
    }

    public async Task SaveExceptionAsync(string path, string method, Exception exception)
    {
        var log = new Log
        {
            RequestPath = path,
            Method = method,
            ResponseBody = exception.ToString(),
            StatusCode = 500,
            Level = LogLevelEnum.Error,
            CreatedAt = DateTime.UtcNow
        };

        await _logRepository.AddAsync(log);
        await _unitOfWork.CommitAsync();
    }
}
