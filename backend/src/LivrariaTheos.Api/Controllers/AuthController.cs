using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LivrariaTheos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public record LoginDto(string Email, string Password);

    private const string SeedEmail = "admin@theos";
    private const string SeedPassword = "Admin#2024!"; // altere se quiser

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (dto.Email != SeedEmail || dto.Password != SeedPassword)
            return Unauthorized();

        var cfg = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: creds);
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
        