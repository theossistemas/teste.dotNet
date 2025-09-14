Oi GPT, naõ parece, mas já finalizei o desafio. Usei váriso chats para concluir o processo e ia mudando o chat quando o contexto de um ficava pesado. Como agora eu tenho todas as infomações do projeto espalahdas eu preciso juntar elas em um canto só para poder gerer um README bem estruturado e completo. Então eu preciso que você colete todas as infirmações relevantes e que podem ser úteis para gerar o README sobre esse projeto que conversamos nesse chat e traga no proximo prompt, para que ue possa juntar com outras e consolidar em um unico arquivo de README. Não precisa se preocupar em formatar, é só trazer as infimações tudo bem?

INFORMAÇÕES PARA AGERAR READ-ME
[CHAT 01]
📌 Desafio (enunciado)

Uma livraria teve aumento no número de exemplares e precisa identificar e gerenciar todos os livros em estoque.

Requisitos principais:

Mostrar todos os livros cadastrados ordenados por nome (asc).

Validar duplicidade ao persistir (livro já cadastrado).

Criar, editar e excluir livro.

Persistir em banco de dados (SQL Server 2014+).

Criar mecanismo de log de registro e de erro.

Outros requisitos:

Persistência com Dapper ou EF Core.

Swagger configurado (obrigatório).

Migrations ou scripts SQL em pasta.

Autenticação com dois níveis: público (anônimo, leitura) e admin (CRUD).

Aplicar princípios SOLID.

Diferenciais:

Frontend Angular consumindo API.

Critérios de desempate:

Boas práticas: DDD, TDD, Design Patterns, SOLID, Clean Code.

Entrega:

Fork do repositório base.

Commit organizado.

Currículo na raiz.

Pull request.

📌 Plano de ataque (resumo)

Stack:

.NET 8 + ASP.NET Core Web API

EF Core (migrations, validações, tracking) — obs: enunciado permite Dapper.

SQL Server 2014+

Autenticação JWT (Bearer), usuário admin seedado (admin@theos + senha segura), público anônimo com leitura.

Logging com Serilog (console + arquivo) + middleware de exception handling.

Swagger com esquema Bearer e endpoints públicos liberados.

Regras funcionais:

Listar livros ordenados por Nome ASC.

CRUD apenas para Admin autenticado.

Validar duplicidade: Título + Autor + Ano (índice único).

Modelo de domínio mínimo:

Book (Id, Title, Author, Year, Isbn?, CreatedAt).

Title obrigatório (max 200), Author obrigatório (max 150), Year opcional (>=1450, <= ano atual).

Isbn opcional, único se informado.

Entidade com método Update(...) validando entradas.

Arquitetura (DDD enxuto):

/src
  BookStore.Api            -> controllers, auth, swagger, DI
  BookStore.Application    -> DTOs, use cases, contracts
  BookStore.Domain         -> entidades, regras, interfaces (repos)
  BookStore.Infrastructure -> EFCore, Migrations, Repos, Logging

/tests
  BookStore.Tests          -> unitários + alguns de integração


Princípios aplicados (SOLID):

SRP: separação de camadas.

DIP: IBookRepository no domínio, implementação EF na infra.

OCP: validações como services/validators extensíveis.

Endpoints:

Público (AllowAnonymous)

GET /api/books?search=&page=&pageSize= (ordenado por título ASC, com paginação e filtro opcional).

GET /api/books/{id}

Admin (Authorize Role=Admin)

POST /api/books

PUT /api/books/{id}

DELETE /api/books/{id}

Auth:

POST /api/auth/login → retorna JWT.

Persistência (EF Core):

DbContext com Books.

Migrations + scripts exportados em /db/scripts.

Índices:

HasIndex(b => new { b.Title, b.Author, b.Year }).IsUnique().

HasIndex(b => b.Isbn).IsUnique() se não nulo.

Logging & Erros:

Serilog: Info para requests, Error para exceções.

Middleware global padronizando erros (traceId, message, details em Dev).

Auditoria: registrar userId, ação e bookId em operações CRUD.

Swagger:

Modelos request/response documentados.

Códigos de status (200/201/400/401/403/404/409/500).

