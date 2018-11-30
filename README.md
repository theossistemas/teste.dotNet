

## Programador Backend (C#)

# Instruções para executar o projeto

### Backend
    1. Abra o projeto LibraryControl no Visual Studio 2017
    2. Crie o banco de dados
    3. Alterar o endereco do banco de dados dentro do projeto LC.API/Properties no arquivo launchSettings.json variavel DB_CONNECTION
    4. Execute a aplicacao


### FrontEnd
    1. Abra o projeto angular-boostrap no Visual Studio Code
    2. Execute no terminal npm install 
    2. Alterar da api dentro do environment.ts (endpoint e images_url)
    3. Execute no terminal ng s



# Instruções

    Esse teste é público. Todos os interessados que fizerem pull request receberão um feedback da equipe
    Theòs Sistemas
    
    1. Faça um fork deste repositório;
    2. Crie uma branch com o seu nome.
    2. Adicione seu currículo na raiz do repositório.
    3. Envie-nos o PULL-REQUEST para que seja avaliado.
    
### O Teste

Uma livraria da cidade teve um aumento no número de seus exemplares e está com um problema para identificar todos os livros que possui em estoque. Para ajudar a livraria foi solicitado a você desenvolver uma aplicação web para gerenciar estes exemplares.Requisitos


    * O sistema deverá mostrar todos os livros cadastrados ordenados de forma ascendente pelo nome;
    * O sistema deverá permitir criar, editar e excluir um livro;
    * Os livros devem ser persistidos em um banco de dados.

### Outros Requisitos:
	* Para a persistência dos dados deve ser utilizado o Dapper ou Entity Core.
	* Configurar o Swagger na aplicação
	* Usar Microsfot SqlServer 2014 ou superior.
	* Gerar Scripts e disponibilizá-los um uma pasta.

### Observações:
	* O sistema deverá ser desenvolvido na plataforma .NET com C#, usando o framework ASP.NET CORE 
	(preferêncialmente 2.1, caso for usado outra versão, informar no pull-request)
	* Deve conter autenticação com dois níveis de acesso, um administrador e um público, o usuário de nível 
	público não terá autenticação, ou seja, terá acesso livre;


### Diferencial:
	* Implementar front-end para consumir a API em  Angular como framework Javascript.

    obs: Teste terá como avaliação principal os requisitos solicitados para o backend,  porém o frontend 
    poderá ser critério de desempate.


