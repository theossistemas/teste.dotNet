using System;

namespace Api.Domain.Dto
{
    public class LogErrorDto
    {

        public string Message { get; set; }
        public Guid User { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NameUser { get; set; }
    }
}
