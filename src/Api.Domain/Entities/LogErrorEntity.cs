using System.Diagnostics;
using System;

namespace Api.Domain.Entities
{
    public class LogErrorEntity : BaseEntity
    {
        public string Message { get; set; }
        public Guid User { get; set; }
        public string NameUser { get; set; }
    }
}
