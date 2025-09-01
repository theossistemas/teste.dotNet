
namespace TheosLivraria.Domain.Models.Entities
{
    public class AuditLogEntity : BaseEntity
    {
        public string CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; } 
        public string Message { get; set; }
        public string Path { get; set; }
        public string Data { get; set; } 
    }
}
