/*
Esse script cria um banco de dados de testes do see you runner
Para um de produção modificar os nomes e caminho de instalação
*/

USE [master]
GO

/****** Object:  Database [tst_seeyourunner]    Script Date: 07/16/2012 01:24:24 ******/
CREATE DATABASE [tst_seeyourunner] ON  PRIMARY 
( NAME = N'tst_seeyourunner', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\tst_seeyourunner.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tst_seeyourunner_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\tst_seeyourunner_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [tst_seeyourunner] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tst_seeyourunner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [tst_seeyourunner] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET ARITHABORT OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [tst_seeyourunner] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [tst_seeyourunner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [tst_seeyourunner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET  DISABLE_BROKER 
GO

ALTER DATABASE [tst_seeyourunner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [tst_seeyourunner] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [tst_seeyourunner] SET  READ_WRITE 
GO

ALTER DATABASE [tst_seeyourunner] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [tst_seeyourunner] SET  MULTI_USER 
GO

ALTER DATABASE [tst_seeyourunner] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [tst_seeyourunner] SET DB_CHAINING OFF 
GO


