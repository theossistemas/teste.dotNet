CREATE TABLE dbo.Livros(
	id varchar(36) NOT NULL,
	nome varchar(50) NOT NULL,
	descricao varchar(50),
	CONSTRAINT PK_Livros PRIMARY KEY (id)
)

CREATE TABLE dbo.SistemUser(
	id varchar(36) NOT NULL,
	userName varchar(50) NOT NULL,
	userPassword varchar(50) NOT NULL,
	userRole varchar(50),

	CONSTRAINT PK_SistemUser PRIMARY KEY (id)
)

INSERT INTO dbo.SistemUser(id, userName, userPassword, userRole)
VALUES (1, 'dono','senhateste', 'admin')

INSERT INTO dbo.Livros(id, nome, descricao)
VALUES ('3cd3cf4a-ff5e-4b4c-94df-e6a8f2778f2c', 'senhor dos aneis', 'primeiro livre do senhor dos aneis')

INSERT INTO dbo.Livros(id, nome, descricao)
VALUES ('3cd3cf4a-ff5e-4b4c-94df-e6a8f2778f2a', 'senhor dos aneis 2', 'segundo livro do senhor dos aneis')

INSERT INTO dbo.Livros(id, nome, descricao)
VALUES ('3cd3cf4a-ff5e-4b4c-94df-e6a8f2778f2y', 'senhor dos aneis 3', 'terceiro livro do senhor dos aneis')

