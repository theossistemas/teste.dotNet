using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string TableName { get; set; } = default!;
    public string EventType { get; set; } = default!;
    public string? UserName { get; set; }
    public string Data { get; set; } = default!;
}