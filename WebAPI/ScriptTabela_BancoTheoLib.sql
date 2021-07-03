USE master ;  
GO  
CREATE DATABASE BancoTheoLib  
ON   
( NAME = BancoTheoLib_dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BancoTheoLibdat.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = BancoTheoLib_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BancoTheoLiblog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
GO  

USE [BancoTheoLib]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](300) NULL,
	[Email] [nvarchar](300) NULL,
	[Senha] [nvarchar](300) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Livro](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](300) NULL,
	[Descricao] [text] NULL,
	[Autor] [nvarchar](300) NULL,
	[DataPublicacao] [datetime] NULL,
	[Estoque] [int] NULL,
	[Ativo] [bit] NULL,
	[IdUsuario] [bigint] NULL,
 CONSTRAINT [PK_Livro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Livro]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO

ALTER TABLE [dbo].[Livro] CHECK CONSTRAINT [FK_Livro_Usuario]
GO
