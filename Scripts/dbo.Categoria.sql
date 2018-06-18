USE [dbLivraria]
GO

/****** Objeto: Table [dbo].[Categoria] Data do Script: 17/06/2018 19:24:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categoria] (
    [CategoriaID] INT           IDENTITY (1, 1) NOT NULL,
    [Descricao]   NVARCHAR (50) NOT NULL
);


