/*
Script para criar os logins de acesso ao banco de dados.
*/
/****** Object:  Login [syr_owner]    Script Date: 07/16/2012 01:26:02 ******/
CREATE LOGIN [syr_owner] WITH PASSWORD=N'syr_owner', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [syr_owner] ENABLE
GO

/****** Object:  Login [syr_run]    Script Date: 07/16/2012 01:26:10 ******/
CREATE LOGIN [syr_run] WITH PASSWORD=N'syr_run', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [syr_run] ENABLE
GO

/*
Criando o schema e os usuarios
*/
/****** Object:  Schema [syr_owner]    Script Date: 07/16/2012 01:35:55 ******/
CREATE SCHEMA [syr_owner] AUTHORIZATION [dbo]
GO
/****** Object:  User [syr_run]    Script Date: 07/16/2012 01:35:54 ******/
CREATE USER [syr_run] FOR LOGIN [syr_run] WITH DEFAULT_SCHEMA=[syr_owner]
GO
/****** Object:  User [syr_owner]    Script Date: 07/16/2012 01:35:54 ******/
CREATE USER [syr_owner] FOR LOGIN [syr_owner] WITH DEFAULT_SCHEMA=[syr_owner]
GO
/****** Object:  Role [syr_owners]    Script Date: 07/16/2012 01:35:54 ******/
CREATE ROLE [syr_owners] AUTHORIZATION [dbo]
GO
/****** Object:  Role [syr_users]    Script Date: 07/16/2012 01:35:54 ******/
CREATE ROLE [syr_users] AUTHORIZATION [dbo]
GO
