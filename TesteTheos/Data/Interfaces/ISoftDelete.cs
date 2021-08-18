using System;

namespace TesteTheos.Data
{
    public interface ISoftDelete
    {
        public bool Excluido { get; set; }

        public DateTime? DataExclusao { get; set; }

        public Guid? ExcluidoPorId { get; set; }


        public Usuario ExcluidoPor { get; set; }
    }
}