Security Definition Bearer + botão Authorize.

TDD mínimo:

Domínio:

Book_Should_Throw_When_Title_Empty().

BookRepository_Should_Enforce_Unique_TitleAuthorYear().

Aplicação:

CreateBookHandler_Should_Return_Conflict_On_Duplicate().

ListBooksHandler_Should_Return_Sorted_By_Title_Asc().

API:

GET /api/books retorna ordenação correta.

POST /api/books sem token → 401; com token admin → 201.

Entrega:

README com requisitos, como rodar (dotnet ef database update, connection string).

Usuário admin (email/senha).

Como gerar token e usar Swagger.

Decisões arquiteturais (EF vs Dapper).

Instruções Angular (se bônus feito).

Diferenciais possíveis:

Front Angular.

FluentValidation nos DTOs.

Filtros opcionais com Specification.

Docker Compose (SQL Server + API).

Health Checks (/health).

📌 Entidade Livro (implementada)
public class Livro
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int? AnoPublicacao { get; private set; }
    public string? ISBN { get; private set; }
    public string? Editora { get; private set; }
    public DateOnly DataPublicacao { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    public Livro(string titulo, string autor, int? anoPublicacao,
        string? isbn, string? editora, DateOnly dataPublicacao,
        decimal preco, int estoque) { ... }

    public void Update(string titulo, string autor, int? anoPublicacao,
        string? isbn, string? editora, DateOnly dataPublicacao,
        decimal preco, int estoque) { ... }
}


Validações: título e autor obrigatórios, ano >=1450, preço e estoque não negativos, data não futura.

📌 DTOs (versão original antes do ajuste)

AtualizarLivroDto:

Tinha int Id (incompatível com Guid).

Incluía campo Genero que não existe na entidade.

Não tinha DataPublicacao, ISBN, Estoque.

CriarLivroDto:

Já alinhado com entidade (Titulo, Autor, AnoPublicacao, ISBN, Editora, DataPublicacao, Preco, Estoque).

[CHAT 02]
Stack & Decisões

Plataforma: .NET 8 + ASP.NET Core Web API

ORM: Entity Framework Core 8 (poderia ser Dapper também, mas EF foi escolhido pelo tempo/velocidade)

Banco de dados: SQL Server 2014+

Autenticação: JWT (Bearer) com um usuário Admin seedado (login via endpoint)

Perfis de acesso:

Público (anônimo): somente leitura

Admin (autenticado): CRUD completo

Logging: Serilog (console + arquivo), middleware de tratamento global de exceções

Documentação: Swagger com esquema de segurança Bearer e endpoints públicos liberados

Estrutura de Pastas (DDD enxuto)
/src
  LivrariaTheos.Api             -> controllers/endpoints, auth, swagger, DI
  LivrariaTheos.Application     -> DTOs, use cases/handlers, contracts
  LivrariaTheos.Domain          -> entidades, regras de negócio e interfaces
  LivrariaTheos.Infrastructure  -> EF Core, Migrations, Repositories, Logging
/tests
  LivrariaTheos.Tests           -> testes unitários (domínio, aplicação) e alguns de integração
/db
  scripts.sql                   -> scripts de migrations (exportados)

Entidade Principal (Livro)

Propriedades:

Id

Titulo (string, obrigatório, máx. 200)

Autor (string, obrigatório, máx. 150)

Ano (int?, opcional, >= 1450 e <= ano atual)

Isbn (string?, opcional, único se preenchido)

CreatedAt

Regras:

Unicidade: (Titulo, Autor, Ano) deve ser único

Índices únicos:

HasIndex(b => new { b.Titulo, b.Autor, b.Ano }).IsUnique()

HasIndex(b => b.Isbn).IsUnique() (quando não nulo)

Regras Funcionais

Listar livros ordenados por título ASC (endpoint público)

Criar/Editar/Excluir apenas se usuário for Admin

Validar duplicidade de livros antes de persistir

Logs de auditoria: ao criar/editar/excluir, registrar userId, ação, bookId

Endpoints

Público (AllowAnonymous)

