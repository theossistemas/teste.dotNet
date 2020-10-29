using Domain;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRelatorioHelper
    {
        Task GerarRelatorio(LivroLogs livro);
    }
}
