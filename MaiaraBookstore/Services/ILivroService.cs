using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiaraBookstore.Services
{
    public interface ILivroService
    {
        bool ValidaSeTituloDeLivroEstaCadastrado(string titulo);

    }
}
