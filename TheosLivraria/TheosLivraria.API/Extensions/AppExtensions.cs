using TheosLivraria.API.Endpoint;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Seed;

namespace TheosLivraria.API.Extensions
{
    public static class AppExtensions
    {
        public static void ConfiguracaoAmbienteDesenvolvimento(this WebApplication app)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TheosApplicationDbContext>();
                UsuarioRootSeed.Excute(context);
            }
        }

        public static void ConfiguracaoSeguranca(this WebApplication app)
        {
            
            
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void ConfiguracaoAplicacao(this WebApplication app)
        {
            app.MapEndpoints();
        }
    }
}
