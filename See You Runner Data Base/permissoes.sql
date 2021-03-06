/*
Criacao das permissoes (grants) para as roles e usuarios
*/
USE [master]
GO
--permitindo aos usuarios que se conectem ao banco
GRANT CONNECT TO [syr_owner] AS [dbo]
GO
GRANT CONNECT TO [syr_run] AS [dbo]
GO

--dando permissoes de nivel de banco de dados as roles
GRANT ALTER TO [syr_owners] AS [dbo]
GO
GRANT BACKUP DATABASE TO [syr_owners] AS [dbo]
GO
GRANT CREATE FUNCTION TO [syr_owners] AS [dbo]
GO
GRANT CREATE PROCEDURE TO [syr_owners] AS [dbo]
GO
GRANT CREATE QUEUE TO [syr_owners] AS [dbo]
GO
GRANT CREATE SYNONYM TO [syr_owners] AS [dbo]
GO
GRANT CREATE TABLE TO [syr_owners] AS [dbo]
GO
GRANT CREATE VIEW TO [syr_owners] AS [dbo]
GO
GRANT DELETE TO [syr_owners] AS [dbo]
GO
GRANT EXECUTE TO [syr_owners] AS [dbo]
GO
GRANT INSERT TO [syr_owners] AS [dbo]
GO
GRANT SELECT TO [syr_owners] AS [dbo]
GO
GRANT UPDATE TO [syr_owners] AS [dbo]
GO
--permissoes o banco especifico
USE [tst_seeyourunner]
GO

--adicionando os usuarios nas roles
EXEC dbo.sp_addrolemember @rolename=N'syr_owners', @membername=N'syr_owner'
GO
EXEC dbo.sp_addrolemember @rolename=N'syr_users', @membername=N'syr_run'
GO

--permissoes da role syr_owner dentro do schema padrao
GRANT ALTER ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT DELETE ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT EXECUTE ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT INSERT ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT REFERENCES ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT SELECT ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT UPDATE ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT CONTROL ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT VIEW DEFINITION ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
GRANT VIEW CHANGE TRACKING ON SCHEMA::[syr_owner] TO [syr_owners] AS [dbo]
GO
--permissoes da role syr_users no schema
GRANT EXECUTE ON SCHEMA::[syr_owner] TO [syr_users] AS [dbo]
GO
GRANT SELECT ON SCHEMA::[syr_owner] TO [syr_users] AS [dbo]
GO
