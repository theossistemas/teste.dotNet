using TheosLivraria.Application.UseCases.Commom;

namespace TheosLivraria.Application.UseCases.Abstractions
{
    public interface IUseCase<TInput, TOutput>
    {
        Task<Result<TOutput>> ExecuteAsync(TInput input);
    }
}