GET /api/books?search=&page=&pageSize= → lista ordenada por título ASC, paginação, filtro opcional

GET /api/books/{id}

Admin (Authorize Role=Admin)

POST /api/books → cria (valida índice único, retorna 201 com Location)

PUT /api/books/{id} → edita (valida duplicidade)

DELETE /api/books/{id} → exclui

Auth

POST /api/auth/login → retorna JWT (seed: admin@theos / senha segura)

Swagger

Configurado com:

Security Definition: Bearer

Botão “Authorize” para inserir token

Documentação dos modelos de request/response

Documentação de códigos de resposta (200/201/400/401/403/404/409/500)

Logging & Erros

Serilog configurado para:

Information: requests e eventos

Error: exceções

Middleware global de exception handling

Resposta padronizada com traceId, message, details (detalhes só em Dev)

TDD (mínimo viável)

Domínio

Book_Should_Throw_When_Title_Empty()

BookRepository_Should_Enforce_Unique_TitleAuthorYear() (teste integração com SQLite in-memory ou SQL local)

Aplicação

CreateBookHandler_Should_Return_Conflict_On_Duplicate()

ListBooksHandler_Should_Return_Sorted_By_Title_Asc()

API

GET /api/books retorna ordenação correta

POST /api/books sem token → 401; com token admin → 201

Configurações técnicas

appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LivrariaTheos;Trusted_Connection=True;TrustServerCertificate=True"
}


Program.cs:

builder.Services.AddDbContext<LivrariaTheosDbContext>(...)

builder.Services.AddEndpointsApiExplorer()

builder.Services.AddSwaggerGen()

JWT Authentication + Authorization

Serilog configurado

Middleware global de exceção

Migrations:

dotnet ef migrations add InitialCreate --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api

dotnet ef database update --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api

Pacotes Principais

Api

Microsoft.AspNetCore.Authentication.JwtBearer (8.0.*)

Microsoft.AspNetCore.OpenApi (8.0.*)

Swashbuckle.AspNetCore (6.6.*)

Microsoft.EntityFrameworkCore.Design (8.0.*) (se migrations)

Infrastructure

Microsoft.EntityFrameworkCore (8.0.*)

Microsoft.EntityFrameworkCore.SqlServer (8.0.*)

Entrega do Desafio (Theòs)

Requisitos obrigatórios:

Mostrar livros ordenados por nome

CRUD de livros com validação de duplicidade

Persistência em banco SQL Server

Logs de registro e erro

Swagger configurado

Autenticação com dois níveis de acesso (admin/público)

Usar Dapper ou EF Core (foi escolhido EF Core)

Migrations ou scripts disponíveis em /db

Diferenciais possíveis:

Frontend em Angular (não implementado aqui, mas previsto no plano)

Uso de SOLID, DDD, TDD, Clean Code

Docker Compose com SQL Server + API

Health Checks (/health) e versionamento da API

[CHAT 03]

Projeto: LivrariaTheos
Contexto do Desafio

Criar uma aplicação web para gerenciar livros de uma livraria que aumentou seu estoque.

Requisitos principais:

Mostrar livros cadastrados ordenados ascendente por nome.

Validar se o livro já foi cadastrado (duplicidade).

Permitir criar, editar e excluir livros.

Persistir em banco de dados (SQL Server 2014+).

Criar mecanismo de log e registro de erros.

Configurar Swagger.

Autenticação com dois níveis:

Público (sem autenticação, apenas leitura).

Admin (CRUD autenticado via JWT).

Critério de desempate: boas práticas (DDD, SOLID, TDD, Design Patterns, Clean Code).

Stack e Decisões

.NET 8 + ASP.NET Core Web API

EF Core (com migrations + scripts SQL exportados)

SQL Server 2014+

Autenticação via JWT (usuário Admin seedado: admin@theos)

Logs: Serilog (console + arquivo), middleware global de exceptions

Swagger configurado em PT-BR, com esquema Bearer e instruções

Arquitetura DDD enxuta:

LivrariaTheos.Api → controllers, auth, swagger, DI

LivrariaTheos.Application → DTOs, handlers, contracts

LivrariaTheos.Domain → entidades, regras e interfaces

