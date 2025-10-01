using Library.Domain.Interfaces;
using Library.Domain.Entities.Base;
using Library.Domain.Enum;

namespace Library.Domain.Entities;

public class Login : BaseEntity
{
    public Login(
        string userName,
        string email,
        string passwordHash)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Login(
        Guid id,
        string userName,
        string email,
        string passwordHash,
        bool active,
        DateTime createdAt)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Active = active;
        CreatedAt = createdAt;
    }

    internal Login() { }

    private string _email = default!;
    public string Email
    {
        get => _email;
        private set => _email = value.ToLowerInvariant();
    }
    public string UserName      { get; private set; } = default!;
    public string PasswordHash  { get; private set; } = default!;
    public Roles Role           { get; private set; } = Roles.Admin;

    public static Login Create(
        Guid id,
        string userName,
        string email,
        string passwordHash,
        bool active,
        DateTime createdAt)
    {
        return new Login(
            id,
            userName,
            email,
            passwordHash,
            active,
            createdAt);
    }

    public static Login Create(
        string userName,
        string email,
        string passwordHash)
    {
        return new Login(
            userName,
            email,
            passwordHash);
    }

    public void SetUserName(string userName)
        => UserName = userName;

    public static async Task<Login> Insert(Login login, IRepository<Login> repo, CancellationToken cancellation)
    {
        var entity = await repo.Insert(login, cancellation);
        return entity;
    }
}