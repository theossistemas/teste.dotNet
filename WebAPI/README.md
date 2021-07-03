## Script Das tabelas para o banco [BancoTheoLib]

* ScriptTabela_BancoTheoLib.sql

## Antes de executar (Inserir o usuário amd comsenha 1234)
* descomentar a linha 86 do contreller UsuarioController 
** //var resultado = await _usuarioServico.Inserir( new RequisicaoUsuario{ Nome = "adm",Email="adm@adm.com.br", Senha="123456" }); **
* Fazer uma chamada ao método da API  [POST] url/Usuario/Inserir

## A soluction (API) está em Onion Architecture.
* Evitando assim o acoplamento.
* separação de responsabilidades
* Aplicado os conceitos iniciais do SOLID e padrão DDD

#### Para consumiros métodos de criação e alteração deve primeiro gerar o token:



**Token** 
`/Usuario/login`
    
 * Email: adm@amd.com.br
 * Senha: 123456
 
 