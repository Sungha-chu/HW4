USE [master]
GO
/****** Object:  Database [TestLog]    Script Date: 2022/10/7 上午 01:02:36 ******/
CREATE DATABASE [TestLog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestLog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestLog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestLog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestLog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TestLog] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestLog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestLog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestLog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestLog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestLog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestLog] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestLog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestLog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestLog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestLog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestLog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestLog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestLog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestLog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestLog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestLog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestLog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestLog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestLog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestLog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestLog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestLog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestLog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestLog] SET RECOVERY FULL 
GO
ALTER DATABASE [TestLog] SET  MULTI_USER 
GO
ALTER DATABASE [TestLog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestLog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestLog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestLog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestLog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestLog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestLog', N'ON'
GO
ALTER DATABASE [TestLog] SET QUERY_STORE = OFF
GO
USE [TestLog]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 2022/10/7 上午 01:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[path] [varchar](50) NOT NULL,
	[time] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogItem]    Script Date: 2022/10/7 上午 01:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LogMsg] [nchar](50) NULL,
 CONSTRAINT [PK_LogItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2022/10/7 上午 01:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Account] [nchar](50) NOT NULL,
	[Password] [nchar](50) NOT NULL,
	[Permission] [nchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [TestLog] SET  READ_WRITE 
GO
