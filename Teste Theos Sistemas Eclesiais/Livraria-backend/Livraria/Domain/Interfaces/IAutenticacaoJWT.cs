namespace Livraria.Domain.Interfaces
{
    public interface IAutenticacaoJWT
    {
        Task<string> GerarTokenAsync(string username, string password, string role);
    }
}
