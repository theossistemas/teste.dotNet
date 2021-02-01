USE [master]
GO

/****** Object:  Table [dbo].[Livro]    Script Date: 01/02/2021 11:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Livro](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](300) NULL,
	[Descricao] [text] NULL,
	[Autor] [nvarchar](300) NULL,
	[Imagem] [nvarchar](500) NULL,
	[Paginas] [int] NULL,
	[Edicao] [nvarchar](300) NULL,
	[Idioma] [nvarchar](100) NULL,
	[Editora] [nvarchar](300) NULL,
	[DataPublicacao] [datetime] NULL,
	[Estoque] [int] NULL,
	[Ativo] [bit] NULL,
	[IdUsuario] [bigint] NULL,
	[DataCadastro] [datetime] NULL,
	[DataAlteracao] [datetime] NULL,
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


