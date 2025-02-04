USE [master]
GO
/****** Object:  Database [SchoolProject]    Script Date: 2020-11-17 20:47:43 ******/
CREATE DATABASE [SchoolProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2019\MSSQL\DATA\SchoolProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2019\MSSQL\DATA\SchoolProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SchoolProject] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolProject] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolProject] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolProject] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolProject', N'ON'
GO
ALTER DATABASE [SchoolProject] SET QUERY_STORE = OFF
GO
USE [SchoolProject]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 2020-11-17 20:47:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Ingredients] [nvarchar](max) NOT NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeCategory]    Script Date: 2020-11-17 20:47:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RecipeCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Recipe] ON 

INSERT [dbo].[Recipe] ([RecipeId], [Title], [Description], [Ingredients], [CategoryId]) VALUES (2, N'Kyckling wok', N'Wok', N'kyckling, kokosmjölk', 4)
INSERT [dbo].[Recipe] ([RecipeId], [Title], [Description], [Ingredients], [CategoryId]) VALUES (4, N'Kött grytan', N'Gryta', N'kött, grädde, grönsaker, vatten', 2)
INSERT [dbo].[Recipe] ([RecipeId], [Title], [Description], [Ingredients], [CategoryId]) VALUES (5, N'Köttbullars', N'Köttbullars', N'Kött, lök, salt, peppar, vatten', 1)
INSERT [dbo].[Recipe] ([RecipeId], [Title], [Description], [Ingredients], [CategoryId]) VALUES (6, N'Boeuf bourguignon', N'Gryta', N'kött, lök, vatten, krydda', 1)
INSERT [dbo].[Recipe] ([RecipeId], [Title], [Description], [Ingredients], [CategoryId]) VALUES (11, N'This is new recipe', N'new recipe', N'Grönsaker, ica', 3)
SET IDENTITY_INSERT [dbo].[Recipe] OFF
SET IDENTITY_INSERT [dbo].[RecipeCategory] ON 

INSERT [dbo].[RecipeCategory] ([CategoryId], [Name]) VALUES (1, N'Franskt')
INSERT [dbo].[RecipeCategory] ([CategoryId], [Name]) VALUES (2, N'Husmanskost')
INSERT [dbo].[RecipeCategory] ([CategoryId], [Name]) VALUES (3, N'Italienskt')
INSERT [dbo].[RecipeCategory] ([CategoryId], [Name]) VALUES (4, N'Kinesiskt')
SET IDENTITY_INSERT [dbo].[RecipeCategory] OFF
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [FK_Recipe_RecipeCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[RecipeCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [FK_Recipe_RecipeCategory]
GO
USE [master]
GO
ALTER DATABASE [SchoolProject] SET  READ_WRITE 
GO
