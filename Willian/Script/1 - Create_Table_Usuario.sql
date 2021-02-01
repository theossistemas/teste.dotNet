USE [master]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 01/02/2021 11:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](300) NULL,
	[Email] [nvarchar](300) NULL,
	[Senha] [nvarchar](300) NULL,
	[PrimeiroAcesso] [bit] NULL,
	[Ativo] [bit] NULL,
	[DataCadastro] [datetime] NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


Insert Into Usuario values ('admin', 'admin@livraria.com.br', 'feNn/bOdvmPCgOwfu/m4ug==', 0, 1, GETDATE(), null)