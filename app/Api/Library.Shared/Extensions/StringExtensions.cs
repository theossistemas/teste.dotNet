namespace Library.Shared.Extensions;

public static class StringExtensions
{
    public static Guid GuidParse(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("O valor não pode ser nulo ou vazio.", nameof(value));

        try
        {
            return Guid.Parse(value);
        }
        catch (FormatException)
        {
            throw new FormatException($"O valor '{value}' não é um GUID válido.");
        }
    }
}