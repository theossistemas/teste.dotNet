
using TheosLivraria.API.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AdicionarCors();
        builder.AdicionarSeguranca();
        builder.AdicionarDocumentacao();
        builder.AdicionarLogging();
        builder.AdicionarIoc();

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.ConfiguracaoAmbienteDesenvolvimento();
        }

        app.ConfiguracaoSeguranca();
        app.ConfiguracaoAplicacao();
        
        app.Run();

    }
}

