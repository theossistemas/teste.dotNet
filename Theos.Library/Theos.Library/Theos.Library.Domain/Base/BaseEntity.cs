using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Theos.Library.Domain.Logs;

namespace Theos.Library.Domain.Base
{
    public class BaseEntity : BaseKey
    {
        public BaseEntity()
        {
            Date = DateTime.Now;
            Version = 1;
            Active = true;
        }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int Version { get; set; }
        [Required]
        public bool Active { get; set; }

        [NotMapped]
        public List<Log> Logs { get; set; }

        [NotMapped]
        public bool CreateLog { get; set; }

        public virtual Guid GetPk()
        {
            return Id;
        }
    }
}