LivrariaTheos.Infrastructure → EF Core, Migrations, Repos, Logging

LivrariaTheos.Tests → testes unitários e integração

Modelo de Domínio

Entidade Livro

public class Livro
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int? AnoPublicacao { get; private set; }
    public string? ISBN { get; private set; }
    public string? Editora { get; private set; }
    public DateOnly DataPublicacao { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    private Livro() { }

    public Livro(string titulo, string autor, int? anoPublicacao, string? isbn, string? editora, DateOnly dataPublicacao, decimal preco, int estoque) { ... }

    public void Update(string titulo, string autor, int? anoPublicacao, string? isbn, string? editora, DateOnly dataPublicacao, decimal preco, int estoque) { ... }
}


Validações:

Titulo obrigatório (max 200).

Autor obrigatório (max 150).

AnoPublicacao opcional, entre 1450 e ano atual.

ISBN e Editora opcionais (com limite de tamanho).

Preco e Estoque obrigatórios, não negativos.

Persistência

LivrariaTheosDbContext com DbSet<Livro> Livros.

Índices configurados:

(Titulo, Autor, AnoPublicacao) único.

ISBN único quando não nulo.

Endpoints
Público (sem autenticação)

GET /api/livros?busca=&page=&pageSize=
→ lista paginada, ordenada por Titulo ASC, filtro opcional por título/autor.

GET /api/livros/{id} → retorna um livro.

Admin (JWT, Role=Admin)

POST /api/livros → cria (retorna 201 com Location).

PUT /api/livros/{id} → edita (retorna 204).

DELETE /api/livros/{id} → exclui (retorna 204).

Duplicidade validada antes de persistir → retorna 409.

Autenticação

POST /api/auth/login

body: { "email": "admin@theos", "senha": "Admin#2024!" }

retorna JWT com claim Role=Admin.

Swagger com botão Authorize para inserir token.

Logs e Erros

Serilog → grava em console e arquivo logs/api-.log.

Middleware global captura exceções e retorna JSON com traceId e mensagem padrão.

Scripts e Migrations

Migrations geradas via dotnet ef.

Script SQL exportado em /db/scripts/initial.sql (cumprindo exigência do desafio).

Testes (mínimo viável)

Domínio:

Livro_Should_Throw_When_Titulo_Empty()

Livro_Should_Throw_When_AnoPublicacao_Out_Of_Range()

Integração:

LivroRepository_Should_Enforce_Unique_TituloAutorAno() (SQLite in-memory).

ListLivros_Should_Return_Sorted_By_Titulo_Asc().

API:

GET /api/livros → deve retornar lista ordenada.

POST /api/livros sem token → 401.

POST /api/livros com token válido → 201.

Diferenciais Implementáveis

FluentValidation nos DTOs (ex: CriarLivroDto, AtualizarLivroDto).

Health Checks em /health.

Docker Compose (API + SQL Server Linux).

Front Angular (público: listagem; admin: login + CRUD).

[CHAT 04]

Coleta de informações sobre o projeto (do chat atual)

Nome da solução: LivrariaTheos

Arquitetura:

ASP.NET Core Web API (.NET 8)

Projetos: LivrariaTheos.Api, LivrariaTheos.Domain, LivrariaTheos.Application, LivrariaTheos.Infrastructure, LivrariaTheos.Tests

Camadas:

Api → controllers/endpoints, auth, swagger, DI

Application → DTOs, use cases/handlers, contratos

Domain → entidades, regras, interfaces

Infrastructure → EF Core, repositórios, migrations, logging

Tests → unitários (domínio e aplicação), integração mínima

Stack & bibliotecas:

ASP.NET Core 8

EF Core 8 + Migrations

SQL Server Express (2014+)

Autenticação JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer)

Autorização baseada em Role (Admin)

Serilog (Serilog.AspNetCore, Serilog.Sinks.File)

Swagger (Swashbuckle.AspNetCore)

Requisitos atendidos (do enunciado):

Listar livros ordenados de forma ascendente pelo nome.

Validar duplicidade ao persistir (Titulo + Autor + AnoPublicacao único, ISBN único se não nulo).

