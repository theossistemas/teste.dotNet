IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Livros] (
    [Id] int NOT NULL IDENTITY,
    [ISBN] int NOT NULL,
    [NomeLivro] nvarchar(max) NULL,
    [NomeAutor] nvarchar(max) NULL,
    [Editora] nvarchar(max) NULL,
    [AnoLancamento] int NOT NULL,
    [Edicao] tinyint NOT NULL,
    CONSTRAINT [PK_Livros] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190406175054_CriacaoDaBaseTheos', N'2.1.4-rtm-31024');

GO

