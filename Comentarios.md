# Descrição

Aplicação DDD Full Layer com .NET Core 3.1 e C#

## Instalação

```
- Abrir projeto no Visual Studio e restaurar pacotes
- Configurar ambiente no appsettings.Development.json do projeto "Services.AspNetWebApi"
- Executar Migrations para Dbcontexts dos projetos de "Infra.Data" e "Infra.CrossCutting.Identity":
```

## Arquitetura e conceitos
```
SOLID
Clean Code
DDD – Domain Driven Design
CQRS – Command Query Segregation Responsibility
Event Sourcing
Domínios Ricos
Domain Notification
Repository Pattern
Unit Of Work
Mediator
Fast Fail Validation
Testes Unitários
```

## Technologias e recursos

* .NET Core 3.1 e C#
* SQL Server 2017 v14 (X64) 
* Microsoft Identity
* Custom Claims/Polices Authorizations
* JWT - Jason Web Tokens
* Entity Framework Core / Code First
* Migrations
* Fluent API
* Soft Delete com EF Core e Global Query Filters
* Documentação da API com Swagger
* Versionamento da API
* Otimização da resposta da API com Gzip Compression and Cache
* ASP.NET Core Native Dependency Injection
* Logging com KissLog provider
* AutoMapper
* MediatR
* FluentValitation
* xUnit
* Bogus Fake Data for .NET


## Kiss Log provider

```
- Criar conta gratuita e configurar novo app em https://kisslog.net/
- Atualizar valores no appsettings.json com os dados do KissLog app:
  "KissLog.OrganizationId" 
  "KissLog.ApplicationId"
```
Exemplos:
![Alt text](files/kisslog_dashboard.PNG?raw=true "KissLog")

![Alt text](files/kisslog.PNG?raw=true "KissLog") 

![Alt text](files/kisslog_log.PNG?raw=true "KissLog") 

## Executar EF Migrations
```
- Configurar ConnectionStrings no arquivo appsettings.Development.json ou manter o mesmo para utilizar localdb
- Selecionar StartUp Project e Default Project (Package Manager Console) para "MMM.Library.Infra.Data"
- Executar comando no Package Manager Console: update-database -c LibraryDbContext
- Selecionar StartUp Project e Default Project (Package Manager Console) para "MMM.Library.Infra.CrossCutting.Identity"
- Executar comando no Package Manager Console: update-database -c ApplicationDbContext
```
