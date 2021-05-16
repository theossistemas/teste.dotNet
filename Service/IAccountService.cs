using Architecture;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAccountService : IServiceBase<Account>
    {
        Task<(Account, string, string)> AuthenticateAsync(Account model, string ipAddress);
        Task<(Account, string, string)> RefreshTokenAsync(string token, string ipAddress);
        Task RevokeTokenAsync(string token, string ipAddress);
        Task<Account> RegisterAsync(Account model, string origin);
        Task VerifyEmailAsync(string token);
        Task ForgotPasswordAsync(ForgotPasswordRequestDTO model, string origin);
        Task ValidateResetTokenAsync(ValidateResetTokenRequestDTO model);
        Task ResetPasswordAsync(ResetPasswordRequestDTO model);
        Task DisableAccount(Guid id);
    }
}
