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
    [Id] uniqueidentifier NOT NULL,
    [CriadoEm] datetime2 NULL,
    [ModificadoEm] datetime2 NULL,
    [Isbn] varchar(30) NOT NULL,
    [Autor] varchar(100) NOT NULL,
    [Titulo] varchar(100) NOT NULL,
    [Preco] float NOT NULL,
    [Publicacao] datetime2 NOT NULL,
    [ImagemCapa] nvarchar(max) NULL,
    CONSTRAINT [PK_Livros] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190226022539_Initial', N'2.2.3-servicing-35854');

GO