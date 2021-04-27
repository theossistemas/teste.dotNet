Estou enviando os comandos para executar a migritions, caso seja preciso.

gera migrations do API=> C:\Users\lpith\source\theo\teste.dotNet\Livraria.Infra\Livraria.Infra.Data>dotnet ef migrations add InitialCreate -s ..\..\livraria-api\livraria-api.csproj
execute DB => C:\Users\lpith\source\theo\teste.dotNet\Livraria.Infra\Livraria.Infra.Data>dotnet ef database update -s ..\..\livraria-api\livraria-api.csproj

gera migrations da SEGURANÇA => dotnet ef migrations add InitialCreate -s ..\..\livraria-api\livraria-api.csproj -c SecurityDbContext
generate migrations => dotnet ef database update -s ..\..\livraria-api\livraria-api.csproj -c SecurityDbContext