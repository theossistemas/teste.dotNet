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
