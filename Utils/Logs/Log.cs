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
                writer.WriteLine(FormatarMensagemLog(exception.ToString()));

                writer.Flush();
            }
        }

        private static void CriarDiretorioLogCasoNaoExista(String pastaLog)
        {
            if (!Directory.Exists(pastaLog))
                Directory.CreateDirectory(pastaLog);
        }

        public static void ConsoleLog(String message)
        {
            Console.WriteLine(FormatarMensagemLog(message));
        }

        private static String FormatarMensagemLog(String mensagem)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            builder.Append(" ===> ");

            builder.Append(mensagem);

            builder.AppendLine();

            return builder.ToString();
        }
    }
}
