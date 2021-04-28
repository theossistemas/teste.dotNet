using System;
using System.ComponentModel.DataAnnotations;

namespace Theos.Model.Model
{
    public class LogAtividade
    {
        public int LogAtividadeId { get; set; }
        public DateTime DataAcesso { get; set; }
        [StringLength(250)]
        public string FuncaoAcessada { get; set; }
    }
}
