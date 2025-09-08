# Tutorial: Configurando o Banco de Dados SQL Server (sem Docker)
1. Pré-requisitos
•	SQL Server instalado (pode ser SQL Server Express ou versão completa)
•	SQL Server Management Studio (SSMS) para gerenciar o banco (opcional, mas recomendado)
•	.NET 8 SDK instalado
---
2. Crie o banco de dados
1.	Abra o SQL Server Management Studio (SSMS).
2.	Conecte-se ao seu servidor local.
3.	Clique com o botão direito em "Databases" e escolha "New Database".
4.	Dê o nome LivrariaDb e clique em "OK".
---
3. Configure a string de conexão
No arquivo appsettings.json do seu projeto, ajuste a string de conexão conforme seu ambiente.
Exemplo para Windows com autenticação integrada:

---
4. Execute as migrations
No terminal, navegue até a pasta do projeto principal (onde está o .csproj) e rode:

dotnet ef database update

5. Seed automático dos dados
O projeto já possui uma classe SeedData que insere 25 livros automaticamente na primeira execução.
Não é necessário rodar scripts SQL manualmente.

6. Rode o projeto
No terminal, execute:
dotnet run

7. Pronto!
•	O banco estará criado e populado com os livros do seed.
•	A API estará disponível na porta configurada (ex: https://localhost:5001).

# Informações Importantes: 
Bibliotecas utilizadas: 
• Entity Framework ORM para persistência dos dados.
• Xunit para testes unitários (aplicado o TDD).
• Serilog para facilitar a criação de logs.
• Moq para criação de Mocks.
• Microsoft Authentication Jwt Bearer para autenticação.

# Resumo do Projeto: 
• Este projeto é uma aplicação completa para gerenciamento de uma livraria, desenvolvida com Angular no frontend e .NET no backend, utilizando Entity Framework para persistência de dados. Projeto respeitando o SOLID. 

# Funcionalidades: 
• Autenticação de usuários > Sistema de login e registro, garantindo acesso seguro às funcionalidades.
• CRUD de Livros > Cadastro, edição, exclusão e listagem de livros.
• Paginação (back e front) > Listagem de livros com paginação para melhor desempenho e usabilidade.
• Validação de formulários > Validações no frontend para garantir integridade dos dados.
• Tela de login no Front-end > Tela de login com autenticação (usuário pré-criado no código)
• Tratamento de exceções no back-end > Integrado com Serilog para gravar logs no banco (tabela criada automaticamente ao executar código)

