
-- Apagar registros das tabelas do Asp.Net Identity

USE [TheosLibrary]
GO

DELETE FROM [dbo].[AspNetUserClaims]
DELETE FROM [dbo].[AspNetUserLogins]
DELETE FROM [dbo].[AspNetRoles]
DELETE FROM [dbo].[AspNetRoleClaims]
DELETE FROM [dbo].[AspNetUserRoles]
DELETE FROM [dbo].[AspNetUsers]

GO