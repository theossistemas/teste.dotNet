## Desafio 1

Uma livraria da cidade teve um aumento no número de seus exemplares e está com um problema para identificar todos os livros que possui em estoque. 
Para ajudar a livraria foi solicitado a você desenvolver uma aplicação web para gerenciar estes exemplares.Requisitos


* O sistema deverá mostrar todos os livros cadastrados ordenados de forma ascendente pelo nome.
* Ao persistir, validar se o livra já foi cadastrado.
* O sistema deverá permitir criar, editar e excluir um livro.
* Os livros devem ser persistidos em um banco de dados.
* Criar algum mecanismo de log de registro e de erro.

#### Outros Requisitos:
* Para a persistência dos dados deve ser utilizado o Dapper ou EF Core.
* Configurar o Swagger na aplicação(fundamental)
* Usar Microsfot SqlServer 2014 ou superior.
* Utilizar migrations ou Gerar Scripts e disponibilizá-los em uma pasta.

#### Observações:
* O sistema deverá ser desenvolvido na plataforma .NET com C#, usando o framework ASP.NET CORE 
	(preferêncialmente 8.0, caso for usado outra versão, informar no pull-request)
* Deve conter autenticação com dois níveis de acesso, um administrador e um público, o usuário de nível 
	público não terá autenticação, ou seja, terá acesso livre.
* Atenção aos princípio do SOLID.


#### Diferencial do desafio 1:
* Implementar front-end para consumir a API em  Angular como framework Javascript.
* obs: Teste terá como avaliação principal os requisitos solicitados para o backend,  porém o frontend 
    poderá ser critério de desempate.
     

##
#### Critério de desempate

- Aplicação das boas práticas do DDD, TDD, Design Patterns, SOLID e Clean Code.


## Como deverá ser entregue:

    1. Faça um fork deste repositório;
    2. Realize o teste;
    3. Adicione seu currículo na raiz do repositório;
    4. Envie-nos o PULL-REQUEST para que seja avaliado;


 # Projeto Livraria Full-Stack (ASP.NET Core + Angular)

> Aplicação web para gerenciamento de um inventário de livraria, desenvolvida como um projeto full-stack. O backend foi construído com ASP.NET Core 8 e o frontend com Angular, implementando um sistema de autenticação JWT com controle de acesso baseado em funções.

## Tecnologias Utilizadas

### Backend
* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Autenticação JWT (JSON Web Token)**
* **Swagger/OpenAPI**

### Frontend
* **Angular 17+**
* **TypeScript**
* **Angular Material** (Para componentes de UI)
* **Angular Reactive Forms**
* **Angular Router**

## Pré-requisitos

Antes de começar, garanta que você tem as seguintes ferramentas instaladas na sua máquina:
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server 2014](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou superior
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) ou VS Code com C# Dev Kit.
* [Node.js](https://nodejs.org/en/) (Versão LTS 18.x ou 20.x recomendada)
* [Angular CLI](https://angular.io/cli) instalado globalmente (`npm install -g @angular/cli`)

## Como Executar o Projeto

O projeto está dividido em duas pastas: `backend` e `frontend`. Siga os passos abaixo em ordem.

### 1. Backend (.NET Core API)

A API é o cérebro da aplicação e precisa estar rodando para que o frontend funcione.

1.  **Configurar o Banco de Dados:**
    * Abra o arquivo `appsettings.json` na pasta do backend.
    * Localize a `ConnectionString` e a ajuste para apontar para a sua instância local do SQL Server.
    * Abra um terminal na pasta do backend e execute as migrations para criar o banco de dados e as tabelas:
        ```sh
        dotnet ef database update
        ```

2.  **Executar a API:**
    * Abra o arquivo de solução (`.sln`) no Visual Studio 2022.
    * Pressione **F5** ou clique no botão de "Iniciar" (com o perfil HTTP) para executar a aplicação.
    * Uma janela do navegador abrirá com a interface do Swagger, geralmente em uma URL que será necessário usar, pois você precisará dela para o frontend.

### 2. Frontend (Angular)

Com a API rodando, agora podemos iniciar a interface do usuário.

1.  **Configurar a URL da API:**
    * Navegue até a pasta do frontend.
    * Abra os arquivos de serviço: `src/app/services/auth.service.ts` e `src/app/services/book.service.ts`.
    * Verifique se a variável `private apiUrl` em ambos os arquivos corresponde **exatamente** à URL base em que sua API está rodando.

2.  **Instalar Dependências:**
    * Abra um **novo terminal** na pasta do frontend.
    * Execute o comando abaixo para instalar todos os pacotes necessários:
        ```sh
        npm install
        ```

3.  **Executar a Aplicação:**
    * Após a instalação, execute o comando para iniciar o servidor de desenvolvimento:
        ```sh
        ng serve -o
        ```
    * A aplicação será compilada e aberta automaticamente no seu navegador, geralmente em `http://localhost:4200`.

## Credenciais de Teste

A aplicação possui dois níveis de acesso. Utilize a tela de registro para registrar um usuário administrador e outro público.

## Funcionalidades Implementadas

* **Autenticação de Usuários:** Sistema completo de Login e Registro.
* **Controle de Acesso por Função:** A interface se adapta dinamicamente, mostrando/escondendo funcionalidades com base no cargo do usuário (Admin vs. Público).
* **Proteção de Rotas:** Páginas internas são protegidas com `AuthGuard`, redirecionando usuários não autorizados para a tela de login.
* **Gerenciamento de Token:** O token JWT é enviado automaticamente em todas as requisições para endpoints protegidos via `HTTP Interceptor`.
* **CRUD de Livros:** Administradores podem realizar todas as operações de Criar, Ler, Atualizar e Excluir livros.
* **Interface Responsiva:** Utilização do Angular Material para criar uma UI/UX moderna, consistente e adaptável a diferentes tamanhos de tela.
