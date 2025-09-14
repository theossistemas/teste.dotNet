IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Livros] (
    [Id] uniqueidentifier NOT NULL,
    [Titulo] nvarchar(200) NOT NULL,
    [Autor] nvarchar(150) NOT NULL,
    [AnoPublicacao] int NULL,
    [ISBN] nvarchar(20) NULL,
    [Editora] nvarchar(100) NULL,
    [DataPublicacao] date NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [Estoque] int NOT NULL,
    CONSTRAINT [PK_Livros] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Livro_Estoque_NaoNegativo] CHECK ([Estoque] >= 0),
    CONSTRAINT [CK_Livro_Preco_NaoNegativo] CHECK ([Preco] >= 0)
);
GO

CREATE INDEX [IX_Livro_Titulo] ON [Livros] ([Titulo]);
GO

CREATE UNIQUE INDEX [IX_Livros_ISBN] ON [Livros] ([ISBN]) WHERE [ISBN] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Livros_Titulo_Autor_AnoPublicacao] ON [Livros] ([Titulo], [Autor], [AnoPublicacao]) WHERE [AnoPublicacao] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250913112934_Initial', N'8.0.20');
GO

COMMIT;
GO

