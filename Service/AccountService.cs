using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Repository;
using Microsoft.Extensions.Options;
using System.Linq;
using Architecture;
using AutoMapper;

namespace Service
{
    public class AccountService : ServiceBase<Account>, IAccountService
    {
        private readonly ApplicationSettings _applicationSettings;
        
        public AccountService(IAccountRepository repository, IOptions<ApplicationSettings> applicationSettings) : base(repository)
        {
            this._applicationSettings = applicationSettings.Value;
        }

        public async Task<(Account, string, string)> AuthenticateAsync(Account model, string ipAddress)
        {
            var account = (await this._repository.GetByIncludingAsync((e => e.Email == model.Email), false, (e => e.RefreshTokens))).FirstOrDefault();

            if(account == null)
            {
                throw new ApplicationException("Email not found!");
            }

            if (!account.Active)
            {
                throw new ApplicationException("Conta desativada!");
            }

            if (account == null || /*!account.IsVerified ||*/ !BC.Verify(model.Password, account.PasswordHash))
            {
                throw new ApplicationException("Email or password is incorrect");
            }

            var jwtToken = this.GenerateJwtToken(account);
            var refreshToken = this.GenerateRefreshToken(ipAddress);

            account.RefreshTokens.Add(refreshToken);
            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();

            return (account, jwtToken, refreshToken.Token);
        }

        public async Task<(Account, string, string)> RefreshTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = await this.GetRefreshToken(token);

            var newRefreshToken = this.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();

            var jwtToken = this.GenerateJwtToken(account);

            return (account, jwtToken, newRefreshToken.Token);
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = await this.GetRefreshToken(token);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();
        }

        public async Task<Account> RegisterAsync(Account model, string origin)
        {
            var account = await this._repository.GetByAsync((e => e.Email == model.Email));

            if (account != null)
            {
                throw new Exception("Account exist");
            }

            model.VerificationToken = this.RandomTokenString();
            model.PasswordHash = BC.HashPassword(model.Password);
            
            await this._repository.CreateAsync(model);
            await this._repository.SaveChangesAsync();

            return model;
        }

        public async Task VerifyEmailAsync(string token)
        {
            var account = await this._repository.GetByAsync((e => e.VerificationToken == token));

            if (account == null)
            {
                throw new ApplicationException("Verification failed");
            }

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDTO model, string origin)
        {
            var account = await this._repository.GetByAsync((e => e.Email == model.Email));

            if (account == null)
            {
                return;
            }

            if (!account.Active)
            {
                throw new ApplicationException("Conta desativada!");
            }

            account.ResetToken = this.RandomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(24);

            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();
        }

        public async Task ValidateResetTokenAsync(ValidateResetTokenRequestDTO model)
        {
            var account = await this._repository.GetByAsync((e => e.ResetToken == model.Token && e.ResetTokenExpires > DateTime.UtcNow));

            if (account == null)
            {
                throw new ApplicationException("Invalid token");
            }

            if (!account.Active)
            {
                throw new ApplicationException("Conta desativada!");
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDTO model)
        {
            var account = await this._repository.GetByAsync((e => e.ResetToken == model.Token && e.ResetTokenExpires > DateTime.UtcNow));

            if (account == null)
            {
                throw new ApplicationException("Invalid token");
            }

            if (!account.Active)
            {
                throw new ApplicationException("Conta desativada!");
            }

            account.PasswordHash = BC.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await this._repository.UpdateAsync(account.ID, account);
            await this._repository.SaveChangesAsync();
        }

        public override async Task<ICollection<Account>> GetAllAsync()
        {
            var accounts = await this._repository.GetAllAsync();
            return accounts;
        }

        public override async Task<Account> GetByIdAsync(Guid id)
        {
            var account = await this._repository.GetByIdAsync(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public override async Task<Account> CreateAsync(Account model)
        {
            var exist = await this._repository.GetByAsync((e => e.Email == model.Email));

            if (exist != null)
            {
                throw new ApplicationException($"Email '{model.Email}' is already registered");
            }

            var account = model;
            account.Verified = DateTime.UtcNow;
            account.PasswordHash = BC.HashPassword(model.PasswordHash);
            
            await this._repository.CreateAsync(account);

            return account;
        }

        public override async Task<Account> UpdateAsync(Guid id, Account model)
        {
            var account = await this.GetByIdAsync(id);
            var exist = await this._repository.GetByAsync((e => e.Email == model.Email));
            
            await this._repository.UpdateAsync(id, model);

            return account;
        }

        public override async Task<Account> DeleteAsync(Guid id)
        {
            var account = await this.GetByIdAsync(id);
            await this._repository.DeleteAsync(account);

            return account;
        }

        private async Task<(RefreshToken, Account)> GetRefreshToken(string token)
        {
            var account = await this._repository.GetByAsync((e => e.RefreshTokens.Any(t => t.Token == token)));
            if (account == null)
            {
                throw new ApplicationException("Invalid token");
            }
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                throw new ApplicationException("Invalid token");
            }
            return (refreshToken, account);
        }

        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_applicationSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public async Task DisableAccount(Guid id)
        {
            var account = await this._repository.GetByIdAsync(id);

            if (account == null)
            {
                throw new KeyNotFoundException("Conta não localizada!");
            }

            account.Active = false;

            await this._repository.UpdateAsync(account.ID, account);
        }
    }
}
