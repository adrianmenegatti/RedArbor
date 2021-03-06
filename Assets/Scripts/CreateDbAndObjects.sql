USE [master]
GO
/****** Object:  Database [RedArbor]    Script Date: 1/21/2019 4:58:12 PM ******/
CREATE DATABASE [RedArbor]
GO
ALTER DATABASE [RedArbor] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RedArbor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RedArbor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RedArbor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RedArbor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RedArbor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RedArbor] SET ARITHABORT OFF 
GO
ALTER DATABASE [RedArbor] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RedArbor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RedArbor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RedArbor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RedArbor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RedArbor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RedArbor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RedArbor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RedArbor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RedArbor] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RedArbor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RedArbor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RedArbor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RedArbor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RedArbor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RedArbor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RedArbor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RedArbor] SET RECOVERY FULL 
GO
ALTER DATABASE [RedArbor] SET  MULTI_USER 
GO
ALTER DATABASE [RedArbor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RedArbor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RedArbor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RedArbor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RedArbor] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RedArbor', N'ON'
GO
ALTER DATABASE [RedArbor] SET QUERY_STORE = OFF
GO
USE [RedArbor]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 1/21/2019 4:58:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[Email] [nvarchar](200) NULL,
	[Fax] [nvarchar](20) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastLogin] [datetime] NULL,
	[Password] [nvarchar](50) NOT NULL,
	[PortalId] [int] NULL,
	[RoleId] [int] NULL,
	[StatusId] [int] NULL,
	[Telephone] [nvarchar](20) NULL,
	[UpdatedOn] [datetime] NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [RedArbor] SET  READ_WRITE 
GO