CRUD completo de livros.

Diferentes níveis de acesso:

Público (GET livros anônimo).

Admin (CRUD protegido por JWT Role=Admin).

Persistência em banco de dados (SQL Server Express, EF Core).

Logs de registro e de erro (Serilog, middleware de exceções).

Swagger configurado com esquema de segurança Bearer.

Migrations + script SQL exportado para pasta db/scripts.

Entidade Livro (Domain):

Propriedades: Id, Titulo, Autor, AnoPublicacao, ISBN (opcional), Editora, DataPublicacao, Preco, Estoque.

Restrições:

Titulo obrigatório, max 200.

Autor obrigatório, max 150.

ISBN opcional, max 20, único se não nulo.

Editora opcional, max 100.

DataPublicacao obrigatória (mapeada como date).

Preco decimal(18,2), não negativo.

Estoque int, não negativo.

Método Update(...) para atualização.

Índices criados no banco:

Índice único em (Titulo, Autor, AnoPublicacao).

Índice único condicional em ISBN.

Índice auxiliar em Titulo (para ordenação/paginação).

Check constraints: Preco >= 0, Estoque >= 0.

Controllers:

AuthController: login (/api/auth/login), retorna JWT com Role=Admin.

LivrosController: rota /api/livros, GET público (ordenado ASC), CRUD protegido com policy Admin, checagem de duplicidade retorna 409.

Program.cs:

Configuração Serilog (console + arquivo logs/api-.log).

Middleware global de erro com traceId.

Autenticação + Autorização JWT.

Swagger com Bearer e descrição em PT-BR.

Configuração do DbContext com connection string "DefaultConnection".

Connection string (SQL Server Express):

Definida em appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=LivrariaTheos;User Id=sa;Password=Wil@5678#Cle;TrustServerCertificate=True;MultipleActiveResultSets=True"
}


Chave JWT em appsettings.json:

"Jwt": { "Key": "LivrariaTheos_Desafio2025@SuperSecretKey" }


Comandos importantes:

Criar migration inicial:

dotnet ef migrations add Initial --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api


Atualizar banco:

dotnet ef database update --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api


Gerar script SQL:

dotnet ef migrations script --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api -o db/scripts/initial.sql


Rodar a API:

dotnet run --project src/LivrariaTheos.Api


Banco de dados:

Criado banco LivrariaTheos no SQL Server Express.

Tabelas:

Livros (colunas conforme entidade).

__EFMigrationsHistory (controle do EF Core).

Testes (planejados/mínimos):

Domínio:

Book_Should_Throw_When_Title_Empty()

Book_Should_Throw_When_Year_Out_Of_Range()

Integração:

Ordenação ASC no GET anônimo.

409 ao tentar inserir duplicado (Titulo, Autor, AnoPublicacao).

API:

GET /api/livros retorna lista ordenada.

POST /api/livros sem token → 401.

POST /api/livros com token Admin → 201.

Smoke test no Swagger (checklist):

POST /api/auth/login → pegar token.

GET /api/livros → lista vazia, ordenada por título.

POST /api/livros → inserir livro.

POST /api/livros repetindo → 409 (duplicado).

Conferir logs (logs/api-*.log).

Entrega (segundo desafio):

Fork do repositório.

Implementação concluída.

Currículo na raiz.

Pull request para avaliação.

Diferenciais (planejados, mas não obrigatórios):

FluentValidation nos DTOs.

Health check /health.

Versionamento da API.

Docker Compose (API + SQL Server).

Frontend Angular simples (listagem, login, CRUD).

[CHAT 05]

Desafio recebido

Desenvolver aplicação web para gerenciar exemplares de uma livraria.

Requisitos principais:

Mostrar livros cadastrados ordenados de forma ascendente pelo nome.

Validar duplicidade ao persistir (livro já cadastrado).

Permitir criar, editar e excluir livro.

Persistência em banco de dados (Dapper ou EF Core).

Log de registro e erro.

Swagger configurado.

Banco: SQL Server 2014 ou superior.

Usar migrations ou gerar scripts e disponibilizá-los em uma pasta.

Observações:

Usar .NET Core (preferência 8.0).

