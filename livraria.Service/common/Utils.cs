using livraria.Domain.entities.Validation.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace livraria.Service.common
{
    public static class Utils
    {
        public static Exception TrataErros(this Exception ex, IList<ValidationError> erros)
        {
            var strb = new StringBuilder();

            erros.ToList().ForEach(x => strb.AppendLine(x.Message));

            return new Exception(strb.ToString());
        }
    }
}
