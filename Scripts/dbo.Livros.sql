USE [dbLivraria]
GO

/****** Objeto: Table [dbo].[Livros] Data do Script: 17/06/2018 19:25:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Livros] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [Descricao]   NVARCHAR (50)   NOT NULL,
    [valor]       DECIMAL (18, 2) NOT NULL,
    [CategoriaID] INT             NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Livros_CategoriaID]
    ON [dbo].[Livros]([CategoriaID] ASC);


GO
ALTER TABLE [dbo].[Livros]
    ADD CONSTRAINT [PK_Livros] PRIMARY KEY CLUSTERED ([id] ASC);


GO
ALTER TABLE [dbo].[Livros]
    ADD CONSTRAINT [FK_Livros_Categoria_CategoriaID] FOREIGN KEY ([CategoriaID]) REFERENCES [dbo].[Categoria] ([CategoriaID]);


