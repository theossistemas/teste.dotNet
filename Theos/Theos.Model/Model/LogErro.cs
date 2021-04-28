using System;
using System.ComponentModel.DataAnnotations;

namespace Theos.Model.Model
{
    public class LogErro
    {
        public int LogErroId { get; set; }
        public DateTime DataErro { get; set; }
        [StringLength(5000)]
        public string Erro { get; set; }
    }
}
