namespace Library.CrossCutting.Auth.Models;

public class LoginModel
{
    public string UserName  { get; set; } = default!;
    public string Email     { get; set; } = default!;
    public string Role      { get; set; } = default!;
}