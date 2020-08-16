/*
	Autor : Paulo Zier

	*Nota: Todos os comandos são executados automáticamente através dos xmls de atualização no projeto Services, 
     e gerenciados pelo número e Guid do mesmo.
*/

-- =============================================

CREATE DATABASE Theos_teste;

-- =============================================

CREATE TABLE Versao (Id BIGINT IDENTITY PRIMARY KEY, Guid VARCHAR(100) NOT NULL UNIQUE, Numero BIGINT NOT NULL);

-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
        
    CREATE TABLE Usuario (Id BIGINT NOT NULL IDENTITY PRIMARY KEY, Login VARCHAR(100) NOT NULL UNIQUE, Senha VARCHAR(100) NOT NULL, Permissao TINYINT NOT NULL)
        
    INSERT INTO Usuario (Login, Senha, Permissao) VALUES ('admin', 'MTIzNA==', 0)
        
END
    
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Livro')
BEGIN
        
    CREATE TABLE Livro (Id BIGINT NOT NULL IDENTITY PRIMARY KEY, Titulo VARCHAR(100) NOT NULL UNIQUE, Descricao VARCHAR(MAX))
        
END

-- =============================================

CREATE PROC PROC_SalvarAtualizarLivro 
(
    @id BIGINT,
    @titulo VARCHAR(MAX),
    @descricao VARCHAR(MAX)
)
AS
BEGIN
        
    IF (@id IS NULL)
    BEGIN
        
        INSERT INTO Livro 
        (
            Titulo,
            Descricao
        ) 
        VALUES 
        (
            @titulo,
            @descricao
        )
          
        SET @id = (SELECT SCOPE_IDENTITY())
        
    END
    ELSE
    BEGIN
        
        UPDATE Livro SET Titulo = @titulo, Descricao = @descricao WHERE Id = @id
        
    END
        
    SELECT @id
        
END

-- =============================================

CREATE PROC PROC_SalvarAtualizarUsuario
(
    @id BIGINT,
    @login VARCHAR(MAX),
    @senha VARCHAR(MAX),
    @permissao SMALLINT
)
AS
BEGIN
            
    IF (@id IS NULL)
    BEGIN
        
    INSERT INTO Usuario 
    (
        Login,
        Senha,
        Permissao
    ) 
    VALUES 
    (
        @login,
        @senha,
        @permissao
    )
          
    SET @id = (SELECT SCOPE_IDENTITY())
        
    END
    ELSE
    BEGIN
        
    UPDATE Usuario SET Login = @login, Senha = @senha, Permissao = @permissao WHERE Id = @id
        
    END
        
    SELECT @id
           
END

-- =============================================
      