Autenticação com dois níveis de acesso: administrador (autenticado) e público (sem autenticação).

Aplicar princípios SOLID.

Diferenciais:

Front-end em Angular para consumir a API.

Avaliação extra: DDD, TDD, Design Patterns, SOLID, Clean Code.

Entrega: fork do repositório, teste realizado, currículo na raiz, PR para avaliação.

Stack e decisões

ASP.NET Core .NET 8 Web API.

EF Core para persistência (agilidade com migrations, validações, tracking).

SQL Server Express (2014+).

JWT (Bearer) para autenticação.

Um usuário Admin seedado via endpoint de login.

Perfil público anônimo para leitura.

Serilog para logging (console + arquivo).

Middleware global de exception handling.

Swagger em PT-BR com esquema de segurança Bearer.

Arquitetura em camadas (DDD enxuto):

Api → controllers, endpoints, auth, swagger, DI.

Application → DTOs, use cases, contracts.

Domain → entidades, regras, interfaces.

Infrastructure → EFCore, migrations, repos, logging.

Tests → unitários e integração.

Entidade Livro (Book)

Propriedades mínimas: Id, Titulo, Autor, AnoPublicacao, ISBN?, CreatedAt.

Validações:

Título obrigatório (máx 200).

Autor obrigatório (máx 150).

Ano ≥ 1450 e ≤ ano atual.

ISBN opcional (único se preenchido).

Regras:

Unicidade: (Título + Autor + Ano).

Entidade com método Update(...) para validação básica.

Controllers

AuthController:

Rota /api/auth/login.

Login com admin@theos / Admin#2024!.

Retorna JWT com claim de role Admin, expira em 8h.

LivrosController:

Rota /api/livros.

Endpoints públicos [AllowAnonymous]:

GET /api/livros (ordenado ASC por título, suporta busca e paginação).

GET /api/livros/{id}.

Endpoints restritos [Authorize(Policy="Admin")]:

POST → criar livro (valida duplicidade).

PUT → atualizar (valida duplicidade).

DELETE → excluir.

Retornos padrão:

201 Created no POST.

204 NoContent no PUT/DELETE.

409 Conflict se duplicado.

404 NotFound se inexistente.

Program.cs

Configuração do Serilog (console + arquivo logs/api-.log).

Configuração do DbContext com SQL Server.

Autenticação JWT:

Key vinda do appsettings.json (Jwt:Key, fallback “dev-secret-change-me”).

RequireRole("Admin") na policy “Admin”.

Swagger configurado com esquema Bearer e requirement global.

Middleware de erros (retorna JSON com traceId).

Ordem do pipeline:

UseSerilogRequestLogging().

UseAuthentication().

UseAuthorization().

MapControllers().

Swagger habilitado em ambiente Development.

Banco de Dados e Migrations

DbContext configurado com:

Índice único em (Titulo, Autor, AnoPublicacao).

Índice único condicional em ISBN (IS NOT NULL).

Migration inicial criada: 20250913112934_Initial.

Scripts gerados:

db/scripts/initial.sql → não idempotente.

db/scripts/initial-idempotent.sql → seguro para múltiplas execuções.

Como aplicar:

Criar DB: sqlcmd -S .\SQLEXPRESS -Q "IF DB_ID('LivrariaTheos') IS NULL CREATE DATABASE LivrariaTheos;"

Aplicar: sqlcmd -S .\SQLEXPRESS -d LivrariaTheos -i db\scripts\initial.sql.

Funcionalidades entregues

Login → gera token JWT.

GET livros (público).

POST livros (Admin).

PUT livros (Admin).

DELETE livros (Admin).

Validação de duplicidade.

Logs em arquivo.

Swagger funcional em http://localhost:xxxx/swagger
.

Scripts de migrations exportados.

Próximos diferenciais possíveis

FluentValidation nos DTOs (CreateLivroDto, UpdateLivroDto).

Health check /health.

Docker Compose (API + SQL Server Linux).

Front Angular simples (público + admin).

Testes automatizados mínimos:

Domínio: título obrigatório, ano fora da faixa.

Integração: unicidade e ordenação ASC.