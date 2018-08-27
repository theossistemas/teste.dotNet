USE [master]
GO
/****** Object:  Database [Livraria]    Script Date: 26/08/2018 21:45:21 ******/
CREATE DATABASE [Livraria]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Livraria', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Livraria.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Livraria_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Livraria_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Livraria] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Livraria].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Livraria] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Livraria] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Livraria] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Livraria] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Livraria] SET ARITHABORT OFF 
GO
ALTER DATABASE [Livraria] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Livraria] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Livraria] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Livraria] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Livraria] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Livraria] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Livraria] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Livraria] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Livraria] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Livraria] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Livraria] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Livraria] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Livraria] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Livraria] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Livraria] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Livraria] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Livraria] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Livraria] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Livraria] SET  MULTI_USER 
GO
ALTER DATABASE [Livraria] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Livraria] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Livraria] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Livraria] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Livraria] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Livraria] SET QUERY_STORE = OFF
GO
USE [Livraria]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Livraria]
GO
/****** Object:  Table [dbo].[Autor]    Script Date: 26/08/2018 21:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autor](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[Nome] [nvarchar](max) NULL,
 CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Editora]    Script Date: 26/08/2018 21:45:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editora](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[Nome] [nvarchar](max) NULL,
 CONSTRAINT [PK_Editora] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro]    Script Date: 26/08/2018 21:45:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[Descricao] [nvarchar](max) NULL,
	[AutorId] [uniqueidentifier] NULL,
	[EditoraId] [uniqueidentifier] NULL,
	[Edicao] [int] NULL,
 CONSTRAINT [PK_Livro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 26/08/2018 21:45:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[Login] [nvarchar](max) NULL,
	[Senha] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Livro_AutorId]    Script Date: 26/08/2018 21:45:22 ******/
CREATE NONCLUSTERED INDEX [IX_Livro_AutorId] ON [dbo].[Livro]
(
	[AutorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Livro_EditoraId]    Script Date: 26/08/2018 21:45:22 ******/
CREATE NONCLUSTERED INDEX [IX_Livro_EditoraId] ON [dbo].[Livro]
(
	[EditoraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Livro]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Autor_AutorId] FOREIGN KEY([AutorId])
REFERENCES [dbo].[Autor] ([Id])
GO
ALTER TABLE [dbo].[Livro] CHECK CONSTRAINT [FK_Livro_Autor_AutorId]
GO
ALTER TABLE [dbo].[Livro]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Editora_EditoraId] FOREIGN KEY([EditoraId])
REFERENCES [dbo].[Editora] ([Id])
GO
ALTER TABLE [dbo].[Livro] CHECK CONSTRAINT [FK_Livro_Editora_EditoraId]
GO
USE [master]
GO
ALTER DATABASE [Livraria] SET  READ_WRITE 
GO
