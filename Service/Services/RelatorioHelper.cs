using Domain;
using Repository.Repositories;
using Service.Contracts;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RelatorioHelper : IRelatorioHelper
    {
        #region Methods
        public async Task GerarRelatorio(LivroLogs log)
        {
            string linhaRelatorio = await GetLinhaRelatorio(log);
            await System.IO.File.AppendAllLinesAsync("Relatorio.txt", new string[] { linhaRelatorio });
        }

        private async Task<string> GetLinhaRelatorio(LivroLogs log)
        {
            StringBuilder sb = new StringBuilder();

            string templateRelatorio =
                    await System.IO.File.ReadAllTextAsync("TemplateRelatorio.txt");

            sb.AppendLine($@"=============================================");

            string linhaRelatorio =
                string.Format(templateRelatorio,
                    log.NomeLivro,
                    log.Data,
                    log.Acao);


            sb.AppendLine(linhaRelatorio);

            sb.AppendLine($@"=============================================");

            return sb.ToString();
        }
        #endregion
    }
}