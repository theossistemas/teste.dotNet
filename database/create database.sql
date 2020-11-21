--Use this script to create and reset the database
--Execute as an admin
--OBS.: Dropping the database wont work if the application is running
USE master
GO

IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'TheosTest')
BEGIN
	DROP DATABASE TheosTest
END
GO

create DATABASE TheosTest
GO

USE TheosTest
GO

IF EXISTS (SELECT name FROM master.sys.server_principals WHERE name = N'theos_application_access')
BEGIN
	DROP LOGIN theos_application_access
	--DROP USER theos_application_access
END
GO

CREATE LOGIN theos_application_access WITH PASSWORD = 'v3ry_str0ng_p4ssw0rd'
CREATE USER theos_application_access FOR LOGIN theos_application_access
GO

GRANT INSERT TO theos_application_access 
GRANT UPDATE TO theos_application_access
GRANT DELETE TO theos_application_access
GRANT SELECT TO theos_application_access
GO

CREATE TABLE books (
	id				INT PRIMARY KEY IDENTITY,
	name			VARCHAR(100) NOT NULL,
	author			VARCHAR(50) NOT NULL,
	launch_year		SMALLINT NOT NULL,
	price			DECIMAL(6,2) NOT NULL,
	
	CHECK (launch_year >= 0),
	CHECK (price >= 0),
	UNIQUE(name)
)

INSERT books (name, author, launch_year, price) VALUES
('O dilema do porco-espinho', 'Leandro Karnal', 2018, 25.89),
('Dom Quixote', 'Miguel de Cervantes', 1605, 56.79),
('Guerra e Paz', 'Liev Tolstói', 1869, 32.98)

select * from books


CREATE TABLE admins (
	id				INT PRIMARY KEY IDENTITY,
	name			VARCHAR(100) NOT NULL,
	login			VARCHAR(50) NOT NULL,
	password		CHAR(64) NOT NULL,
	
	UNIQUE(login)
)

INSERT admins (name, login, password) VALUES 
('Marcelo Guimarães da Costa', 'marcelo', 'A075D17F3D453073853F813838C15B8023B8C487038436354FE599C3942E1F95') --p4ssw0rd

select * from admins


--PM> Scaffold-DbContext "Data Source=localhost\WIN-R30N1VRLA7T\MSSQLSERVER,11433;Initial Catalog=TheosTest;User ID=theos_application_access;Password=v3ry_str0ng_p4ssw0rd" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entity -Force

