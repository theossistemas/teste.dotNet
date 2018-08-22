CREATE TABLE [dbo].[Users] (
    [Id]           INT IDENTITY(1,1) PRIMARY KEY,
    [UserName]     VARCHAR(50) NULL,
    [FirstName]    VARCHAR(50)     NULL,
    [LastName]     VARCHAR(50)     NULL,
    [Permissao]    VARCHAR(50)      NULL,
    [PasswordHash] VARCHAR(MAX)           NULL,
    [PasswordSalt] VARCHAR(MAX)           NULL
);