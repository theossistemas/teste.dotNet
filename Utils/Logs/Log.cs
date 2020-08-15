using System;
using System.IO;
using System.Text;

namespace Utils.Logs
{
    public class Log
    {
        private Log() { }
        
        public static String DiretorioLogs { get; private set; }

        static Log()
        {
            DiretorioLogs = Path.Combine(AppContext.BaseDirectory, "logs");

            CriarDiretorioLogCasoNaoExista(DiretorioLogs);
        }
        
        public static void GravarLog(Exception exception)
        {
            String arquivo = Path.Combine(DiretorioLogs, "Log.txt");

            using (StreamWriter writer = new StreamWriter(arquivo, true))
            {
                StringBuilder builder = new StringBuilder();

                builder.Append(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                builder.Append(" ===> ");

                builder.Append(exception.ToString());

                writer.WriteLine(builder.ToString());

                writer.Flush();
            }
        }

        private static void CriarDiretorioLogCasoNaoExista(String pastaLog)
        {
            if (!Directory.Exists(pastaLog))
                Directory.CreateDirectory(pastaLog);
        }
    }
}
