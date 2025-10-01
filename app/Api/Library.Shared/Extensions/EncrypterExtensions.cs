namespace Library.Shared.Extensions;

public class EncrypterExtensions
{
    public static string HashPassword(string password, int? salt = null)
        => BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt(salt ?? 16));

    public static bool IsValidPassword(string password, string hashPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashPassword);

    private static string GetRandomSalt(int salt)
        => BCrypt.Net.BCrypt.GenerateSalt(salt);
}