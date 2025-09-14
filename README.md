# 📚 LivrariaTheos — Sistema de Gerenciamento de Livros

---

## 1️⃣ Visão Geral

Aplicação desenvolvida como parte do desafio técnico da Théos, para gerenciar exemplares de uma livraria.  
O projeto foi construído em dois módulos principais:

- **Backend**: **.NET 8 + ASP.NET Core Web API**, persistência em **SQL Server** e autenticação via **JWT**.  
- **Frontend**: **Angular 17/20 standalone**, integrado à API via proxy, com autenticação JWT, guards de rota e interceptors.  

**Status atual:** todos os requisitos obrigatórios concluídos, com frontend implementado.  
**Próximos passos:** melhorias adicionais de UX, testes automatizados e containerização.

---

## 2️⃣ Arquitetura do Projeto

- **Backend**
  - Padrão **DDD enxuto** com separação clara em camadas.  
  - Persistência com **EF Core**, utilizando índices únicos para reforçar regras de negócio.  
  - Autenticação via **JWT**, com um usuário administrador seedado.  
  - **Logging centralizado** com Serilog e middleware global para tratamento de erros.

- **Frontend**
  - Estrutura **Angular standalone** (sem NgModules), com menor boilerplate e alinhado às práticas modernas.  
  - Guards de rota (`authGuard`, `adminGuard`) para proteger rotas privadas.  
  - Interceptores para anexar token de autenticação e tratar erros (401/403).  
  - Integração transparente com a API via **proxy (`/api`)**.

---

## 3️⃣ Requisitos Atendidos

- ✅ Listagem de livros ordenada por título (ASC).  
- ✅ CRUD completo de livros.  
- ✅ Validação de duplicidade (Título + Autor + Ano).  
- ✅ Persistência em SQL Server 2014+.  
- ✅ Logging e padronização de erros.  
- ✅ Swagger configurado.  
- ✅ Autenticação com dois níveis:  
  - Público (anônimo) → leitura.  
  - Admin (JWT) → CRUD.  
- ✅ Frontend funcional: login, listagem e criação de livros com rotas protegidas.

---

## 4️⃣ Estrutura de Pastas

```
/src
  LivrariaTheos.Api            -> Controllers, Auth, Swagger, DI
  LivrariaTheos.Application    -> DTOs, Use Cases/Handlers, Contracts
  LivrariaTheos.Domain         -> Entidades, Regras, Interfaces
  LivrariaTheos.Infrastructure -> EF Core, Repos, Migrations, Logging

/tests
  LivrariaTheos.Tests          -> (planejado) Unit/Integration

/db
  scripts/initial.sql          -> Script inicial do schema

/forntend/livraria-theos-web
  app/
    core/ (services, interceptors, models)
    features/ (auth, livros, home)
    app.component.*, app.routes.ts
  environments/
  proxy.conf.json
```

---

## 5️⃣ Backend — Como Rodar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- SQL Server 2014+ (pode ser Express)  
- (Opcional) Docker Desktop  

### Configuração
Arquivo `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=LivrariaTheos;User Id=sa;Password=SUASENHA;TrustServerCertificate=True;MultipleActiveResultSets=True"
},
"Jwt": {
  "Key": "LivrariaTheos_Desafio2025@SuperSecretKey"
}
```

### Banco de Dados
- **Migrations**
  ```sh
  dotnet ef database update --project src/LivrariaTheos.Infrastructure --startup-project src/LivrariaTheos.Api
  ```
- **Scripts**
  ```sh
  sqlcmd -S .\SQLEXPRESS -d LivrariaTheos -i db/scripts/initial.sql
  ```

### Executar API
```sh
dotnet run --project src/LivrariaTheos.Api
```
Swagger: [http://localhost:5122/swagger/index.html](http://localhost:5122/swagger/index.html)

---

## 6️⃣ Frontend — Como Rodar

### Pré-requisitos
- [Node.js](https://nodejs.org) (versão 20+)  
- npm (instalado junto com Node)  

### Executar
```sh
cd forntend/livraria-theos-web
npm install
npm start --proxy-config proxy.conf.json
```

➡️ Acesse [http://localhost:4200](http://localhost:4200)  

### Observações
- `environment.development.ts` define `apiBaseUrl: '/api'`.  
- O proxy redireciona `/api` para `http://localhost:5122`.  
- Login com usuário seed da API:  
  - Email: `admin@theos`  
  - Senha: `Admin#2024!`  

---

## 7️⃣ Autenticação & Fluxo de Acesso

- **Backend**
  - Usuário admin seedado.  
  - JWT configurado com role `Admin`.  

- **Frontend**
  - `authGuard`: exige login.  
  - `adminGuard`: exige role Admin.  
  - Interceptor de Auth: anexa token Bearer (ignora `/Auth/login`).  
  - Interceptor de Erro: trata `401` (logout + redirect) e `403` (alerta).  

---

## 8️⃣ Modelo de Domínio

| Campo           | Tipo      | Regras                                 |
|-----------------|-----------|----------------------------------------|
| Id              | Guid      |                                        |
| Titulo          | string    | obrigatório, máx 200                   |
| Autor           | string    | obrigatório, máx 150                   |
| AnoPublicacao   | int?      | 1450..ano atual                        |
| ISBN            | string?   | único se informado                     |
| Editora         | string?   | opcional                               |
| DataPublicacao  | DateOnly  | obrigatório                            |
| Preco           | decimal   | ≥0                                     |
| Estoque         | int       | ≥0                                     |

**Índices**
- Único: (Titulo, Autor, AnoPublicacao).  
- Único condicional: ISBN (quando não nulo).  

---

## 9️⃣ Logging & Erros

- **Backend**
  - Serilog (console + arquivo).  
  - Middleware global padronizando respostas JSON (`traceId`, `message`, `details`).  
- **Frontend**
  - Interceptor de erro → feedback imediato em 401/403.  

---

## 🔟 Decisões Arquiteturais

- **EF Core > Dapper**: por eu ter mais domínio no EF Core, resolvi utilizá-lo para otimizar o tempo de produção da aplicação.  
- **Angular standalone**: escolhido por reduzir boilerplate (sem NgModules), facilitar manutenção e estar alinhado às boas práticas modernas recomendadas pela comunidade Angular.  
- **Proxy Angular**: simplifica integração, evitando problemas de CORS.  
- **SOLID aplicado**: SRP, DIP, OCP respeitados em backend e frontend.  
- **DDD enxuto**: separação em camadas garantindo clareza arquitetural.  

---

## 1️⃣1️⃣ Propostas de Implementação Adicional

- Backend:  
  - Testes automatizados (unitários e integração).  
  - Health checks em `/health`.  
  - Docker Compose (API + SQL Server).  

- Frontend:  
  - UX melhorada (feedback visual, loaders, toasts).  
  - Paginação e busca avançada.  

- Infraestrutura:  
  - CI/CD com GitHub Actions.  
  - Deploy em nuvem (Azure/AWS).  

---

## 1️⃣2️⃣ Entrega

- Fork do repositório base → implementado no repo `teste.dotNet`.  
- Currículo adicionado na raiz.  
- Pull Request enviado para avaliação.  

---

## 1️⃣3️⃣ Créditos

Desenvolvido por **Wilkson Colares**  
[GitHub](#) | [LinkedIn](#)
