namespace Library.Shared.Validators;

public static class StringValidators
{
    public static bool IsEmpty(this string? value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsGuid(this string? value)
        => Guid.TryParse(value, out Guid guid) && guid != Guid.Empty;
}