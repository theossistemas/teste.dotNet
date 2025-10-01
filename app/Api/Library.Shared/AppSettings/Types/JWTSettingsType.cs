namespace Library.Shared.AppSettings.Types;

public class JWTSettingsType
{
    public int ExpireHours { get; set; } = default!;
    public string Secret { get; set; } = default!;
}