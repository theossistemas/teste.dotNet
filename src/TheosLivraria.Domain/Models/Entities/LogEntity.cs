using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Enums;

public class Log : BaseEntity
{
    public LogLevelEnum Level { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? RequestPath { get; set; }
    public string? Method { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public int? StatusCode { get; set; }
    public string? Exception { get; set; }
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
}
