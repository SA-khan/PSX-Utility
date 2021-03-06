USE [master]
GO
/****** Object:  Database [PSXDataWarehouse]    Script Date: 9/11/2020 5:50:25 AM ******/
CREATE DATABASE [PSXDataWarehouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PSXDataWarehouse', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PSXDataWarehouse.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PSXDataWarehouse_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PSXDataWarehouse_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PSXDataWarehouse] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PSXDataWarehouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PSXDataWarehouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PSXDataWarehouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PSXDataWarehouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PSXDataWarehouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PSXDataWarehouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET RECOVERY FULL 
GO
ALTER DATABASE [PSXDataWarehouse] SET  MULTI_USER 
GO
ALTER DATABASE [PSXDataWarehouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PSXDataWarehouse] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PSXDataWarehouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PSXDataWarehouse] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PSXDataWarehouse] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PSXDataWarehouse', N'ON'
GO
ALTER DATABASE [PSXDataWarehouse] SET QUERY_STORE = OFF
GO
USE [PSXDataWarehouse]
GO
/****** Object:  Table [dbo].[APP_STATUS]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_STATUS](
	[APP_ST_CLEARING_TABLE] [bit] NULL,
	[APP_ST_FETCHING_DATA] [bit] NULL,
	[APP_ST_DUMPING_DATA] [bit] NULL,
	[APP_ST_FETCH_DATA_TIME] [bigint] NULL,
	[APP_ST_DUMP_DATA_TIME] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[C_ID] [int] IDENTITY(1,1) NOT NULL,
	[C_SYMBOL] [varchar](200) NULL,
	[C_NAME] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FUND]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FUND](
	[FUND_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FUND_CODE] [int] NULL,
	[FUND_NAME] [varchar](500) NULL,
	[FUND_SYMBOL] [varchar](500) NULL,
	[FUND_DESC] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[FUND_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketSummary]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketSummary](
	[MS_ID] [int] IDENTITY(1,1) NOT NULL,
	[MS_COMPANY_NAME] [varchar](500) NULL,
	[MS_COMPANY_SYMBOL] [varchar](500) NULL,
	[MS_COMPANY_LDCP] [float] NULL,
	[MS_COMPANY_OPEN] [float] NULL,
	[MS_COMPANY_HIGH] [float] NULL,
	[MS_COMPANY_LOW] [float] NULL,
	[MS_COMPANY_CURRENT] [float] NULL,
	[MS_COMPANY_CHANGE] [float] NULL,
	[MS_COMPANY_VOLUME] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketSummaryHistory]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketSummaryHistory](
	[MS_ID] [int] IDENTITY(1,1) NOT NULL,
	[MS_DATE] [datetime] NULL,
	[MS_STATUS] [varchar](500) NULL,
	[MS_SECTOR] [varchar](500) NULL,
	[MS_COMPANY_NAME] [varchar](500) NULL,
	[MS_COMPANY_SYMBOL] [varchar](500) NULL,
	[MS_COMPANY_LDCP] [float] NULL,
	[MS_COMPANY_OPEN] [float] NULL,
	[MS_COMPANY_HIGH] [float] NULL,
	[MS_COMPANY_LOW] [float] NULL,
	[MS_COMPANY_CURRENT] [float] NULL,
	[MS_COMPANY_CHANGE] [float] NULL,
	[MS_COMPANY_VOLUME] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketSummaryOverview]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketSummaryOverview](
	[MS_ID] [int] IDENTITY(1,1) NOT NULL,
	[MS_DATE] [datetime] NULL,
	[MS_STATUS] [varchar](300) NULL,
	[MS_VOLUME] [float] NULL,
	[MS_VALUE] [float] NULL,
	[MS_TRADES] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarketSummaryOverviewHistory]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarketSummaryOverviewHistory](
	[MS_ID] [int] IDENTITY(1,1) NOT NULL,
	[MS_DATE] [datetime] NULL,
	[MS_SYS_DATE] [datetime] NULL,
	[MS_STATUS] [varchar](300) NULL,
	[MS_VOLUME] [float] NULL,
	[MS_VALUE] [float] NULL,
	[MS_TRADES] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (201, N'IBLHL', N'IBL HealthCare Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (202, N'MACTER', N'Macter International Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (203, N'SEARL', N'The Searle Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (204, N'WYETH', N'Wyeth Pakistan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (205, N'ALTN', N'Altern Energy Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (206, N'EPQL', N'Engro Powergen Qadirpur Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (207, N'HUBC', N'Hub Power Company Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (208, N'KEL', N'K-Electric Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (209, N'KOHP', N'Kohinoor Power Co Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (210, N'KAPCO', N'Kot Addu Power Company.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (211, N'LPL', N'LALPIR Power Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (212, N'NCPL', N'Nishat Chunian Power Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (213, N'NPL', N'Nishat Power Limited.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (214, N'PKGP', N'Pakgen Power Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (215, N'SPWL', N'Saif Power Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (216, N'ATRL', N'Attock Refinery Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (217, N'BYCO', N'BYCO Petroleum Pak Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (218, N'NRL', N'National Refinary Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (219, N'PRL', N'Pakistan Refinery Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (220, N'AGSML', N'Abdullah Shaha Ghazi Suger Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (221, N'ADAMS', N'Adam Sugar Mills Limited. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (222, N'AABS', N'Al-Abbas Sugar Mills Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (223, N'CHAS', N'Chashma Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (224, N'DWSM', N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (225, N'FRSM', N'Faran Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (226, N'HABSM', N'Habib Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (227, N'HSM', N'Husein Sugar Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (228, N'IMSL', N'Imperial Sugar Limited. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (229, N'JSML', N'Jauharabad Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (230, N'KPUS', N'Khairpur Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (231, N'MRNS', N'Mehran Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (232, N'MIRKS', N'Mirpurkhas Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (233, N'NONS', N'Noon Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (234, N'SKRS', N'Sakrand Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (235, N'SHSML', N'Shahmurad Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (236, N'PSYL', N'Pakistan Synthentics Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (237, N'RUPL', N'Rupali Polyester Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (238, N'TRPOL', N'Tri-Star Polyester Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (239, N'AVN', N'Avanceon Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (240, N'HUMNL', N'Hum Network Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (241, N'MDTL', N'Media Times Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (242, N'NETSOL', N'NetSol Technologies Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (243, N'PTC', N'Pakistan Telecommunication Co.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (244, N'SYS', N'Systems Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (245, N'TELE', N'Telecard Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (246, N'TPL', N'TPL CORP Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (247, N'TPLT', N'TPL Trakker Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (248, N'TRG', N'TRG Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (249, N'WTL', N'Worldcall Telecom Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (250, N'ANL', N'Azgard Nine Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (251, N'BTL', N'Bleesed Textile Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (252, N'CRTM', N'Crescent Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (253, N'DLL', N'Dawood Lawrencepur Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (254, N'FASM', N'Faisal Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (255, N'FML', N'Feroze1888 Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (256, N'GATM', N'Gul Ahmed Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (257, N'ILP', N'Interloop Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (258, N'KOIL', N'Kohinoor Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (259, N'KML', N'Kohinoor Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (260, N'KTML', N'Kohinoor Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (261, N'MTIL', N'Mian Textile Industries Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (262, N'NCL', N'Nishat (Chunia) Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (263, N'NML', N'Nishat Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (264, N'REDCO', N'Redco Textile Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (265, N'REWM', N'Reliance Weaving Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (266, N'TOWL', N'Towellers Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (267, N'ASTM', N'Asim Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (268, N'BILF', N'Bilal Fibres Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (269, N'CWSM', N'Chakwal Spinning Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (270, N'CTM', N'Colony Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (271, N'CCM', N'Crescent Cotton Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (272, N'DFSM', N'Dewan Farooque Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (273, N'DINT', N'Din Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (274, N'GADT', N'Gadoon Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (275, N'JATM', N'J. A. Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (276, N'KOSM', N'Kohinoor Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (277, N'MQTM', N'Maqbool Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (278, N'RAVT', N'Ravi Textile Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (279, N'RUBY', N'Ruby Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (280, N'SAIF', N'Saif Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (281, N'SALT', N'Salfi Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (282, N'SHDT', N'Shadab Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (283, N'SMTM', N'Samin Textiles Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (284, N'SERF', N'Service Fabrics Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (285, N'YOUW', N'Yousuf Weaving Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (286, N'ZTL', N'Zephyr Textile Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (287, N'KHTC', N'Khyber Tobacco Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (288, N'PAKT', N'Pakistan Tobacco Co Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (289, N'PIAA', N'Pak International Airline Corp Ltd')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (290, N'PICT', N'Pakistan Int.Container Terminal.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (291, N'PIBTL', N'Pakistan Intl. Bulk Terminal Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (292, N'PNSC', N'Pakistan National Shipping Co.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (293, N'SSOM', N'S .S . Oil Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (294, N'UNITY', N'Unity Foods Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (295, N'UNITYR2', N'Unity Foods Limited.(R2)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (296, N'DCR', N'Dolmen City REIT.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (297, N'ASTL', N'ASTL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (298, N'ATRL', N'ATRL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (299, N'AVN', N'AVN-SEP')
GO
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (300, N'BAHL', N'BAHL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (301, N'BAFL', N'BAFL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (302, N'BOP', N'BOP-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (303, N'CHCC', N'CHCC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (304, N'DGKC', N'DGKC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (305, N'DOL', N'DOL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (306, N'ENGRO', N'ENGRO-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (307, N'EFERT', N'EFERT-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (308, N'EPCL', N'EPCL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (309, N'FCCL', N'FCCL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (310, N'FFBL', N'FFBL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (311, N'FFC', N'FFC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (312, N'FFL', N'FFL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (313, N'GTYR', N'GTYR-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (314, N'GHNI', N'GHNI-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (315, N'GHNL', N'GHNL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (316, N'GATM', N'GATM-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (317, N'HBL', N'HBL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (318, N'HASCOL', N'HASCOL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (319, N'HUBC', N'HUBC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (320, N'INIL', N'INIL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (321, N'ISL', N'ISL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (322, N'KEL', N'KEL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (323, N'KOHC', N'KOHC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (324, N'KAPCO', N'KAPCO-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (325, N'LOTCHEM', N'LOTCHEM-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (326, N'LUCK', N'LUCK-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (327, N'MLCF', N'MLCF-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (328, N'MCB', N'MCB-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (329, N'MEBL', N'MEBL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (330, N'MUGHAL', N'MUGHAL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (331, N'NBP', N'NBP-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (332, N'NRL', N'NRL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (333, N'NETSOL', N'NETSOL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (334, N'NCL', N'NCL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (335, N'NML', N'NML-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (336, N'OGDC', N'OGDC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (337, N'PAEL', N'PAEL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (338, N'PIBTL', N'PIBTL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (339, N'POL', N'POL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (340, N'PPL', N'PPL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (341, N'PSO', N'PSO-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (342, N'PIOC', N'PIOC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (343, N'SNGP', N'SNGP-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (344, N'SSGC', N'SSGC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (345, N'SEARL', N'SEARL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (346, N'TRG', N'TRG-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (347, N'UBL', N'UBL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (348, N'UNITY', N'UNITY-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (349, N'DFML', N'Dewan Farooque Motors Limited [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (350, N'GHNI', N'Ghandhara Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (351, N'GHNL', N'Ghandhara Nissan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (352, N'GAIL', N'Ghani Automobile Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (353, N'HINO', N'Hino Pak Motor Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (354, N'HCAR', N'Honda Atlas Cars (Pak) Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (355, N'INDU', N'Indus Motor Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (356, N'MTL', N'Millat Tractors Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (357, N'PSMC', N'Pak Suzuki Motors Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (358, N'SAZEW', N'Sazgar Engineering Works Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (359, N'BWHL', N'Baluchistan Wheels Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (360, N'EXID', N'Exide Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (361, N'GTYR', N'General Tyre & Rubber Co.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (362, N'LOADS', N'Loads Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (363, N'THALL', N'Thal Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (364, N'JOPP', N'Johnson & Philips (Pak) Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (365, N'PAEL', N'Pak Elektron Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (366, N'PCAL', N'Pakistan Cables Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (367, N'SIEG', N'Siemens (Pak) Eng. Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (368, N'WAVES', N'WAVES Singer Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (369, N'ACPL', N'Attock Cement Pak Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (370, N'BWCL', N'Bestway Cement Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (371, N'CHCC', N'Cherat Cement Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (372, N'DGKC', N'D. G. Khan Cement Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (373, N'DCL', N'Dewan Cement Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (374, N'FCCL', N'Fauji Cement Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (375, N'FECTC', N'Fecto Cement Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (376, N'FLYNG', N'Flying Cement Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (377, N'GWLC', N'Gharibwal Cement Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (378, N'JVDC', N'Javedan Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (379, N'KOHC', N'Kohat Cement Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (380, N'LCL', N'Lucky Cement Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (381, N'MLCF', N'Maple Leaf Cement Factory Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (382, N'PIOC', N'Pioneer Cement Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (383, N'POWE', N'Power cement Limited')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (384, N'POWE-R1', N'Power cement Limited(R1)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (385, N'SMCPL', N'Safe Mix Concrete Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (386, N'THCCL', N'Thatta Cement Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (387, N'AGL', N'Agritech Limited')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (388, N'ARPL', N'Archroma Pakistan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (389, N'BAPL', N'Bawany Air Products Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (390, N'BERG', N'Berger Paints Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (391, N'BIFO', N'Bifo Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (392, N'BUXL', N'Buxly Paints Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (393, N'COLG', N'Colgate Palmolive (Pak) Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (394, N'DOL', N'Descon Oxychem Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (395, N'DYNO', N'DYNEA Pak.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (396, N'EPCL', N'Engro Polymer & Chemicals Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (397, N'GGL', N'Ghani Global Holdings Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (398, N'ICI', N'ICI Pakistan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (399, N'ICL', N'Ittehad Chemical Ltd.')
GO
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (400, N'LOTCHEM', N'Lotte Chemical Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (401, N'NRSL', N'Nimir Resins Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (402, N'PAKOXY', N'Pakistan Oxygen Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (403, N'SIPE', N'Sitara Peroxide Limited')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (404, N'HGFA', N'HBL Growth Fund')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (405, N'HIFA', N'HBL Investment Fund')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (406, N'ABL', N'Allied Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (407, N'AKBL', N'Askari Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (408, N'BAHL', N'Bank Al-Habib Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (409, N'BAFL', N'Bank Alfalah Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (410, N'BOP', N'Bank Of Punjab.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (411, N'BIPL', N'Bankislami Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (412, N'FABL', N'Faysal Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (413, N'HBL', N'Habib Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (414, N'HMB', N'Habib Metropolitn Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (415, N'JSBL', N'JS Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (416, N'MCB', N'MCB Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (417, N'MEBL', N'Meezan Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (418, N'NBP', N'National Bank Of Pakistan.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (419, N'SILK', N'Silk Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (420, N'SNBL', N'Soneri Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (421, N'SCBPL', N'Standard Chartered Bank Pak Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (422, N'SMBL', N'Summit Bank Limited. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (423, N'UBL', N'United Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (424, N'ADOS', N'Ados Pakistan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (425, N'AISH', N'Aisha Steel Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (426, N'ASTL', N'Amreli Steels Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (427, N'BCL', N'Bolan Casting Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (428, N'CSAP', N'Crescent Steel & Allied Product.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (429, N'DSL', N'Dost Steels Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (430, N'DKL', N'Drekkar Kingsway Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (431, N'INIL', N'International Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (432, N'ISL', N'International Steels Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (433, N'ITTEFAQ', N'Ittefaq Iron Industries Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (434, N'MUGH', N'Mughal Iron & Steels Ind Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (435, N'AHCL', N'Arif Habib Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (436, N'KSE', N'Engro Corporation Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (437, N'EFERT', N'Engro Fertilizers Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (438, N'FATIMA', N'Fatima Fertilizer Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (439, N'FFBL', N'Fauji Fertilizer Bin Qasim Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (440, N'FAUF', N'Fauji Fertilizer Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (441, N'ASC', N'Al-Shaheer Corporation.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (442, N'PREMA', N'At-Tahur Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (443, N'CLOV', N'Clover Pakistan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (444, N'FAUJ', N'Fauji Foods Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (445, N'FCEPL', N'Frieslandcampina Engro Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (446, N'MFL', N'Matco Foods Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (447, N'MFFL', N'Mitchells Fruit Farms Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (448, N'MUREB', N'Murree Brewery Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (449, N'NATF', N'National Foods Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (450, N'NESTLE', N'Nestle Pakistan Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (451, N'QCFD', N'Quice Food Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (452, N'SHEZ', N'Shezan International Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (453, N'SCL', N'Shield Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (454, N'TOMCL', N'The Organic Meat Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (455, N'TRET', N'Treet Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (456, N'UPFL', N'Unilever Pakistan Foods Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (457, N'ZIL', N'ZIL Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (458, N'BGLS', N'Baluchistan Glass Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (459, N'GHGL', N'Ghani Glass Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (460, N'GGGL', N'Ghani Global Glass Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (461, N'GVGL', N'Ghani Value Glass Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (462, N'KCL', N'Karam Ceramics Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (463, N'STCL', N'Shabbir Tiles and Ceramics Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (464, N'TGL', N'Tariq Glass.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (465, N'AICL', N'Adamjee Insurance Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (466, N'AGICO', N'Askari Gen Insurance Co.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (467, N'ALAC', N'Askari Life Assurance Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (468, N'ATIL', N'Atlas Insurance Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (469, N'CENI', N'Century Insurance Co.Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (470, N'CSIL', N'Cresent Star Insurance Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (471, N'EFUG', N'E. F. U. Gen Insurance Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (472, N'EFUL', N'EFU Life Assurance Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (473, N'IGIHL', N'IGI Holdings Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (474, N'PAKRI', N'Pakistan Reinsurance Comp.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (475, N'PIL', N'PICIC Insurance Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (476, N'SHNI', N'Shaheen Insurance Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (477, N'TPLI', N'TPL Insurance Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (478, N'UNIC', N'United Insurance Company.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (479, N'786', N'786 Investments Limited')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (480, N'AHL', N'Arif Habib Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (481, N'CYAN', N'Cyan Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (482, N'DEL', N'Dawood Equities Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (483, N'DAWH', N'Dawood Hercules Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (484, N'EFGH', N'EFG Hermes Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (485, N'ESBL', N'Escorts Investment Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (486, N'FCSC', N'First Capital Sec.Corp. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (487, N'FCIBL', N'First Credit & Investment Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (488, N'FDIBL', N'First Dawood Investment Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (489, N'FNEL', N'First National Equities Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (490, N'ICIBL', N'Invest Capital Investment Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (491, N'JSCL', N'Jahangir Siddiqui & Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (492, N'NEXT', N'Next Capital Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (493, N'PSX', N'Pakistan Stock Exchange Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (494, N'PASL', N'Pervez Ahmed Consultancy Services Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (495, N'GRYL', N'Grays Leasing Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (496, N'OLPL', N'Orix Leasing Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (497, N'SPLC', N'Saudi Pak Leasing Co. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (498, N'SLL', N'SME Leasing Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (499, N'BATA', N'Bata Pakistan Ltd.')
GO
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (500, N'SERT', N'Service Industries.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (501, N'AKDCL', N'AKD Capital Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (502, N'ECOP', N'ECOPAK Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (503, N'GAMON', N'Gammon Pak. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (504, N'MACFL', N'MACPAC Films Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (505, N'PACE', N'Pace (Pakistan) Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (506, N'PHDL', N'Pakistan Hotels Developers Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (507, N'SHFA', N'Shifa Int. Hospital Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (508, N'STPL', N'Siddiqsons Tin Plate Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (509, N'SPEL', N'Synthetic Products Enterprises Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (510, N'TPLP', N'TPL Properties Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (511, N'TRIPF', N'Tri-Pack Films Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (512, N'FFLM', N'First Fidelity Leasing Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (513, N'FHAM', N'First Habib Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (514, N'FIBLM', N'First IBL Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (515, N'FNBM', N'First National Bank Modaraba. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (516, N'PAKMI', N'First Pak Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (517, N'PMI', N'First Prudential Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (518, N'FUDLM', N'First UDL Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (519, N'ORIXM', N'Orix Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (520, N'SINDM', N'Sindh Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (521, N'MARI', N'Mari Petroleum Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (522, N'OGDCL', N'Oil & Gas Development Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (523, N'POL', N'Pakistan Oilfields Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (524, N'PPL', N'Pakistan Petroleum Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (525, N'APL', N'Attock Petroleum Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (526, N'BPL', N'Burshane LPG (Pakistan) Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (527, N'HASCOL', N'Hascol Petroleum Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (528, N'HTL', N'Hi-Tech Lubricants Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (529, N'PSO', N'Pakistan State Oil Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (530, N'SHEL', N'Shell Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (531, N'SNGP', N'Sui Northern Gas Pipe Line Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (532, N'SSGC', N'Sui Southern Gas Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (535, N'CEPB', N'Century Paper & Board Mills.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (536, N'CPPL', N'Cherat Packaging Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (550, N'SAPL', N'Sanofi-Aventis Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (556, N'KOHE', N'Kohinoor Energy Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (578, N'PMRS', N'Premier Sugar Mills & Distille')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (582, N'SML', N'Shakarganj Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (583, N'TICL', N'Thal Industries Corporation Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (614, N'SFL', N'Sapphire Fibres Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (615, N'SAPT', N'Sapphire Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (620, N'DSIL', N'D. S. Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (624, N'HIRAT', N'Hira Textile Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (625, N'ILTM', N'Island Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (629, N'PRET', N'Premium Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (636, N'STJT', N'Shahtaj Textile Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (639, N'PMPK', N'Philip Morris (Pakistan)Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (641, N'PICT', N'Pakistan Int.Container Terminal.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (648, N'BNWM', N'Bannu Woollen Mills Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (649, N'DCR', N'Dolmen City REIT.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (667, N'CEPB', N'CEPB-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (764, N'AGTL', N'Al-Ghazi Tractors Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (765, N'ATLH', N'Atlas Honda Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (766, N'AGIL', N'Agriautos Industries Co. Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (767, N'ATBA', N'Atlas Battery Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (768, N'EMCO', N'EMCO Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (769, N'NICL', N'Nimir Industrial Chemical Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (770, N'WAHN', N'Wah Noble Chemicals Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (771, N'SBL', N'Samba Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (772, N'BOK', N'The Bank of Khyber.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (773, N'DADX', N'Dadex Eternit Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (774, N'KSBP', N'KSB Pumps Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (775, N'MSCL', N'Metropolitan Steel Corporation')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (776, N'PECO', N'Pakistan Engineering Co Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (777, N'RMPL', N'Rafhan Maize Products Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (778, N'FRCL', N'Frontier Ceramics Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (779, N'HICL', N'Habib Insurance Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (780, N'IGIL', N'IGI Life Insurance Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (781, N'JGICL', N'Jubilee General Insurance Co.Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (782, N'JLICL', N'Jubliee Life Insurance Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (783, N'PKGI', N'Pakistan General Insurance Co. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (784, N'RICL', N'Reliance Insurance.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (785, N'AMBL', N'Apna Microfinance Bank Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (786, N'BIPLS', N'BIPL Securities Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (787, N'MCBAH', N'MCB-Arif Habib Savings & Invest Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (788, N'UBDL', N'United Brands Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (789, N'UDPL', N'United Distributors Pakistan.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (790, N'ARM', N'Allied Rental Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (791, N'FPJM', N'First Punjab Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (792, N'HMMCL', N'Habib Metro Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (793, N'MODAM', N'Modarba Al-Mali.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (794, N'PIM', N'Popular Islamic Madaraba')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (795, N'UCAPM', N'Unicap Modaraba. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (796, N'MERI', N'Merit Packaging Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (797, N'PKGS', N'Packages Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (798, N'PPP', N'Pakistan Paper Products Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (799, N'RPL', N'Roshan Packages Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (800, N'SEPL', N'Security Papers Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (801, N'ABOT', N'Abbott Laboratories Pak Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (802, N'AGP', N'AGP Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (803, N'FEROZ', N'Ferozsons Laboratories Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (804, N'GSKCH', N'Glaxo SmithKline Healthcare Pak Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (805, N'GLAXO', N'Glaxo SmithKline Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (806, N'HINOON', N'Highnoon Laboratories Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (807, N'IBLHL', N'IBL HealthCare Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (808, N'BRR', N'B.R.R. Guardian Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (809, N'FANM', N'First Al-Noor Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (810, N'FECM', N'First Elite Capital Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (811, N'SNGP', N'Sui Northern Gas Pipe Line Ltd.')
GO
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (812, N'OTSU', N'Otsuka Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (813, N'AEL', N'Arshad Energy Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (814, N'EPQL', N'Engro Powergen Qadirpur Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (815, N'TSPL', N'Tri -Star Power Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (816, N'ALNRS', N'Al-Noor Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (817, N'DWSM', N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (818, N'TSMF', N'Tri - Star Mutual Fund Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (819, N'JLICL', N'Jubliee Life Insurance Co Ltd.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (820, N'JSGCL', N'JS Global Capital Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (821, N'CPAL', N'Capital Assets Leasing Corp. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (822, N'LEUL', N'Leather Up Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (823, N'ARPAK', N'Arpak International Investment.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (824, N'FTMM', N'First Treet Manufacturing Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (825, N'JDWS', N'J. D. W. Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (826, N'PMRS', N'Premier Sugar Mills & Distille')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (827, N'SASML', N'Sindh Abadgars Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (828, N'GFIL', N'Ghazi Fabrics International Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (829, N'PAKD', N'Pak Datacom Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (830, N'INKL', N'International Knitwear Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (831, N'DSML', N'Dar-es-Salam Textile Mills Ltd [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (832, N'DWTM', N'Dewan Textile Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (833, N'ELCM', N'Elahi Cotton Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (834, N'FZCM', N'Fazal Cloth Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (835, N'KHSM', N'Khurshid Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (836, N'LMSM', N'Landmark Spinning Mills Limite [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (837, N'NCML', N'Nazir Cotton Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (838, N'SLYT', N'Sally Textile Mills Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (839, N'SITC', N'Sitara Chemicals.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (840, N'TSBL', N'Trust Securities & Brokerage.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (841, N'BFMOD', N'B.F.Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (842, N'FEM', N'First Equity Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (843, N'KASBM', N'KASB Modaraba.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (844, N'TRSM', N'Trust Mod.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (845, N'ALTN', N'Altern Energy Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (846, N'SHJS', N'Shahtaj Suger Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (848, N'IDYM', N'Indus Dyeing & Manufacturing.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (849, N'JKSM', N'J. K. Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (850, N'NAGC', N'Nagina Cotton Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (851, N'SSML', N'Saritow Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (852, N'SERT', N'Service Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (853, N'TATM', N'Tata Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (854, N'ASHT', N'Ashfaq Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (855, N'PRWM', N'Prosperity Weaving Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (856, N'PAKT', N'Pakistan Tobacco Co Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (857, N'JSIL', N'JS Investments Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (858, N'NITG-ETF', N'NIT Pakistan Gateway ETF.	')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (859, N'UBLP-ETF', N'UBL Pakistan Enterprise ETF.(XD)')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (860, N'DNCC', N'Dandot Cement Co. Ltd. [ DEFAULTER SEGMENT ]')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (861, N'ASLPS', N'Aisha Steel Mills Convertibl Pre-Sh')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (862, N'MLKP', N'Nestle Pakistan Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (863, N'PINL', N'Premier Insurance Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (864, N'UVIC', N'Universal Insurance Company Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (865, N'SIBL', N'Security Investment Bank Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (866, N'HRPL', N'Habib Rice Product Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (867, N'TSML', N'Tandiwala Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (868, N'GATI', N'Gatron (Industries) Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (869, N'QUET', N'Quetta Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (870, N'ZAHID', N'Zahidjee Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (871, N'IDRT', N'Idrees Textile Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (872, N'SNAI', N'Sana Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (873, N'POML', N'Punjab Oil Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (875, N'FEROZ', N'FEROZ-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (876, N'PSMC', N'PSMC-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (877, N'PRL', N'PRL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (878, N'POWER', N'POWER-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (879, N'SYS', N'SYS-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (880, N'TGL', N'TGL-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (881, N'TREET', N'TREET-SEP')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (882, N'IBFL', N'Ibrahim Fibre Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (883, N'ANL', N'Azgard Nine Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (884, N'ELSM', N'Ellcot Spinning Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (885, N'JDMT', N'Janana De Malucha Tex Mills.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (886, N'ISIL', N'Ismail Industries Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (887, N'TSML', N'Tandliawala Sugar Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (888, N'ARUJ', N'Aruj Industries Limited.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (889, N'SURC', N'Suraj Cotton Mills Ltd.')
INSERT [dbo].[Company] ([C_ID], [C_SYMBOL], [C_NAME]) VALUES (890, N'DCR', N'DCR-SEP')
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[FUND] ON 

INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (1, 5, N'A FUND', N'AFUND', N'Dummy Fund')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (2, 13, N'B FUND', N'B FUND', N'Dummy Fund')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (5, 34, N'C Fund', N'C Fund', N'C Fund')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (6, 34, N'C', N'C', N'C')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (7, 34, N'D', N'D', N'D')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (8, 34, N'E', N'E', N'E')
INSERT [dbo].[FUND] ([FUND_ID], [FUND_CODE], [FUND_NAME], [FUND_SYMBOL], [FUND_DESC]) VALUES (9, 34, N'F', N'F', N'F')
SET IDENTITY_INSERT [dbo].[FUND] OFF
GO
SET IDENTITY_INSERT [dbo].[MarketSummary] ON 

INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (1, N'Al-Ghazi Tractors Limited. ', N'Al-Ghazi Tractors Limited. ', 402.31, 397.55, 407.5, 397.5, 407.5, 5.19, 3400)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (2, N'Atlas Honda Limited. ', N'Atlas Honda Limited. ', 440, 438, 439.9, 438, 439.9, -0.1, 300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (3, N'Dewan Farooque Motors Limited [ DEFAULTER SEGMENT ]', N'Dewan Farooque Motors Limited [ DEFAULTER SEGMENT ]', 5.99, 6.14, 6.14, 5.9, 6.04, 0.02, 324000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (4, N'Ghandhara Industries Ltd. ', N'Ghandhara Industries Ltd. ', 286.28, 290, 298.1, 290, 294.58, 8.3, 1814400)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (5, N'Ghandhara Nissan Ltd. ', N'Ghandhara Nissan Ltd. ', 112.74, 112.95, 115, 112.5, 113.59, 0.76, 487000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (6, N'Ghani Automobile Industries Ltd. ', N'Ghani Automobile Industries Ltd. ', 6.35, 6.49, 6.72, 6.31, 6.5, 0.2, 720500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (7, N'Hino Pak Motor Limited. ', N'Hino Pak Motor Limited. ', 532, 535, 554.99, 535, 550.71, 22.99, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (8, N'Honda Atlas Cars (Pak) Ltd. ', N'Honda Atlas Cars (Pak) Ltd. ', 330.93, 334, 336.6, 330.93, 333.34, 2.41, 280200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (9, N'Indus Motor Company Ltd. ', N'Indus Motor Company Ltd. ', 1280.22, 1284, 1284, 1255, 1265.62, -15.22, 64650)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (10, N'Millat Tractors Limited. ', N'Millat Tractors Limited. ', 888.95, 890, 890, 879.1, 880.67, -8.85, 5200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (11, N'Pak Suzuki Motors Co Ltd. ', N'Pak Suzuki Motors Co Ltd. ', 237.52, 237, 248, 237, 244.72, 8.48, 763300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (12, N'Sazgar Engineering Works Ltd. ', N'Sazgar Engineering Works Ltd. ', 169.9, 170.8, 174.99, 169.05, 173.31, 3.41, 366800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (13, N'Agriautos Industries Co. Ltd. ', N'Agriautos Industries Co. Ltd. ', 207, 208.25, 208.85, 208, 208.32, 1, 2100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (14, N'Atlas Battery Ltd. ', N'Atlas Battery Ltd. ', 220.42, 224.99, 225.8, 221, 224.18, 3.58, 32100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (15, N'Exide Pakistan Ltd. ', N'Exide Pakistan Ltd. ', 350.03, 346, 358, 345.06, 357.62, 7.77, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (16, N'General Tyre & Rubber Co. ', N'General Tyre & Rubber Co. ', 74.52, 74.09, 76, 74.05, 75.86, 1.13, 243000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (17, N'Loads Limited. ', N'Loads Limited. ', 18.65, 18.51, 19.3, 18.5, 18.88, 0.23, 264500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (18, N'Thal Limited. ', N'Thal Limited. ', 390, 383, 392.5, 383, 390.15, 0, 24700)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (19, N'Johnson & Philips (Pak) Ltd. [ DEFAULTER SEGMENT ]', N'Johnson & Philips (Pak) Ltd. [ DEFAULTER SEGMENT ]', 52.5, 52.55, 52.55, 50.9, 50.9, -1.6, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (20, N'Pak Elektron Ltd. ', N'Pak Elektron Ltd. ', 33.23, 33.28, 33.85, 33.1, 33.71, 0.48, 4197000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (21, N'Pakistan Cables Ltd. ', N'Pakistan Cables Ltd. ', 137.99, 139, 140, 137, 137.09, -0.99, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (22, N'Siemens (Pak) Eng. Co. Ltd. ', N'Siemens (Pak) Eng. Co. Ltd. ', 570, 551, 560, 545.01, 560, -10, 1750)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (23, N'WAVES Singer Pakistan Ltd. ', N'WAVES Singer Pakistan Ltd. ', 26.66, 26.4, 27.05, 26.1, 26.95, 0.29, 1126000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (24, N'Attock Cement Pak Ltd. ', N'Attock Cement Pak Ltd. ', 142.99, 144.9, 148, 143.51, 146.61, 4.01, 90500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (25, N'Bestway Cement Limited. ', N'Bestway Cement Limited. ', 149.38, 149.38, 149.99, 147, 147.38, -1.48, 6500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (26, N'Cherat Cement Co. Ltd. ', N'Cherat Cement Co. Ltd. ', 132.24, 133.1, 136.26, 131.08, 133.32, 1.08, 2251500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (27, N'D. G. Khan Cement Co. Ltd. ', N'D. G. Khan Cement Co. Ltd. ', 106.55, 106.61, 108.44, 105.95, 107.97, 1.75, 4405000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (28, N'Dandot Cement Co. Ltd. [ DEFAULTER SEGMENT ]', N'Dandot Cement Co. Ltd. [ DEFAULTER SEGMENT ]', 8.4, 8, 8, 8, 8, -0.4, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (29, N'Dewan Cement Limited. ', N'Dewan Cement Limited. ', 10.02, 10.5, 11.02, 10.4, 11.02, 1, 23459500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (30, N'Fauji Cement Co Ltd. ', N'Fauji Cement Co Ltd. ', 19.99, 20.01, 20.9, 19.86, 20.76, 0.77, 5388000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (31, N'Fecto Cement Ltd. ', N'Fecto Cement Ltd. ', 29.96, 29.9, 30.5, 29.53, 30.04, -0.06, 48000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (32, N'Flying Cement Company Ltd. ', N'Flying Cement Company Ltd. ', 10.43, 10.25, 10.69, 10.25, 10.51, 0.09, 351500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (33, N'Gharibwal Cement Ltd. ', N'Gharibwal Cement Ltd. ', 24.7, 24.7, 25, 24, 24.82, 0.05, 128000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (34, N'Javedan Corporation Ltd. ', N'Javedan Corporation Ltd. ', 25.5, 25.5, 25.7, 25.5, 25.7, 0.2, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (35, N'Kohat Cement Co. Ltd. ', N'Kohat Cement Co. Ltd. ', 167.43, 168.85, 169.44, 165.01, 168.33, -0.43, 67300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (36, N'Lucky Cement Limited. ', N'Lucky Cement Limited. ', 597.86, 597, 619, 590.49, 611.1, 16.15, 1816754)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (37, N'Maple Leaf Cement Factory Ltd. ', N'Maple Leaf Cement Factory Ltd. ', 34.18, 34.45, 35.1, 34.05, 34.91, 0.73, 8843000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (38, N'Pioneer Cement Ltd. ', N'Pioneer Cement Ltd. ', 87.47, 87.47, 89, 85.99, 88.34, 1.23, 3116500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (39, N'Power cement Limited ', N'Power cement Limited ', 8.87, 9.05, 9.25, 8.9, 9.03, 0.16, 8269000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (40, N'Power cement Limited(R1) ', N'Power cement Limited(R1) ', 0.54, 0.52, 0.61, 0.5, 0.52, -0.02, 3676000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (41, N'Safe Mix Concrete Ltd. ', N'Safe Mix Concrete Ltd. ', 8.6, 8.97, 9, 8.5, 8.63, 0.02, 44500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (42, N'Thatta Cement Company Ltd. ', N'Thatta Cement Company Ltd. ', 10.53, 10.89, 10.89, 10.4, 10.59, 0.07, 84000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (43, N'Agritech Limited ', N'Agritech Limited ', 5.3, 5.43, 5.49, 5.32, 5.36, 0.04, 413000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (44, N'Archroma Pakistan Limited. ', N'Archroma Pakistan Limited. ', 562, 556, 562, 539, 540.93, -23, 18150)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (45, N'Bawany Air Products Ltd. [ DEFAULTER SEGMENT ]', N'Bawany Air Products Ltd. [ DEFAULTER SEGMENT ]', 18.22, 19.22, 19.22, 18.91, 18.91, 0.69, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (46, N'Berger Paints Pakistan Ltd. ', N'Berger Paints Pakistan Ltd. ', 79.39, 81.49, 84, 81.1, 82.17, 2.61, 46500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (47, N'Bifo Industries Ltd. ', N'Bifo Industries Ltd. ', 160, 163, 166.99, 162.5, 163.17, 3, 16000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (48, N'Buxly Paints Ltd. ', N'Buxly Paints Ltd. ', 37.5, 39.97, 39.97, 39.97, 39.97, 2.47, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (49, N'Colgate Palmolive (Pak) Ltd. ', N'Colgate Palmolive (Pak) Ltd. ', 2399.99, 2399.99, 2399.99, 2399.99, 2399.99, 0, 140)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (50, N'Descon Oxychem Ltd. ', N'Descon Oxychem Ltd. ', 29.85, 30, 30.28, 29.71, 29.93, 0.2, 295500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (51, N'DYNEA Pak. ', N'DYNEA Pak. ', 144.63, 147.49, 147.9, 144, 146.88, 1.37, 31500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (52, N'Engro Polymer & Chemicals Ltd. ', N'Engro Polymer & Chemicals Ltd. ', 33.61, 33.91, 35.49, 33.85, 34.52, 1.38, 7873000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (53, N'Ghani Global Holdings Limited. ', N'Ghani Global Holdings Limited. ', 16, 15.99, 17.2, 15.99, 16.63, 0.7, 3086000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (54, N'ICI Pakistan Limited. ', N'ICI Pakistan Limited. ', 763.26, 760, 784.95, 740, 772.41, 16.74, 55350)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (55, N'Ittehad Chemical Ltd. ', N'Ittehad Chemical Ltd. ', 28.5, 28.22, 28.49, 28, 28.01, -0.49, 68000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (56, N'Lotte Chemical Pakistan Ltd. ', N'Lotte Chemical Pakistan Ltd. ', 12.43, 12.5, 12.55, 12.16, 12.47, 0.07, 2602000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (57, N'Nimir Resins Limited. ', N'Nimir Resins Limited. ', 7.29, 7.21, 7.51, 7.06, 7.44, 0.22, 645000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (58, N'Pakistan Oxygen Limited. ', N'Pakistan Oxygen Limited. ', 157.28, 159, 159.9, 155, 158.12, 1.11, 24900)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (59, N'Pakistan PVC [ DEFAULTER SEGMENT ]', N'Pakistan PVC [ DEFAULTER SEGMENT ]', 3.35, 3.08, 3.3, 3.08, 3.25, -0.05, 4500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (60, N'Sardar Chemical IndustriesLtd. ', N'Sardar Chemical IndustriesLtd. ', 18, 17.01, 18.85, 17.01, 18.29, 0.85, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (61, N'Sitara Chemicals. ', N'Sitara Chemicals. ', 320, 318.95, 318.95, 313.15, 316.05, -6.85, 800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (62, N'Sitara Peroxide Limited ', N'Sitara Peroxide Limited ', 23.35, 23.1, 23.18, 22.5, 22.81, -0.55, 667500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (63, N'HBL Growth Fund ', N'HBL Growth Fund ', 11.5, 11.55, 11.55, 11.55, 11.55, 0.05, 8000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (64, N'HBL Investment Fund ', N'HBL Investment Fund ', 4.1, 4, 4, 4, 4, -0.1, 3500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (65, N'Allied Bank Ltd. ', N'Allied Bank Ltd. ', 87.61, 91.91, 94.1, 87, 87.88, -0.41, 17500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (66, N'Askari Bank Limited. ', N'Askari Bank Limited. ', 19.05, 19.45, 19.45, 19.05, 19.4, 0.35, 146500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (67, N'Bank Al-Habib Ltd. ', N'Bank Al-Habib Ltd. ', 65.52, 65.52, 66.39, 65.11, 65.29, -0.24, 153357)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (68, N'Bank Alfalah Ltd. ', N'Bank Alfalah Ltd. ', 36.95, 36.69, 37.2, 36.36, 36.65, -0.3, 762587)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (69, N'Bank Of Punjab. ', N'Bank Of Punjab. ', 10.05, 10.15, 10.19, 10.05, 10.16, 0.12, 1990500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (70, N'Bankislami Pakistan Ltd. ', N'Bankislami Pakistan Ltd. ', 7.84, 7.9, 7.9, 7.8, 7.86, 0.01, 214500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (71, N'Faysal Bank Limited. ', N'Faysal Bank Limited. ', 17.51, 17.59, 18, 17.3, 17.37, -0.21, 545000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (72, N'Habib Bank Limited. ', N'Habib Bank Limited. ', 124.27, 124, 126.15, 123.5, 125.1, 0.83, 437781)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (73, N'Habib Metropolitn Bank Limited. ', N'Habib Metropolitn Bank Limited. ', 34.88, 34.9, 34.9, 34.02, 34.3, -0.58, 10000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (74, N'JS Bank Limited. ', N'JS Bank Limited. ', 5.52, 5.51, 5.74, 5.51, 5.68, 0.16, 1186500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (75, N'MCB Bank Limited. ', N'MCB Bank Limited. ', 169.6, 170, 171, 167.11, 168.51, -1.09, 164035)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (76, N'Meezan Bank Limited. ', N'Meezan Bank Limited. ', 85.95, 86, 88, 83, 85.92, 0.05, 1510500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (77, N'National Bank Of Pakistan. ', N'National Bank Of Pakistan. ', 35.14, 35.48, 37.24, 35.02, 36.53, 1.56, 9059500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (78, N'Samba Bank Limited. ', N'Samba Bank Limited. ', 7.5, 7.75, 8, 7.75, 8, 0.5, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (79, N'Silk Bank Limited. ', N'Silk Bank Limited. ', 0.82, 0.83, 0.87, 0.82, 0.83, 0, 269500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (80, N'Soneri Bank Ltd. ', N'Soneri Bank Ltd. ', 9.34, 9.49, 9.49, 9, 9.24, -0.13, 123000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (81, N'Standard Chartered Bank Pak Ltd. ', N'Standard Chartered Bank Pak Ltd. ', 32.8, 32, 32, 31.31, 31.37, -1.48, 146500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (82, N'Summit Bank Limited. [ DEFAULTER SEGMENT ]', N'Summit Bank Limited. [ DEFAULTER SEGMENT ]', 1.71, 1.74, 1.78, 1.66, 1.68, -0.03, 407500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (83, N'United Bank Ltd. ', N'United Bank Ltd. ', 118.6, 118.98, 118.98, 117.5, 118.2, -0.4, 283621)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (84, N'Ados Pakistan Limited. ', N'Ados Pakistan Limited. ', 25.25, 25.25, 25.25, 25.25, 25.25, 0, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (85, N'Aisha Steel Mills Limited. ', N'Aisha Steel Mills Limited. ', 13.92, 14.05, 14.25, 13.9, 14.15, 0.28, 2848500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (86, N'Amreli Steels Limited. ', N'Amreli Steels Limited. ', 47.05, 47.35, 49.75, 47.02, 49.43, 2.38, 3423000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (87, N'Bolan Casting Ltd. ', N'Bolan Casting Ltd. ', 74.94, 75, 78, 75, 75.27, 0.06, 17500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (88, N'Crescent Steel & Allied Product. ', N'Crescent Steel & Allied Product. ', 69.4, 69.5, 69.5, 67.75, 68.14, -1.39, 345500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (89, N'Dost Steels Ltd. [ DEFAULTER SEGMENT ]', N'Dost Steels Ltd. [ DEFAULTER SEGMENT ]', 4.17, 4.21, 4.34, 4.09, 4.12, -0.04, 2198500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (90, N'Drekkar Kingsway Ltd. [ DEFAULTER SEGMENT ]', N'Drekkar Kingsway Ltd. [ DEFAULTER SEGMENT ]', 3.4, 3.4, 3.55, 3.31, 3.55, 0.15, 45000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (91, N'International Industries Ltd. ', N'International Industries Ltd. ', 131.04, 132.5, 134.97, 131.2, 134.11, 3.93, 931500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (92, N'International Steels Limited. ', N'International Steels Limited. ', 71.03, 71.75, 72.5, 70.51, 72.18, 1.15, 2402000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (93, N'Ittefaq Iron Industries Limited. ', N'Ittefaq Iron Industries Limited. ', 10.31, 10.45, 10.74, 10.25, 10.65, 0.29, 458000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (94, N'KSB Pumps Co Ltd. ', N'KSB Pumps Co Ltd. ', 162.55, 154, 164.9, 154, 159.63, -2.92, 11500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (95, N'Metropolitan Steel Corporation ', N'Metropolitan Steel Corporation ', 9.32, 10.32, 10.32, 9.82, 9.85, 0.53, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (96, N'Mughal Iron & Steels Ind Ltd. ', N'Mughal Iron & Steels Ind Ltd. ', 61.55, 61.89, 66.16, 61.88, 66.16, 4.61, 3931500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (97, N'Arif Habib Corporation Ltd. ', N'Arif Habib Corporation Ltd. ', 37.66, 38, 38, 37.9, 38, 0.34, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (98, N'Engro Corporation Limited. ', N'Engro Corporation Limited. ', 292.57, 292.97, 293.78, 289.25, 292.79, 0.43, 295272)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (99, N'Engro Fertilizers Limited. ', N'Engro Fertilizers Limited. ', 61.77, 61.81, 62, 61.25, 61.39, -0.37, 4104860)
GO
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (100, N'Fatima Fertilizer Co Ltd. ', N'Fatima Fertilizer Co Ltd. ', 26.25, 26.25, 26.25, 26.01, 26.2, -0.05, 8000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (101, N'Fauji Fertilizer Bin Qasim Ltd. ', N'Fauji Fertilizer Bin Qasim Ltd. ', 18.34, 18.32, 18.52, 18.32, 18.38, 0.04, 958500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (102, N'Fauji Fertilizer Co. Ltd. ', N'Fauji Fertilizer Co. Ltd. ', 109.3, 109, 110.89, 107.92, 108.33, -0.97, 1826773)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (103, N'Al-Shaheer Corporation. ', N'Al-Shaheer Corporation. ', 12.79, 12.9, 13.03, 12.85, 12.9, 0.24, 208000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (104, N'At-Tahur Ltd. ', N'At-Tahur Ltd. ', 18.55, 18.5, 18.89, 18.5, 18.84, 0.33, 44000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (105, N'Clover Pakistan Limited. ', N'Clover Pakistan Limited. ', 92.89, 94, 99.85, 93.95, 99.32, 6.96, 138500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (106, N'Fauji Foods Limited. ', N'Fauji Foods Limited. ', 12.73, 12.73, 12.88, 12.56, 12.83, 0.1, 1718000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (107, N'Frieslandcampina Engro Pakistan Ltd. ', N'Frieslandcampina Engro Pakistan Ltd. ', 86.07, 89.49, 91.5, 86.5, 87.65, 1.53, 1433500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (108, N'Ismail Industries Ltd. ', N'Ismail Industries Ltd. ', 345.02, 320, 320, 320, 320, -25.02, 100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (109, N'Matco Foods Limited. ', N'Matco Foods Limited. ', 22.5, 22.21, 22.29, 22, 22.18, -0.39, 103000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (110, N'Murree Brewery Company Ltd. ', N'Murree Brewery Company Ltd. ', 563.5, 555, 570, 555, 563.1, -0.4, 200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (111, N'National Foods Ltd. ', N'National Foods Ltd. ', 234.8, 236.5, 236.5, 230.03, 233.93, -4.77, 14700)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (112, N'Nestle Pakistan Ltd.(XD) ', N'Nestle Pakistan Ltd.(XD) ', 6150.01, 6050, 6289.99, 6050, 6100, -50.01, 120)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (113, N'Quice Food Industries Ltd. ', N'Quice Food Industries Ltd. ', 4.85, 4.85, 4.87, 4.75, 4.79, -0.07, 170500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (114, N'Rafhan Maize Products Ltd. ', N'Rafhan Maize Products Ltd. ', 8025, 8000, 8000, 8000, 8000, -25, 40)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (115, N'Shezan International Ltd. ', N'Shezan International Ltd. ', 218.5, 220.99, 222, 220, 220, 1.5, 2400)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (116, N'Shield Corporation Ltd. ', N'Shield Corporation Ltd. ', 220.02, 212.01, 212.01, 212.01, 212.01, -8.01, 200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (117, N'The Organic Meat Company Ltd. ', N'The Organic Meat Company Ltd. ', 21.37, 21.26, 21.4, 21.1, 21.29, 0.01, 140000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (118, N'Treet Corporation Ltd. ', N'Treet Corporation Ltd. ', 23.26, 23.2, 23.95, 23.1, 23.59, 0.29, 1485000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (119, N'Baluchistan Glass Ltd. ', N'Baluchistan Glass Ltd. ', 6.37, 6.34, 6.5, 6.31, 6.34, 0.03, 184500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (120, N'Frontier Ceramics Ltd. ', N'Frontier Ceramics Ltd. ', 14.5, 13.42, 14.46, 13.42, 14.35, -0.15, 12500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (121, N'Ghani Glass Ltd. ', N'Ghani Glass Ltd. ', 49.57, 49.57, 49.57, 49, 49.36, -0.13, 20000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (122, N'Ghani Global Glass Limited. ', N'Ghani Global Glass Limited. ', 14.96, 14.96, 15.7, 14.8, 15.23, 0.27, 1013000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (123, N'Ghani Value Glass Limited. ', N'Ghani Value Glass Limited. ', 42.75, 40.93, 41.77, 40.93, 41.77, -0.98, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (124, N'Shabbir Tiles and Ceramics Limited. ', N'Shabbir Tiles and Ceramics Limited. ', 10.76, 10.78, 10.89, 10.61, 10.65, -0.05, 356500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (125, N'Tariq Glass. ', N'Tariq Glass. ', 84.54, 84.5, 90.88, 82.52, 89.18, 6.34, 3685000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (126, N'Adamjee Insurance Co. Ltd. ', N'Adamjee Insurance Co. Ltd. ', 40, 40.69, 42.8, 40.69, 42.15, 2, 261500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (127, N'Askari Gen Insurance Co. ', N'Askari Gen Insurance Co. ', 22.01, 22.05, 22.05, 22, 22, -0.01, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (128, N'Askari Life Assurance Company Ltd. ', N'Askari Life Assurance Company Ltd. ', 8.45, 8.44, 8.44, 8.44, 8.44, -0.01, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (129, N'Century Insurance Co.Ltd. ', N'Century Insurance Co.Ltd. ', 23, 22.5, 22.5, 21.5, 21.81, -1, 8500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (130, N'Cresent Star Insurance Ltd. ', N'Cresent Star Insurance Ltd. ', 2.31, 2.27, 2.39, 2.27, 2.35, 0.08, 198500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (131, N'E. F. U. Gen Insurance Ltd. ', N'E. F. U. Gen Insurance Ltd. ', 110, 113, 113, 108, 108, -2, 23600)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (132, N'EFU Life Assurance Ltd. ', N'EFU Life Assurance Ltd. ', 194.1, 0, 0, 0, 194.1, 0, 700)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (133, N'Habib Insurance Co Ltd. ', N'Habib Insurance Co Ltd. ', 9.4, 8.79, 9.81, 8.75, 9.4, 0, 85000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (134, N'IGI Holdings Limited. ', N'IGI Holdings Limited. ', 203.45, 204, 207, 202.25, 204.34, 3.55, 61200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (135, N'Jubilee General Insurance Co.Ltd. ', N'Jubilee General Insurance Co.Ltd. ', 43.5, 43.21, 43.21, 43.21, 43.21, -0.29, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (136, N'Jubliee Life Insurance Co Ltd. ', N'Jubliee Life Insurance Co Ltd. ', 360, 350.05, 360, 347, 353.06, -6.94, 6800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (137, N'Pakistan General Insurance Co. [ DEFAULTER SEGMENT ]', N'Pakistan General Insurance Co. [ DEFAULTER SEGMENT ]', 2, 1.86, 2, 1.85, 2, 0, 8000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (138, N'Pakistan Reinsurance Comp. ', N'Pakistan Reinsurance Comp. ', 24.93, 24.79, 26.25, 24.51, 25.82, 1.07, 385000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (139, N'PICIC Insurance Ltd. [ DEFAULTER SEGMENT ]', N'PICIC Insurance Ltd. [ DEFAULTER SEGMENT ]', 0.85, 0.84, 0.84, 0.8, 0.8, -0.05, 28000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (140, N'Premier Insurance Limited. ', N'Premier Insurance Limited. ', 5.5, 5.5, 5.5, 5.5, 5.5, 0, 214500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (141, N'Reliance Insurance. ', N'Reliance Insurance. ', 6.55, 6.35, 6.35, 6.25, 6.25, -0.3, 4000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (142, N'Shaheen Insurance Co Ltd. ', N'Shaheen Insurance Co Ltd. ', 3.87, 4, 4, 4, 4, 0.13, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (143, N'TPL Insurance Ltd. ', N'TPL Insurance Ltd. ', 23.5, 24.45, 24.45, 24.45, 24.45, 0.95, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (144, N'United Insurance Company. ', N'United Insurance Company. ', 7.52, 7.9, 7.9, 7.6, 7.6, 0.08, 5000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (145, N'786 Investments Limited ', N'786 Investments Limited ', 26.57, 24.58, 24.58, 24.58, 24.58, -1.99, 88500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (146, N'Arif Habib Limited. ', N'Arif Habib Limited. ', 49.11, 49.5, 49.5, 48.8, 48.94, -0.11, 7500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (147, N'Cyan Limited. ', N'Cyan Limited. ', 30.67, 31.6, 31.68, 31.6, 31.66, 1.01, 11000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (148, N'Dawood Equities Ltd. ', N'Dawood Equities Ltd. ', 4.08, 4.05, 4.05, 4.05, 4.05, -0.03, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (149, N'Dawood Hercules Corporation Ltd. ', N'Dawood Hercules Corporation Ltd. ', 127.61, 128, 134.35, 127.01, 131.49, 3.88, 127000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (150, N'EFG Hermes Pakistan Ltd. ', N'EFG Hermes Pakistan Ltd. ', 22.94, 22.21, 22.21, 22.01, 22.01, -0.93, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (151, N'Escorts Investment Bank Ltd. ', N'Escorts Investment Bank Ltd. ', 9.33, 9.4, 9.7, 9.25, 9.4, 0.07, 60000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (152, N'First Capital Sec.Corp. Ltd. ', N'First Capital Sec.Corp. Ltd. ', 1.06, 1.05, 1.37, 1.05, 1.21, 0.14, 8191000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (153, N'First Credit & Investment Bank Ltd. ', N'First Credit & Investment Bank Ltd. ', 6.66, 7.05, 7.24, 6.7, 6.98, 0.39, 54000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (154, N'First Dawood Investment Bank Ltd. ', N'First Dawood Investment Bank Ltd. ', 1.59, 1.63, 1.86, 1.63, 1.84, 0.24, 827500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (155, N'First National Equities Limited. ', N'First National Equities Limited. ', 12.95, 13.1, 13.18, 12.9, 13.06, 0.23, 132000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (156, N'Invest Capital Investment Bank Ltd. ', N'Invest Capital Investment Bank Ltd. ', 0.77, 0.85, 0.85, 0.78, 0.82, 0.05, 213500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (157, N'Jahangir Siddiqui & Company Ltd. ', N'Jahangir Siddiqui & Company Ltd. ', 16.08, 16.05, 16.5, 16.03, 16.3, 0.26, 2123000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (158, N'JS Global Capital Limited. ', N'JS Global Capital Limited. ', 63, 61, 61, 61, 61, -2, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (159, N'JS Investments Limited. ', N'JS Investments Limited. ', 18.08, 17.39, 17.39, 17.25, 17.25, -0.83, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (160, N'MCB-Arif Habib Savings & Invest Ltd. ', N'MCB-Arif Habib Savings & Invest Ltd. ', 30.98, 32, 32.8, 32, 32.8, 1.82, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (161, N'Next Capital Limited. ', N'Next Capital Limited. ', 9.89, 9.03, 9.35, 9, 9.02, -0.89, 19000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (162, N'Pakistan Stock Exchange Limited. ', N'Pakistan Stock Exchange Limited. ', 13.21, 13.4, 13.4, 13, 13.27, 0.04, 1215500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (163, N'Pervez Ahmed Consultancy Services Ltd. ', N'Pervez Ahmed Consultancy Services Ltd. ', 0.89, 1.03, 1.03, 0.92, 0.95, 0.06, 407500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (164, N'Grays Leasing Ltd. ', N'Grays Leasing Ltd. ', 6.81, 7.8, 7.8, 5.85, 6.23, -0.66, 64500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (165, N'Orix Leasing Pakistan Ltd. ', N'Orix Leasing Pakistan Ltd. ', 27.85, 27.55, 27.55, 27.55, 27.55, -0.3, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (166, N'Saudi Pak Leasing Co. [ DEFAULTER SEGMENT ]', N'Saudi Pak Leasing Co. [ DEFAULTER SEGMENT ]', 0.68, 0.62, 0.7, 0.62, 0.62, -0.06, 52000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (167, N'SME Leasing Ltd. ', N'SME Leasing Ltd. ', 2.1, 2.75, 2.75, 2.75, 2.75, 0.65, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (168, N'Bata Pakistan Ltd. ', N'Bata Pakistan Ltd. ', 1589.99, 1590, 1615, 1550, 1610, 20.01, 1460)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (169, N'Leather Up Ltd. ', N'Leather Up Ltd. ', 13.41, 13.79, 13.99, 13.79, 13.99, 0.58, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (170, N'Service Industries. ', N'Service Industries. ', 710.21, 711, 714, 703, 709.99, -0.22, 13250)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (171, N'Arpak International Investment. ', N'Arpak International Investment. ', 120, 120, 120, 120, 120, 0, 7000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (172, N'ECOPAK Limited. ', N'ECOPAK Limited. ', 19.56, 20, 21.02, 19, 21.01, 1.46, 600000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (173, N'Gammon Pak. [ DEFAULTER SEGMENT ]', N'Gammon Pak. [ DEFAULTER SEGMENT ]', 14.96, 15, 15, 15, 15, 0.04, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (174, N'MACPAC Films Limited. ', N'MACPAC Films Limited. ', 21.56, 21.56, 22.49, 20.5, 21.26, 0.44, 63500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (175, N'Olympia Mills Limited. [ DEFAULTER SEGMENT ]', N'Olympia Mills Limited. [ DEFAULTER SEGMENT ]', 10.65, 10.65, 10.7, 10.65, 10.7, 0.05, 8000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (176, N'Pace (Pakistan) Ltd. ', N'Pace (Pakistan) Ltd. ', 3.2, 3.29, 3.88, 3.29, 3.53, 0.3, 23457000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (177, N'Pakistan Hotels Developers Ltd. ', N'Pakistan Hotels Developers Ltd. ', 96, 97.98, 97.98, 93, 96, 0, 32000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (178, N'Shifa Int. Hospital Ltd. ', N'Shifa Int. Hospital Ltd. ', 237.13, 241, 241, 233.81, 234.7, -2.07, 30700)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (179, N'Siddiqsons Tin Plate Ltd. ', N'Siddiqsons Tin Plate Ltd. ', 11.79, 11.79, 12.33, 11.72, 12.21, 0.42, 2862500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (180, N'Synthetic Products Enterprises Ltd. ', N'Synthetic Products Enterprises Ltd. ', 42.41, 43, 43.25, 42.25, 42.25, -0.16, 40500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (181, N'TPL Properties Limited. ', N'TPL Properties Limited. ', 10, 10.3, 10.35, 9.36, 9.44, -0.56, 2416000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (182, N'Tri-Pack Films Ltd. ', N'Tri-Pack Films Ltd. ', 134.94, 134, 134.8, 130, 132.05, -2.44, 227500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (183, N'United Brands Limited. ', N'United Brands Limited. ', 27.5, 27.13, 27.5, 27.13, 27.5, 0, 3500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (184, N'Allied Rental Modaraba. ', N'Allied Rental Modaraba. ', 10.8, 11.1, 11.1, 11.1, 11.1, 0.3, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (185, N'First Elite Capital Mod. ', N'First Elite Capital Mod. ', 3.05, 2.95, 2.97, 2.85, 2.85, -0.2, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (186, N'First Habib Mod. ', N'First Habib Mod. ', 10.95, 10.9, 10.95, 10.9, 10.95, 0, 49500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (187, N'First Pak Mod. ', N'First Pak Mod. ', 1.59, 1.58, 1.59, 1.43, 1.59, 0, 43000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (188, N'First Prudential Mod. ', N'First Prudential Mod. ', 1.02, 1, 1.03, 0.95, 0.95, -0.07, 10500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (189, N'First UDL Mod. ', N'First UDL Mod. ', 7.5, 7.6, 7.6, 7.6, 7.6, 0.1, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (190, N'Habib Metro Modaraba. ', N'Habib Metro Modaraba. ', 10, 9.06, 9.75, 9.05, 9.75, -0.25, 3500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (191, N'KASB Modaraba. ', N'KASB Modaraba. ', 1, 1, 1, 1, 1, 0, 5000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (192, N'Modarba Al-Mali. ', N'Modarba Al-Mali. ', 2.7, 2.25, 2.75, 2.02, 2.75, 0.05, 14000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (193, N'Orix Modaraba. ', N'Orix Modaraba. ', 18.5, 18.21, 18.21, 18.1, 18.18, -0.4, 3000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (194, N'Sindh Modaraba. ', N'Sindh Modaraba. ', 10.99, 10.2, 10.85, 10, 10.5, -0.49, 75500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (195, N'Mari Petroleum Company Ltd. ', N'Mari Petroleum Company Ltd. ', 1342.66, 1343.07, 1343.07, 1330, 1330.88, -7.71, 11380)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (196, N'Oil & Gas Development Company Ltd. ', N'Oil & Gas Development Company Ltd. ', 114.92, 114.5, 114.5, 111.8, 113.09, -1.57, 1379455)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (197, N'Pakistan Oilfields Limited. ', N'Pakistan Oilfields Limited. ', 421.86, 422, 424.5, 413.8, 415.21, -7.86, 461002)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (198, N'Pakistan Petroleum Limited. ', N'Pakistan Petroleum Limited. ', 100.26, 99.7, 99.7, 97, 98.84, -1.12, 4658225)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (199, N'Attock Petroleum Limited. ', N'Attock Petroleum Limited. ', 340.17, 341, 341, 338.15, 340.33, 0.82, 6800)
GO
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (200, N'Burshane LPG (Pakistan) Limited. ', N'Burshane LPG (Pakistan) Limited. ', 29.25, 30, 30, 29.25, 29.5, 0.25, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (201, N'Hascol Petroleum Ltd. ', N'Hascol Petroleum Ltd. ', 15.52, 15.45, 16.18, 15.4, 16.06, 0.54, 16813000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (202, N'Hi-Tech Lubricants Limited. ', N'Hi-Tech Lubricants Limited. ', 36.21, 36.79, 37.73, 36.5, 36.97, 0.8, 604000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (203, N'Pakistan State Oil Co Ltd. ', N'Pakistan State Oil Co Ltd. ', 186.52, 185, 190.25, 185, 189.15, 2.63, 1556612)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (204, N'Shell Pakistan Ltd. ', N'Shell Pakistan Ltd. ', 227.51, 228.01, 244.57, 228, 244.57, 17.06, 329400)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (205, N'Sui Northern Gas Pipe Line Ltd.(XD) ', N'Sui Northern Gas Pipe Line Ltd.(XD) ', 63.34, 63.8, 64.9, 63.3, 64.58, 1.24, 4062500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (206, N'Sui Southern Gas Co Ltd. ', N'Sui Southern Gas Co Ltd. ', 17.36, 17.37, 17.6, 17.2, 17.25, -0.11, 2774000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (207, N'Baluchistan Particle Board Ltd [ DEFAULTER SEGMENT ]', N'Baluchistan Particle Board Ltd [ DEFAULTER SEGMENT ]', 16.56, 16.5, 17.68, 16.5, 17.26, 1.12, 32000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (208, N'Century Paper & Board Mills. ', N'Century Paper & Board Mills. ', 86.98, 87.9, 89.75, 86.03, 86.99, 0.01, 1867000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (209, N'Cherat Packaging Limited. ', N'Cherat Packaging Limited. ', 166.01, 170, 178.46, 165, 178.46, 12.45, 409400)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (210, N'Merit Packaging Ltd. ', N'Merit Packaging Ltd. ', 12.94, 12.85, 13, 12.72, 12.9, -0.04, 206000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (211, N'Packages Ltd. ', N'Packages Ltd. ', 408.32, 407.98, 419, 405, 415.8, 6.68, 395300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (212, N'Roshan Packages Limited. ', N'Roshan Packages Limited. ', 25.68, 25.5, 27.1, 25.5, 26.91, 1.23, 1050000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (213, N'Security Papers Ltd.(XD) ', N'Security Papers Ltd.(XD) ', 153.71, 151.2, 155, 151.2, 154.47, 0.44, 10900)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (214, N'Abbott Laboratories Pak Ltd. ', N'Abbott Laboratories Pak Ltd. ', 690, 690, 699, 685, 690.49, 0.15, 230100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (215, N'AGP Limited. ', N'AGP Limited. ', 115.8, 115.5, 116, 112.01, 114.55, -1.25, 336000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (216, N'Ferozsons Laboratories Ltd. ', N'Ferozsons Laboratories Ltd. ', 360.62, 363, 387.66, 361.5, 386.7, 27.04, 1180200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (217, N'Glaxo SmithKline Healthcare Pak Ltd. ', N'Glaxo SmithKline Healthcare Pak Ltd. ', 310.03, 310.04, 318, 310, 316.19, 6.97, 24800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (218, N'Glaxo SmithKline Pakistan Ltd. ', N'Glaxo SmithKline Pakistan Ltd. ', 176.54, 179.99, 187.47, 179.01, 184.96, 8.42, 301900)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (219, N'Highnoon Laboratories Ltd. ', N'Highnoon Laboratories Ltd. ', 596.53, 597, 618, 590, 611.85, 14.97, 248700)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (220, N'IBL HealthCare Limited. ', N'IBL HealthCare Limited. ', 76.06, 72.6, 78.5, 72.6, 78.18, 2.04, 100500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (221, N'Macter International Limited. ', N'Macter International Limited. ', 132, 132, 132, 132, 132, 0, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (222, N'Sanofi-Aventis Pakistan Ltd. ', N'Sanofi-Aventis Pakistan Ltd. ', 858.17, 874.98, 874.98, 874.98, 874.98, 16.81, 50)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (223, N'The Searle Company Ltd. ', N'The Searle Company Ltd. ', 248.79, 250.05, 261, 249, 259.97, 11.18, 2462600)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (224, N'Altern Energy Ltd. ', N'Altern Energy Ltd. ', 26, 26, 26, 25.8, 25.89, -0.1, 25500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (225, N'Arshad Energy Limited. ', N'Arshad Energy Limited. ', 5.97, 5.97, 5.97, 5.9, 5.9, -0.07, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (226, N'Engro Powergen Qadirpur Ltd. ', N'Engro Powergen Qadirpur Ltd. ', 23.21, 23.11, 23.49, 23.11, 23.41, 0.22, 71500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (227, N'Hub Power Company Limited. ', N'Hub Power Company Limited. ', 80.59, 80.5, 81.65, 80.1, 80.44, -0.19, 2533165)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (228, N'K-Electric Limited. ', N'K-Electric Limited. ', 3.71, 3.72, 3.74, 3.66, 3.71, -0.01, 2900500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (229, N'Kohinoor Energy Ltd. ', N'Kohinoor Energy Ltd. ', 36, 35.7, 35.7, 35.7, 35.7, -0.3, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (230, N'Kohinoor Power Co Ltd. [ DEFAULTER SEGMENT ]', N'Kohinoor Power Co Ltd. [ DEFAULTER SEGMENT ]', 2.19, 2.03, 2.19, 2.02, 2.15, 0, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (231, N'Kot Addu Power Company. ', N'Kot Addu Power Company. ', 27.66, 27.85, 28.2, 27.5, 28.01, 0.24, 5503000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (232, N'LALPIR Power Limited. ', N'LALPIR Power Limited. ', 13.15, 13.21, 14.15, 13.2, 13.3, 0.15, 1316500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (233, N'Nishat Chunian Power Ltd. ', N'Nishat Chunian Power Ltd. ', 14.62, 14.55, 14.75, 14.4, 14.44, -0.2, 525000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (234, N'Nishat Power Limited.(XD) ', N'Nishat Power Limited.(XD) ', 23.34, 23.2, 23.5, 22.65, 22.87, -0.34, 69000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (235, N'Pakgen Power Limited. ', N'Pakgen Power Limited. ', 18.13, 17.55, 17.56, 17.27, 17.44, -0.83, 40000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (236, N'Saif Power Ltd. ', N'Saif Power Ltd. ', 17.22, 17.3, 17.3, 16.95, 17, -0.23, 137000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (237, N'Tri -Star Power Ltd. ', N'Tri -Star Power Ltd. ', 3.82, 3.9, 4, 3.9, 3.99, 0.17, 27000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (238, N'Attock Refinery Limited. ', N'Attock Refinery Limited. ', 166.68, 168, 173.48, 164.65, 171.98, 5.3, 3001000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (239, N'BYCO Petroleum Pak Ltd. ', N'BYCO Petroleum Pak Ltd. ', 7.61, 7.6, 7.9, 7.56, 7.81, 0.19, 1832000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (240, N'National Refinary Ltd. ', N'National Refinary Ltd. ', 184.79, 185, 189.29, 183.5, 188.32, 3.49, 688000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (241, N'Pakistan Refinery Ltd. ', N'Pakistan Refinery Ltd. ', 15.63, 15.7, 16.8, 15.5, 16.8, 1.17, 19421500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (242, N'Abdullah Shaha Ghazi Suger Mills Ltd. ', N'Abdullah Shaha Ghazi Suger Mills Ltd. ', 2.3, 2.38, 2.38, 2.25, 2.25, -0.05, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (243, N'Adam Sugar Mills Limited. [ DEFAULTER SEGMENT ]', N'Adam Sugar Mills Limited. [ DEFAULTER SEGMENT ]', 21, 21, 21, 19.43, 19.9, -1.1, 58500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (244, N'Al-Abbas Sugar Mills Ltd.(XD) ', N'Al-Abbas Sugar Mills Ltd.(XD) ', 317, 317, 317, 317, 317, 0, 100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (245, N'Al-Noor Sugar Mills Ltd. ', N'Al-Noor Sugar Mills Ltd. ', 49.43, 48.1, 49.5, 48, 49.5, 0.07, 14500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (246, N'Chashma Sugar Mills Ltd. ', N'Chashma Sugar Mills Ltd. ', 89.5, 89.9, 94.49, 86.01, 92.07, 4.5, 66500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (247, N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]', N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]', 2.57, 2.75, 3, 2.75, 2.9, 0.33, 136000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (248, N'Habib Sugar Mills Ltd. ', N'Habib Sugar Mills Ltd. ', 34.58, 34.88, 34.88, 34.5, 34.59, 0.01, 4000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (249, N'Husein Sugar Mills Limited. ', N'Husein Sugar Mills Limited. ', 15.55, 15.5, 15.95, 15.5, 15.95, 0.4, 6000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (250, N'Imperial Sugar Limited. [ DEFAULTER SEGMENT ]', N'Imperial Sugar Limited. [ DEFAULTER SEGMENT ]', 14.51, 15.49, 15.49, 14.12, 14.23, -0.01, 7000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (251, N'J. D. W. Sugar Mills Ltd. ', N'J. D. W. Sugar Mills Ltd. ', 227, 227, 227, 225, 225.01, -2, 600)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (252, N'Jauharabad Sugar Mills Ltd. ', N'Jauharabad Sugar Mills Ltd. ', 15.39, 15.45, 15.55, 15.01, 15.55, 0.16, 9000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (253, N'Khairpur Sugar Mills Ltd. ', N'Khairpur Sugar Mills Ltd. ', 97.6, 100.5, 100.5, 90.28, 90.46, -7.32, 9000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (254, N'Mehran Sugar Mills Ltd. ', N'Mehran Sugar Mills Ltd. ', 60.52, 59, 59, 59, 59, -1.52, 7500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (255, N'Noon Sugar Mills Ltd. ', N'Noon Sugar Mills Ltd. ', 70, 68.1, 68.1, 68, 68, -2, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (256, N'Premier Sugar Mills & Distille ', N'Premier Sugar Mills & Distille ', 611.95, 0, 0, 0, 611.95, 0, 100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (257, N'Sakrand Sugar Mills Ltd. ', N'Sakrand Sugar Mills Ltd. ', 9.45, 9.55, 9.75, 9.15, 9.2, -0.3, 393000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (258, N'Shahmurad Sugar Mills Ltd. ', N'Shahmurad Sugar Mills Ltd. ', 104.9, 104.95, 104.95, 103.14, 103.72, -1.76, 3500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (259, N'Tandliawala Sugar Mills Ltd. ', N'Tandliawala Sugar Mills Ltd. ', 175, 169.99, 187.5, 169.99, 186.25, 12.5, 5500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (260, N'Thal Industries Corporation Ltd. ', N'Thal Industries Corporation Ltd. ', 248.5, 247, 247, 247, 248.5, -1.5, 100)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (261, N'Gatron (Industries) Ltd. ', N'Gatron (Industries) Ltd. ', 630, 625.5, 625.5, 620, 620.07, -9.93, 1250)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (262, N'Pakistan Synthentics Ltd. ', N'Pakistan Synthentics Ltd. ', 17.14, 17, 17.47, 17, 17.47, 0.33, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (263, N'Rupali Polyester Ltd. ', N'Rupali Polyester Ltd. ', 16.4, 16.4, 17.31, 16.4, 17.28, 0.88, 7000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (264, N'Tri-Star Polyester Ltd. ', N'Tri-Star Polyester Ltd. ', 8.55, 8.55, 9.1, 8.5, 8.58, 0.03, 1828000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (265, N'Avanceon Limited. ', N'Avanceon Limited. ', 58.42, 59.74, 59.95, 58.35, 59.65, 1.23, 970000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (266, N'Hum Network Limited. ', N'Hum Network Limited. ', 7.93, 8.1, 8.35, 8.03, 8.1, 0.18, 1130500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (267, N'Media Times Limited. ', N'Media Times Limited. ', 1.25, 1.32, 1.68, 1.32, 1.49, 0.23, 7288500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (268, N'NetSol Technologies Ltd. ', N'NetSol Technologies Ltd. ', 63, 63.4, 64.24, 62.62, 63.91, 1.24, 635000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (269, N'Pakistan Telecommunication Co. ', N'Pakistan Telecommunication Co. ', 9.24, 9.38, 9.38, 9.22, 9.26, 0.02, 1353500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (270, N'Systems Limited. ', N'Systems Limited. ', 190.53, 193, 196.7, 193, 195.86, 5.47, 101900)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (271, N'Telecard Ltd. ', N'Telecard Ltd. ', 1.27, 1.34, 1.39, 1.25, 1.3, 0.03, 580500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (272, N'TPL CORP Limited. ', N'TPL CORP Limited. ', 7.15, 7.2, 7.2, 6.68, 6.74, -0.41, 4629500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (273, N'TPL Trakker Limited. ', N'TPL Trakker Limited. ', 9.92, 9.75, 10.2, 9.56, 10.02, -0.03, 374500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (274, N'TRG Pakistan Ltd. ', N'TRG Pakistan Ltd. ', 50.82, 51.01, 54.63, 51.01, 54.46, 3.81, 29893500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (275, N'Worldcall Telecom Ltd. ', N'Worldcall Telecom Ltd. ', 1.21, 1.18, 1.27, 1.18, 1.18, -0.03, 20166000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (276, N'Artistic Denim Mills Limited. ', N'Artistic Denim Mills Limited. ', 57.58, 0, 0, 0, 57.58, 0, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (277, N'Azgard Nine Limited. ', N'Azgard Nine Limited. ', 17.24, 17.75, 17.75, 17.05, 17.27, 0.03, 2767000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (278, N'Bleesed Textile Ltd. ', N'Bleesed Textile Ltd. ', 290, 290, 290, 290, 290, 0, 300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (279, N'Crescent Textile Mills Ltd. ', N'Crescent Textile Mills Ltd. ', 21.27, 21.25, 21.4, 21.1, 21.28, -0.01, 20000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (280, N'Faisal Spinning Mills Ltd. ', N'Faisal Spinning Mills Ltd. ', 320, 329.99, 329.99, 300, 300, -20, 2200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (281, N'Feroze1888 Mills Ltd. ', N'Feroze1888 Mills Ltd. ', 94, 94, 96.75, 94, 96.75, 2.75, 5000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (282, N'Ghazi Fabrics International Ltd. ', N'Ghazi Fabrics International Ltd. ', 3.25, 0, 0, 0, 3.25, 0, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (283, N'Gul Ahmed Textile Mills Ltd. ', N'Gul Ahmed Textile Mills Ltd. ', 36, 36.49, 36.79, 35.71, 35.9, -0.11, 177500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (284, N'Interloop Limited. ', N'Interloop Limited. ', 50.47, 51, 51.9, 50.5, 51.64, 1.17, 47500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (285, N'International Knitwear Ltd. ', N'International Knitwear Ltd. ', 17.5, 17.5, 17.5, 17.5, 17.5, 0, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (286, N'Jubilee Spinning & Weaving Mil [ DEFAULTER SEGMENT ]', N'Jubilee Spinning & Weaving Mil [ DEFAULTER SEGMENT ]', 3.3, 0, 0, 0, 3.3, 0, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (287, N'Kohinoor Industries Ltd. ', N'Kohinoor Industries Ltd. ', 3.7, 3.71, 3.72, 3.71, 3.72, 0.02, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (288, N'Kohinoor Mills Ltd. ', N'Kohinoor Mills Ltd. ', 36.5, 36.85, 37, 36.8, 37, 0.5, 6500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (289, N'Kohinoor Textile Mills Ltd. ', N'Kohinoor Textile Mills Ltd. ', 50.97, 50.5, 52, 50.5, 51.58, 1.03, 7500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (290, N'Masood Textile Mills Ltd. ', N'Masood Textile Mills Ltd. ', 60.01, 64.5, 64.5, 64.5, 64.5, 4.49, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (291, N'Nishat (Chunia) Ltd. ', N'Nishat (Chunia) Ltd. ', 40.71, 40, 41.1, 40, 40.75, 0.04, 260000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (292, N'Nishat Mills Ltd. ', N'Nishat Mills Ltd. ', 102.94, 103.98, 106.07, 103, 105.06, 2.12, 1111500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (293, N'Redco Textile Ltd. ', N'Redco Textile Ltd. ', 6.95, 7.14, 7.14, 7.14, 7.14, 0.19, 1000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (294, N'Reliance Weaving Mills Ltd. ', N'Reliance Weaving Mills Ltd. ', 25, 25.95, 26, 25, 25.25, 0.25, 14500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (295, N'Sapphire Fibres Mills Ltd. ', N'Sapphire Fibres Mills Ltd. ', 805, 756, 770.2, 756, 770.1, -34.9, 200)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (296, N'Sapphire Textile Mills Ltd. ', N'Sapphire Textile Mills Ltd. ', 870, 899.99, 899.99, 899.99, 899.99, 29.99, 50)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (297, N'Asim Textile Mills Ltd. ', N'Asim Textile Mills Ltd. ', 8.1, 7.63, 8.48, 7.63, 8.48, 0.38, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (298, N'Bilal Fibres Ltd. [ DEFAULTER SEGMENT ]', N'Bilal Fibres Ltd. [ DEFAULTER SEGMENT ]', 1.15, 1.07, 1.07, 1.07, 1.07, -0.08, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (299, N'Chakwal Spinning Mills Limited. ', N'Chakwal Spinning Mills Limited. ', 1.93, 1.93, 1.94, 1.9, 1.92, 0.01, 74500)
GO
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (300, N'Colony Textile Mills Ltd. ', N'Colony Textile Mills Ltd. ', 3.51, 3.7, 3.7, 3.5, 3.57, 0.07, 52500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (301, N'D. S. Industries Ltd. ', N'D. S. Industries Ltd. ', 1.75, 1.8, 1.9, 1.72, 1.84, 0.09, 164500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (302, N'Dewan Farooque Spinning Mills Ltd. ', N'Dewan Farooque Spinning Mills Ltd. ', 1.66, 1.75, 1.77, 1.64, 1.7, 0.04, 63500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (303, N'Din Textile Mills Ltd. ', N'Din Textile Mills Ltd. ', 49, 47, 50, 47, 50, 1, 18000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (304, N'Elahi Cotton Mills Ltd. ', N'Elahi Cotton Mills Ltd. ', 33.68, 36.2, 36.2, 36.2, 36.2, 2.52, 1500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (305, N'Fazal Cloth Mills Ltd. ', N'Fazal Cloth Mills Ltd. ', 139.97, 138, 138, 135.36, 135.37, -4.61, 1300)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (306, N'Hira Textile Mills Ltd. [ DEFAULTER SEGMENT ]', N'Hira Textile Mills Ltd. [ DEFAULTER SEGMENT ]', 1.95, 1.91, 2.02, 1.91, 2, 0.05, 164500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (307, N'Indus Dyeing & Manufacturing. ', N'Indus Dyeing & Manufacturing. ', 540, 526.01, 526.01, 526.01, 526.01, -13.99, 50)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (308, N'Island Textile Mills Ltd. ', N'Island Textile Mills Ltd. ', 991.99, 1065.94, 1065.94, 1065.94, 1065.94, 73.95, 40)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (309, N'J. A. Textile Mills Ltd. ', N'J. A. Textile Mills Ltd. ', 5.2, 5, 5, 5, 5, -0.2, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (310, N'Janana De Malucha Tex Mills. ', N'Janana De Malucha Tex Mills. ', 70, 70.02, 70.02, 70.02, 70.02, 0.02, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (311, N'Khurshid Spinning Mills Ltd. ', N'Khurshid Spinning Mills Ltd. ', 7.55, 7.5, 7.5, 7.5, 7.5, -0.05, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (312, N'Kohinoor Spinning Mills Ltd. ', N'Kohinoor Spinning Mills Ltd. ', 2.41, 2.45, 2.8, 2.45, 2.69, 0.33, 827000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (313, N'Maqbool Textile Mills Ltd. ', N'Maqbool Textile Mills Ltd. ', 32.4, 34.49, 34.49, 34.49, 34.49, 2.09, 2000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (314, N'Nazir Cotton Mills Ltd. [ DEFAULTER SEGMENT ]', N'Nazir Cotton Mills Ltd. [ DEFAULTER SEGMENT ]', 3.42, 3.45, 3.45, 3.26, 3.33, -0.09, 14000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (315, N'Ravi Textile Mills Ltd. [ DEFAULTER SEGMENT ]', N'Ravi Textile Mills Ltd. [ DEFAULTER SEGMENT ]', 3.5, 3.5, 3.55, 3.47, 3.47, -0.03, 14000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (316, N'Ruby Textile Mills Ltd. ', N'Ruby Textile Mills Ltd. ', 6.83, 6.72, 7.25, 6.72, 7, 0.37, 4500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (317, N'Salfi Textile Mills Ltd. ', N'Salfi Textile Mills Ltd. ', 148.35, 159, 159.47, 159, 159.32, 11.1, 1800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (318, N'Shadab Textile Mills Ltd. ', N'Shadab Textile Mills Ltd. ', 29, 29, 29.5, 29, 29.5, 0.5, 11000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (319, N'Ashfaq Textile Mills Ltd. ', N'Ashfaq Textile Mills Ltd. ', 14, 14.02, 14.02, 14.02, 14.02, 0.02, 500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (320, N'Samin Textiles Ltd. ', N'Samin Textiles Ltd. ', 3.7, 3.35, 3.35, 3.35, 3.35, -0.35, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (321, N'Service Fabrics Ltd. ', N'Service Fabrics Ltd. ', 3.4, 3.45, 3.49, 3.32, 3.34, 0, 10000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (322, N'Shahtaj Textile Ltd. ', N'Shahtaj Textile Ltd. ', 120.74, 111.69, 111.7, 111.69, 111.69, -9.05, 4000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (323, N'Yousuf Weaving Mills Limited. ', N'Yousuf Weaving Mills Limited. ', 3.44, 3.5, 3.6, 3.45, 3.48, 0.03, 289000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (324, N'Khyber Tobacco Co. Ltd. ', N'Khyber Tobacco Co. Ltd. ', 210.2, 208.01, 215, 207.01, 215, 4.8, 2800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (325, N'Pakistan Tobacco Co Ltd. ', N'Pakistan Tobacco Co Ltd. ', 1656, 1680, 1680, 1680, 1680, 24, 20)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (326, N'Pak International Airline Corp Ltd ', N'Pak International Airline Corp Ltd ', 4.69, 4.83, 4.88, 4.7, 4.75, 0.06, 505500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (327, N'Pakistan Int.Container Terminal.(XD) ', N'Pakistan Int.Container Terminal.(XD) ', 232.61, 235, 235, 215.17, 215.36, -17.44, 69800)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (328, N'Pakistan Intl. Bulk Terminal Ltd. ', N'Pakistan Intl. Bulk Terminal Ltd. ', 12.26, 12.35, 12.38, 12.08, 12.32, 0.06, 3811000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (329, N'Pakistan National Shipping Co. ', N'Pakistan National Shipping Co. ', 88.99, 89.8, 91, 88.1, 90.48, 1.71, 7500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (330, N'Unity Foods Limited. ', N'Unity Foods Limited. ', 14.37, 14.35, 14.48, 13.9, 14.29, -0.08, 9243500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (331, N'Unity Foods Limited.(R2) ', N'Unity Foods Limited.(R2) ', 4.17, 4.17, 4.2, 3.7, 4.03, -0.16, 16278500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (332, N'Bannu Woollen Mills Limited. ', N'Bannu Woollen Mills Limited. ', 37.8, 36.85, 39, 36.5, 36.5, -1.3, 5000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (333, N'Dolmen City REIT. ', N'Dolmen City REIT. ', 10.99, 10.86, 11, 10.86, 11, 0.01, 799500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (334, N'NIT Pakistan Gateway  ETF. ', N'NIT Pakistan Gateway  ETF. ', 12, 11.16, 12, 11.16, 12, 0, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (335, N'UBL Pakistan Enterprise ETF.(XD) ', N'UBL Pakistan Enterprise ETF.(XD) ', 12.71, 12.63, 12.63, 12.63, 12.63, -0.08, 28500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (336, N'ASTL-AUG', N'ASTL-AUG', 47.14, 47.1, 49.7, 47.06, 49.46, 2.56, 1349000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (337, N'ASTL-SEP', N'ASTL-SEP', 47.14, 47.65, 50.1, 47.65, 49.94, 2.77, 555000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (338, N'ATRL-AUG', N'ATRL-AUG', 167.09, 168, 173.4, 164.65, 172.26, 5.83, 1903500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (339, N'ATRL-SEP', N'ATRL-SEP', 167.09, 168, 174.49, 165.91, 173.37, 6.91, 989500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (340, N'AVN-AUG', N'AVN-AUG', 58.52, 59.7, 59.7, 58.15, 59.62, 1.13, 569000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (341, N'AVN-SEP', N'AVN-SEP', 58.52, 59.25, 60.5, 59, 60.04, 1.28, 124000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (342, N'BAHL-AUG', N'BAHL-AUG', 65.5, 61.1, 66.75, 61.1, 66.08, 1.25, 15000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (343, N'BAFL-AUG', N'BAFL-AUG', 37, 36.45, 36.8, 36.45, 36.54, -0.44, 23000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (344, N'BAFL-SEP', N'BAFL-SEP', 37, 37.3, 37.3, 36.81, 36.91, -0.09, 143000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (345, N'BOP-AUG', N'BOP-AUG', 10.08, 10.1, 10.2, 10.07, 10.19, 0.11, 850500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (346, N'BOP-SEP', N'BOP-SEP', 10.08, 10.2, 10.45, 10.16, 10.35, 0.27, 775500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (347, N'CEPB-SEP', N'CEPB-SEP', 86.98, 89, 92, 87, 87.86, 1.21, 191000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (348, N'CHCC-AUG', N'CHCC-AUG', 132.58, 133.25, 136.39, 131.5, 133.58, 0.92, 953000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (349, N'CHCC-SEP', N'CHCC-SEP', 132.58, 136, 137, 132.65, 134.92, 2.32, 581000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (350, N'DGKC-AUG', N'DGKC-AUG', 106.61, 107, 108.3, 106, 107.91, 1.64, 3289000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (351, N'DGKC-SEP', N'DGKC-SEP', 106.61, 107.5, 111.75, 106.8, 108.8, 2.19, 2311000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (352, N'DOL-AUG', N'DOL-AUG', 29.95, 30.29, 30.29, 29.81, 29.99, -0.02, 91500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (353, N'DOL-SEP', N'DOL-SEP', 29.95, 30.35, 30.35, 30.2, 30.26, 0.33, 113000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (354, N'ENGRO-SEP', N'ENGRO-SEP', 284.58, 286, 290, 285.5, 287.79, 3.42, 96500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (355, N'ENGRO-AUG', N'ENGRO-AUG', 292.58, 292.1, 294, 291, 293.22, 0.67, 110000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (356, N'EFERT-SEP', N'EFERT-SEP', 57.96, 61.49, 61.49, 57.75, 57.98, -0.01, 686500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (357, N'EFERT-AUG', N'EFERT-AUG', 61.96, 61.52, 61.7, 61, 61.36, -0.56, 180000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (358, N'EPCL-AUG', N'EPCL-AUG', 33.59, 34.23, 35, 33.85, 34.51, 1.01, 642000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (359, N'EPCL-SEP', N'EPCL-SEP', 33.59, 34.51, 35, 34.22, 34.78, 1.31, 551500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (360, N'FCCL-AUG', N'FCCL-AUG', 20.03, 19.5, 20.91, 19.5, 20.8, 0.63, 1286500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (361, N'FCCL-SEP', N'FCCL-SEP', 20.03, 20.05, 21.1, 20, 20.96, 0.87, 929000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (362, N'FFBL-AUG', N'FFBL-AUG', 18.38, 18.5, 18.57, 18.4, 18.4, 0.02, 535000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (363, N'FFBL-SEP', N'FFBL-SEP', 18.38, 18.6, 19.54, 18.5, 18.56, 0.12, 580500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (364, N'FFC-SEP', N'FFC-SEP', 106.38, 108, 108.25, 107, 107, 0.62, 84000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (365, N'FFC-AUG', N'FFC-AUG', 109.13, 109.99, 110.8, 109.25, 109.25, 0.12, 110500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (366, N'FFL-AUG', N'FFL-AUG', 12.77, 12.63, 12.89, 12.53, 12.83, 0.08, 1132000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (367, N'FEROZ-SEP', N'FEROZ-SEP', 360.62, 369.65, 387.66, 369.65, 386.37, 27.04, 23000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (368, N'GTYR-AUG', N'GTYR-AUG', 74.74, 73.7, 76, 73.7, 75.98, 1.24, 90500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (369, N'GTYR-SEP', N'GTYR-SEP', 74.74, 75.4, 77.35, 75, 76.72, 2.48, 60000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (370, N'GHNI-AUG', N'GHNI-AUG', 286.45, 291, 299.4, 290.15, 295.05, 8.6, 1191500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (371, N'GHNI-SEP', N'GHNI-SEP', 286.45, 294.99, 301.99, 294, 298.01, 11.55, 434500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (372, N'GHNL-AUG', N'GHNL-AUG', 113.32, 113, 115.2, 113, 113.82, 0.58, 229500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (373, N'GHNL-SEP', N'GHNL-SEP', 113.32, 115.5, 116, 114, 114.96, 0.89, 87500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (374, N'GATM-AUG', N'GATM-AUG', 36.25, 36.7, 36.8, 36, 36.24, -0.05, 20500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (375, N'GATM-SEP', N'GATM-SEP', 36.25, 36.5, 36.5, 36.25, 36.25, 0, 20000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (376, N'HBL-AUG', N'HBL-AUG', 124.65, 124.5, 126, 123.99, 125.64, -0.24, 402500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (377, N'HBL-SEP', N'HBL-SEP', 124.65, 125.01, 127.3, 125.01, 126.74, 1.6, 376000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (378, N'HASCOL-AUG', N'HASCOL-AUG', 15.54, 15.41, 16.19, 15.41, 16.07, 0.53, 8756500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (379, N'HASCOL-SEP', N'HASCOL-SEP', 15.54, 16.48, 16.5, 15.67, 16.23, 0.69, 7537000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (380, N'HUBC-AUG', N'HUBC-AUG', 80.84, 80.5, 81.7, 80.01, 80.54, -0.29, 318000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (381, N'HUBC-SEP', N'HUBC-SEP', 80.84, 81.5, 82.4, 81, 81.08, 0.24, 251000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (382, N'INIL-AUG', N'INIL-AUG', 131.36, 132.5, 134.6, 130.55, 134.14, 3.24, 376500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (383, N'INIL-SEP', N'INIL-SEP', 131.36, 130, 136, 129, 135.63, 4.64, 169000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (384, N'ISL-AUG', N'ISL-AUG', 71.13, 71.5, 72.5, 70.65, 72.26, 1.07, 1438500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (385, N'ISL-SEP', N'ISL-SEP', 71.13, 72, 73.25, 71, 72.93, 1.8, 418000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (386, N'KEL-AUG', N'KEL-AUG', 3.73, 3.7, 3.75, 3.69, 3.7, 0, 305000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (387, N'KEL-SEP', N'KEL-SEP', 3.73, 3.78, 3.8, 3.73, 3.74, 0.06, 238500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (388, N'KOHC-AUG', N'KOHC-AUG', 167.22, 0, 0, 0, 167.25, 0, 2500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (389, N'KOHC-SEP', N'KOHC-SEP', 167.22, 170, 170, 170, 170, 2.78, 5500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (390, N'KAPCO-AUG', N'KAPCO-AUG', 27.72, 27.9, 28.19, 27, 28.02, 0.18, 1916000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (391, N'KAPCO-SEP', N'KAPCO-SEP', 27.72, 28.1, 28.45, 27.85, 28.21, 0.43, 924000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (392, N'LOTCHEM-AUG', N'LOTCHEM-AUG', 12.36, 12.21, 12.55, 12.16, 12.46, 0.19, 228000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (393, N'LOTCHEM-SEP', N'LOTCHEM-SEP', 12.36, 12.4, 12.49, 12.3, 12.49, 0.13, 175500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (394, N'LUCK-AUG', N'LUCK-AUG', 598.26, 601, 616, 591, 610.74, 15.85, 710000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (395, N'LUCK-SEP', N'LUCK-SEP', 598.26, 603, 622, 595.2, 616.35, 20.74, 228500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (396, N'MLCF-AUG', N'MLCF-AUG', 34.2, 34.2, 35.12, 34, 34.9, 0.7, 5549000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (397, N'MLCF-SEP', N'MLCF-SEP', 34.2, 34.6, 35.45, 34.43, 35.25, 1.2, 3587500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (398, N'MCB-AUG', N'MCB-AUG', 169.99, 169.01, 169.01, 168.5, 168.5, -1.49, 4000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (399, N'MEBL-AUG', N'MEBL-AUG', 86.49, 86.99, 87.9, 84.01, 86.04, -0.31, 38500)
GO
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (400, N'MEBL-SEP', N'MEBL-SEP', 78.63, 82, 82, 79.5, 79.64, 1.17, 45500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (401, N'MUGHAL-AUG', N'MUGHAL-AUG', 61.67, 62.5, 66.29, 62.17, 66.2, 4.62, 810000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (402, N'MUGHAL-SEP', N'MUGHAL-SEP', 61.67, 64.49, 66.29, 64.49, 66.29, 4.62, 77500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (403, N'NBP-AUG', N'NBP-AUG', 35.09, 35.25, 36.55, 33.65, 36.25, 1.46, 386500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (404, N'NBP-SEP', N'NBP-SEP', 35.09, 37.48, 37.48, 35.7, 36.74, 1.65, 173500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (405, N'NRL-AUG', N'NRL-AUG', 185.32, 185, 189.5, 183.55, 188.77, 2.69, 445500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (406, N'NRL-SEP', N'NRL-SEP', 185.32, 187.5, 191, 185.99, 190.09, 4.73, 54000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (407, N'NETSOL-AUG', N'NETSOL-AUG', 63.15, 63.98, 64.3, 62.75, 63.91, 0.86, 292000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (408, N'NETSOL-SEP', N'NETSOL-SEP', 63.15, 63.66, 64.7, 63.3, 64.53, 1.41, 53000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (409, N'NCL-AUG', N'NCL-AUG', 40.93, 41, 41.05, 40.5, 41, 0.07, 31000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (410, N'NCL-SEP', N'NCL-SEP', 40.93, 41.4, 41.45, 41.4, 41.44, 0.52, 24500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (411, N'NML-AUG', N'NML-AUG', 103.09, 103.5, 106, 103.4, 105.02, 1.92, 279000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (412, N'NML-SEP', N'NML-SEP', 103.09, 105.3, 107, 105, 106.01, 2.91, 187500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (413, N'OGDC-AUG', N'OGDC-AUG', 115.16, 114.1, 114.5, 112, 113.02, -1.97, 177000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (414, N'OGDC-SEP', N'OGDC-SEP', 115.16, 115, 115, 113, 114.12, -1.16, 246500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (415, N'PAEL-AUG', N'PAEL-AUG', 33.26, 33.12, 33.77, 33.12, 33.72, 0.45, 4289500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (416, N'PAEL-SEP', N'PAEL-SEP', 33.26, 33.51, 35.6, 33.5, 34.05, 0.79, 2929000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (417, N'PSMC-SEP', N'PSMC-SEP', 237.52, 244, 249.9, 244, 246.72, 10.48, 61000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (418, N'PIBTL-AUG', N'PIBTL-AUG', 12.28, 12.22, 12.39, 12.06, 12.34, 0.02, 1499500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (419, N'PIBTL-SEP', N'PIBTL-SEP', 12.28, 12.45, 12.51, 12.21, 12.44, 0.16, 709500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (420, N'POL-AUG', N'POL-AUG', 421.49, 422, 422, 415.5, 415.5, -5.99, 13000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (421, N'POL-SEP', N'POL-SEP', 421.49, 421.5, 424.06, 418, 418.12, -3.49, 10000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (422, N'PPL-AUG', N'PPL-AUG', 100.37, 99, 99.7, 97, 98.71, -1.22, 2559500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (423, N'PPL-SEP', N'PPL-SEP', 100.37, 100.1, 100.5, 97.9, 99.51, -0.39, 2346000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (424, N'PRL-SEP', N'PRL-SEP', 15.63, 15.81, 16.8, 15.81, 16.79, 1.17, 834500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (425, N'PSO-AUG', N'PSO-AUG', 186.79, 186.25, 190.25, 186.01, 189.32, 2.76, 968000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (426, N'PSO-SEP', N'PSO-SEP', 186.79, 188.3, 195, 188.3, 190.96, 4.61, 727500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (427, N'PIOC-AUG', N'PIOC-AUG', 87.63, 87.25, 89, 86, 88.56, 1.17, 1568000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (428, N'PIOC-SEP', N'PIOC-SEP', 87.63, 90, 90, 86.8, 89.36, 2.07, 965000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (429, N'POWER-SEP', N'POWER-SEP', 8.87, 9.05, 9.2, 9.02, 9.15, 0.28, 456000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (430, N'SNGP-AUG', N'SNGP-AUG', 63.38, 63.99, 64.75, 63.3, 64.51, 1.27, 1093000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (431, N'SNGP-SEP', N'SNGP-SEP', 63.38, 64.5, 65.44, 64.01, 65.16, 1.95, 598500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (432, N'SSGC-AUG', N'SSGC-AUG', 17.36, 17.3, 17.65, 17.15, 17.24, -0.17, 968000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (433, N'SSGC-SEP', N'SSGC-SEP', 17.36, 17.7, 17.75, 17.35, 17.48, -0.01, 897500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (434, N'TGL-SEP', N'TGL-SEP', 84.54, 87.5, 90.88, 86.27, 88.96, 6.34, 20500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (435, N'SEARL-AUG', N'SEARL-AUG', 248.93, 250, 261.4, 249.71, 260.04, 11.08, 1198000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (436, N'SEARL-SEP', N'SEARL-SEP', 248.93, 255, 264.99, 252, 262.11, 13.57, 772000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (437, N'TRG-AUG', N'TRG-AUG', 50.93, 51.15, 54.74, 51.06, 54.53, 3.6, 29156000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (438, N'TRG-SEP', N'TRG-SEP', 50.93, 51.51, 54.74, 51.5, 54.74, 3.81, 24072000)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (439, N'UBL-AUG', N'UBL-AUG', 118.62, 118, 118.6, 118, 118.42, -0.37, 39500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (440, N'UBL-SEP', N'UBL-SEP', 118.62, 119.14, 119.6, 119, 119.33, 0.5, 47500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (441, N'UNITY-AUG', N'UNITY-AUG', 14.45, 14.48, 14.5, 13.9, 14.3, -0.1, 3953500)
INSERT [dbo].[MarketSummary] ([MS_ID], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (442, N'UNITY-SEP', N'UNITY-SEP', 14.45, 13.76, 14.6, 13.76, 14.37, -0.02, 4675500)
SET IDENTITY_INSERT [dbo].[MarketSummary] OFF
GO
SET IDENTITY_INSERT [dbo].[MarketSummaryHistory] ON 

INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (1, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Al-Ghazi Tractors Limited. ', N'Al-Ghazi Tractors Limited. ', 402.31, 397.55, 407.5, 397.5, 407.5, 5.19, 3400)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (2, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Atlas Honda Limited. ', N'Atlas Honda Limited. ', 440, 438, 439.9, 397.5, 439.9, -0.1, 300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (3, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dewan Farooque Motors Limited [ DEFAULTER SEGMENT ]', N'Dewan Farooque Motors Limited [ DEFAULTER SEGMENT ]', 5.99, 6.14, 6.14, 397.5, 6.04, 0.02, 324000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (4, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghandhara Industries Ltd. ', N'Ghandhara Industries Ltd. ', 286.28, 290, 298.1, 397.5, 294.58, 8.3, 1814400)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (5, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghandhara Nissan Ltd. ', N'Ghandhara Nissan Ltd. ', 112.74, 112.95, 115, 397.5, 113.59, 0.76, 487000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (6, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghani Automobile Industries Ltd. ', N'Ghani Automobile Industries Ltd. ', 6.35, 6.49, 6.72, 397.5, 6.5, 0.2, 720500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (7, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hino Pak Motor Limited. ', N'Hino Pak Motor Limited. ', 532, 535, 554.99, 397.5, 550.71, 22.99, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (8, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Honda Atlas Cars (Pak) Ltd. ', N'Honda Atlas Cars (Pak) Ltd. ', 330.93, 334, 336.6, 397.5, 333.34, 2.41, 280200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (9, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Indus Motor Company Ltd. ', N'Indus Motor Company Ltd. ', 1280.22, 1284, 1284, 397.5, 1265.62, -15.22, 64650)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (10, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Millat Tractors Limited. ', N'Millat Tractors Limited. ', 888.95, 890, 890, 397.5, 880.67, -8.85, 5200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (11, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pak Suzuki Motors Co Ltd. ', N'Pak Suzuki Motors Co Ltd. ', 237.52, 237, 248, 397.5, 244.72, 8.48, 763300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (12, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sazgar Engineering Works Ltd. ', N'Sazgar Engineering Works Ltd. ', 169.9, 170.8, 174.99, 397.5, 173.31, 3.41, 366800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (13, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Agriautos Industries Co. Ltd. ', N'Agriautos Industries Co. Ltd. ', 207, 208.25, 208.85, 397.5, 208.32, 1, 2100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (14, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Atlas Battery Ltd. ', N'Atlas Battery Ltd. ', 220.42, 224.99, 225.8, 397.5, 224.18, 3.58, 32100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (15, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Exide Pakistan Ltd. ', N'Exide Pakistan Ltd. ', 350.03, 346, 358, 397.5, 357.62, 7.77, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (16, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'General Tyre & Rubber Co. ', N'General Tyre & Rubber Co. ', 74.52, 74.09, 76, 397.5, 75.86, 1.13, 243000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (17, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Loads Limited. ', N'Loads Limited. ', 18.65, 18.51, 19.3, 397.5, 18.88, 0.23, 264500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (18, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Thal Limited. ', N'Thal Limited. ', 390, 383, 392.5, 397.5, 390.15, 0, 24700)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (19, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Johnson & Philips (Pak) Ltd. [ DEFAULTER SEGMENT ]', N'Johnson & Philips (Pak) Ltd. [ DEFAULTER SEGMENT ]', 52.5, 52.55, 52.55, 397.5, 50.9, -1.6, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (20, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pak Elektron Ltd. ', N'Pak Elektron Ltd. ', 33.23, 33.28, 33.85, 397.5, 33.71, 0.48, 4197000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (21, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Cables Ltd. ', N'Pakistan Cables Ltd. ', 137.99, 139, 140, 397.5, 137.09, -0.99, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (22, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Siemens (Pak) Eng. Co. Ltd. ', N'Siemens (Pak) Eng. Co. Ltd. ', 570, 551, 560, 397.5, 560, -10, 1750)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (23, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'WAVES Singer Pakistan Ltd. ', N'WAVES Singer Pakistan Ltd. ', 26.66, 26.4, 27.05, 397.5, 26.95, 0.29, 1126000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (24, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Attock Cement Pak Ltd. ', N'Attock Cement Pak Ltd. ', 142.99, 144.9, 148, 397.5, 146.61, 4.01, 90500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (25, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bestway Cement Limited. ', N'Bestway Cement Limited. ', 149.38, 149.38, 149.99, 397.5, 147.38, -1.48, 6500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (26, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Cherat Cement Co. Ltd. ', N'Cherat Cement Co. Ltd. ', 132.24, 133.1, 136.26, 397.5, 133.32, 1.08, 2251500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (27, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'D. G. Khan Cement Co. Ltd. ', N'D. G. Khan Cement Co. Ltd. ', 106.55, 106.61, 108.44, 397.5, 107.97, 1.75, 4405000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (28, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dandot Cement Co. Ltd. [ DEFAULTER SEGMENT ]', N'Dandot Cement Co. Ltd. [ DEFAULTER SEGMENT ]', 8.4, 8, 8, 397.5, 8, -0.4, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (29, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dewan Cement Limited. ', N'Dewan Cement Limited. ', 10.02, 10.5, 11.02, 397.5, 11.02, 1, 23459500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (30, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fauji Cement Co Ltd. ', N'Fauji Cement Co Ltd. ', 19.99, 20.01, 20.9, 397.5, 20.76, 0.77, 5388000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (31, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fecto Cement Ltd. ', N'Fecto Cement Ltd. ', 29.96, 29.9, 30.5, 397.5, 30.04, -0.06, 48000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (32, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Flying Cement Company Ltd. ', N'Flying Cement Company Ltd. ', 10.43, 10.25, 10.69, 397.5, 10.51, 0.09, 351500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (33, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Gharibwal Cement Ltd. ', N'Gharibwal Cement Ltd. ', 24.7, 24.7, 25, 397.5, 24.82, 0.05, 128000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (34, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Javedan Corporation Ltd. ', N'Javedan Corporation Ltd. ', 25.5, 25.5, 25.7, 397.5, 25.7, 0.2, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (35, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohat Cement Co. Ltd. ', N'Kohat Cement Co. Ltd. ', 167.43, 168.85, 169.44, 397.5, 168.33, -0.43, 67300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (36, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Lucky Cement Limited. ', N'Lucky Cement Limited. ', 597.86, 597, 619, 397.5, 611.1, 16.15, 1816754)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (37, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Maple Leaf Cement Factory Ltd. ', N'Maple Leaf Cement Factory Ltd. ', 34.18, 34.45, 35.1, 397.5, 34.91, 0.73, 8843000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (38, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pioneer Cement Ltd. ', N'Pioneer Cement Ltd. ', 87.47, 87.47, 89, 397.5, 88.34, 1.23, 3116500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (39, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Power cement Limited ', N'Power cement Limited ', 8.87, 9.05, 9.25, 397.5, 9.03, 0.16, 8269000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (40, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Power cement Limited(R1) ', N'Power cement Limited(R1) ', 0.54, 0.52, 0.61, 397.5, 0.52, -0.02, 3676000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (41, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Safe Mix Concrete Ltd. ', N'Safe Mix Concrete Ltd. ', 8.6, 8.97, 9, 397.5, 8.63, 0.02, 44500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (42, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Thatta Cement Company Ltd. ', N'Thatta Cement Company Ltd. ', 10.53, 10.89, 10.89, 397.5, 10.59, 0.07, 84000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (43, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Agritech Limited ', N'Agritech Limited ', 5.3, 5.43, 5.49, 397.5, 5.36, 0.04, 413000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (44, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Archroma Pakistan Limited. ', N'Archroma Pakistan Limited. ', 562, 556, 562, 397.5, 540.93, -23, 18150)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (45, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bawany Air Products Ltd. [ DEFAULTER SEGMENT ]', N'Bawany Air Products Ltd. [ DEFAULTER SEGMENT ]', 18.22, 19.22, 19.22, 397.5, 18.91, 0.69, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (46, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Berger Paints Pakistan Ltd. ', N'Berger Paints Pakistan Ltd. ', 79.39, 81.49, 84, 397.5, 82.17, 2.61, 46500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (47, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bifo Industries Ltd. ', N'Bifo Industries Ltd. ', 160, 163, 166.99, 397.5, 163.17, 3, 16000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (48, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Buxly Paints Ltd. ', N'Buxly Paints Ltd. ', 37.5, 39.97, 39.97, 397.5, 39.97, 2.47, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (49, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Colgate Palmolive (Pak) Ltd. ', N'Colgate Palmolive (Pak) Ltd. ', 2399.99, 2399.99, 2399.99, 397.5, 2399.99, 0, 140)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (50, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Descon Oxychem Ltd. ', N'Descon Oxychem Ltd. ', 29.85, 30, 30.28, 397.5, 29.93, 0.2, 295500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (51, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'DYNEA Pak. ', N'DYNEA Pak. ', 144.63, 147.49, 147.9, 397.5, 146.88, 1.37, 31500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (52, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Engro Polymer & Chemicals Ltd. ', N'Engro Polymer & Chemicals Ltd. ', 33.61, 33.91, 35.49, 397.5, 34.52, 1.38, 7873000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (53, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghani Global Holdings Limited. ', N'Ghani Global Holdings Limited. ', 16, 15.99, 17.2, 397.5, 16.63, 0.7, 3086000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (54, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ICI Pakistan Limited. ', N'ICI Pakistan Limited. ', 763.26, 760, 784.95, 397.5, 772.41, 16.74, 55350)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (55, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ittehad Chemical Ltd. ', N'Ittehad Chemical Ltd. ', 28.5, 28.22, 28.49, 397.5, 28.01, -0.49, 68000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (56, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Lotte Chemical Pakistan Ltd. ', N'Lotte Chemical Pakistan Ltd. ', 12.43, 12.5, 12.55, 397.5, 12.47, 0.07, 2602000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (57, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nimir Resins Limited. ', N'Nimir Resins Limited. ', 7.29, 7.21, 7.51, 397.5, 7.44, 0.22, 645000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (58, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Oxygen Limited. ', N'Pakistan Oxygen Limited. ', 157.28, 159, 159.9, 397.5, 158.12, 1.11, 24900)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (59, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan PVC [ DEFAULTER SEGMENT ]', N'Pakistan PVC [ DEFAULTER SEGMENT ]', 3.35, 3.08, 3.3, 397.5, 3.25, -0.05, 4500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (60, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sardar Chemical IndustriesLtd. ', N'Sardar Chemical IndustriesLtd. ', 18, 17.01, 18.85, 397.5, 18.29, 0.85, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (61, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sitara Chemicals. ', N'Sitara Chemicals. ', 320, 318.95, 318.95, 397.5, 316.05, -6.85, 800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (62, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sitara Peroxide Limited ', N'Sitara Peroxide Limited ', 23.35, 23.1, 23.18, 397.5, 22.81, -0.55, 667500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (63, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HBL Growth Fund ', N'HBL Growth Fund ', 11.5, 11.55, 11.55, 397.5, 11.55, 0.05, 8000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (64, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HBL Investment Fund ', N'HBL Investment Fund ', 4.1, 4, 4, 397.5, 4, -0.1, 3500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (65, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Allied Bank Ltd. ', N'Allied Bank Ltd. ', 87.61, 91.91, 94.1, 397.5, 87.88, -0.41, 17500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (66, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Askari Bank Limited. ', N'Askari Bank Limited. ', 19.05, 19.45, 19.45, 397.5, 19.4, 0.35, 146500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (67, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bank Al-Habib Ltd. ', N'Bank Al-Habib Ltd. ', 65.52, 65.52, 66.39, 397.5, 65.29, -0.24, 153357)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (68, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bank Alfalah Ltd. ', N'Bank Alfalah Ltd. ', 36.95, 36.69, 37.2, 397.5, 36.65, -0.3, 762587)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (69, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bank Of Punjab. ', N'Bank Of Punjab. ', 10.05, 10.15, 10.19, 397.5, 10.16, 0.12, 1990500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (70, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bankislami Pakistan Ltd. ', N'Bankislami Pakistan Ltd. ', 7.84, 7.9, 7.9, 397.5, 7.86, 0.01, 214500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (71, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Faysal Bank Limited. ', N'Faysal Bank Limited. ', 17.51, 17.59, 18, 397.5, 17.37, -0.21, 545000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (72, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Habib Bank Limited. ', N'Habib Bank Limited. ', 124.27, 124, 126.15, 397.5, 125.1, 0.83, 437781)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (73, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Habib Metropolitn Bank Limited. ', N'Habib Metropolitn Bank Limited. ', 34.88, 34.9, 34.9, 397.5, 34.3, -0.58, 10000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (74, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'JS Bank Limited. ', N'JS Bank Limited. ', 5.52, 5.51, 5.74, 397.5, 5.68, 0.16, 1186500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (75, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MCB Bank Limited. ', N'MCB Bank Limited. ', 169.6, 170, 171, 397.5, 168.51, -1.09, 164035)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (76, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Meezan Bank Limited. ', N'Meezan Bank Limited. ', 85.95, 86, 88, 397.5, 85.92, 0.05, 1510500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (77, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'National Bank Of Pakistan. ', N'National Bank Of Pakistan. ', 35.14, 35.48, 37.24, 397.5, 36.53, 1.56, 9059500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (78, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Samba Bank Limited. ', N'Samba Bank Limited. ', 7.5, 7.75, 8, 397.5, 8, 0.5, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (79, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Silk Bank Limited. ', N'Silk Bank Limited. ', 0.82, 0.83, 0.87, 397.5, 0.83, 0, 269500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (80, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Soneri Bank Ltd. ', N'Soneri Bank Ltd. ', 9.34, 9.49, 9.49, 397.5, 9.24, -0.13, 123000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (81, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Standard Chartered Bank Pak Ltd. ', N'Standard Chartered Bank Pak Ltd. ', 32.8, 32, 32, 397.5, 31.37, -1.48, 146500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (82, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Summit Bank Limited. [ DEFAULTER SEGMENT ]', N'Summit Bank Limited. [ DEFAULTER SEGMENT ]', 1.71, 1.74, 1.78, 397.5, 1.68, -0.03, 407500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (83, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'United Bank Ltd. ', N'United Bank Ltd. ', 118.6, 118.98, 118.98, 397.5, 118.2, -0.4, 283621)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (84, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ados Pakistan Limited. ', N'Ados Pakistan Limited. ', 25.25, 25.25, 25.25, 397.5, 25.25, 0, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (85, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Aisha Steel Mills Limited. ', N'Aisha Steel Mills Limited. ', 13.92, 14.05, 14.25, 397.5, 14.15, 0.28, 2848500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (86, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Amreli Steels Limited. ', N'Amreli Steels Limited. ', 47.05, 47.35, 49.75, 397.5, 49.43, 2.38, 3423000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (87, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bolan Casting Ltd. ', N'Bolan Casting Ltd. ', 74.94, 75, 78, 397.5, 75.27, 0.06, 17500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (88, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Crescent Steel & Allied Product. ', N'Crescent Steel & Allied Product. ', 69.4, 69.5, 69.5, 397.5, 68.14, -1.39, 345500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (89, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dost Steels Ltd. [ DEFAULTER SEGMENT ]', N'Dost Steels Ltd. [ DEFAULTER SEGMENT ]', 4.17, 4.21, 4.34, 397.5, 4.12, -0.04, 2198500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (90, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Drekkar Kingsway Ltd. [ DEFAULTER SEGMENT ]', N'Drekkar Kingsway Ltd. [ DEFAULTER SEGMENT ]', 3.4, 3.4, 3.55, 397.5, 3.55, 0.15, 45000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (91, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'International Industries Ltd. ', N'International Industries Ltd. ', 131.04, 132.5, 134.97, 397.5, 134.11, 3.93, 931500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (92, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'International Steels Limited. ', N'International Steels Limited. ', 71.03, 71.75, 72.5, 397.5, 72.18, 1.15, 2402000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (93, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ittefaq Iron Industries Limited. ', N'Ittefaq Iron Industries Limited. ', 10.31, 10.45, 10.74, 397.5, 10.65, 0.29, 458000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (94, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KSB Pumps Co Ltd. ', N'KSB Pumps Co Ltd. ', 162.55, 154, 164.9, 397.5, 159.63, -2.92, 11500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (95, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Metropolitan Steel Corporation ', N'Metropolitan Steel Corporation ', 9.32, 10.32, 10.32, 397.5, 9.85, 0.53, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (96, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Mughal Iron & Steels Ind Ltd. ', N'Mughal Iron & Steels Ind Ltd. ', 61.55, 61.89, 66.16, 397.5, 66.16, 4.61, 3931500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (97, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Arif Habib Corporation Ltd. ', N'Arif Habib Corporation Ltd. ', 37.66, 38, 38, 397.5, 38, 0.34, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (98, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Engro Corporation Limited. ', N'Engro Corporation Limited. ', 292.57, 292.97, 293.78, 397.5, 292.79, 0.43, 295272)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (99, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Engro Fertilizers Limited. ', N'Engro Fertilizers Limited. ', 61.77, 61.81, 62, 397.5, 61.39, -0.37, 4104860)
GO
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (100, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fatima Fertilizer Co Ltd. ', N'Fatima Fertilizer Co Ltd. ', 26.25, 26.25, 26.25, 397.5, 26.2, -0.05, 8000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (101, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fauji Fertilizer Bin Qasim Ltd. ', N'Fauji Fertilizer Bin Qasim Ltd. ', 18.34, 18.32, 18.52, 397.5, 18.38, 0.04, 958500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (102, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fauji Fertilizer Co. Ltd. ', N'Fauji Fertilizer Co. Ltd. ', 109.3, 109, 110.89, 397.5, 108.33, -0.97, 1826773)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (103, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Al-Shaheer Corporation. ', N'Al-Shaheer Corporation. ', 12.79, 12.9, 13.03, 397.5, 12.9, 0.24, 208000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (104, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'At-Tahur Ltd. ', N'At-Tahur Ltd. ', 18.55, 18.5, 18.89, 397.5, 18.84, 0.33, 44000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (105, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Clover Pakistan Limited. ', N'Clover Pakistan Limited. ', 92.89, 94, 99.85, 397.5, 99.32, 6.96, 138500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (106, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fauji Foods Limited. ', N'Fauji Foods Limited. ', 12.73, 12.73, 12.88, 397.5, 12.83, 0.1, 1718000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (107, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Frieslandcampina Engro Pakistan Ltd. ', N'Frieslandcampina Engro Pakistan Ltd. ', 86.07, 89.49, 91.5, 397.5, 87.65, 1.53, 1433500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (108, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ismail Industries Ltd. ', N'Ismail Industries Ltd. ', 345.02, 320, 320, 397.5, 320, -25.02, 100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (109, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Matco Foods Limited. ', N'Matco Foods Limited. ', 22.5, 22.21, 22.29, 397.5, 22.18, -0.39, 103000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (110, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Murree Brewery Company Ltd. ', N'Murree Brewery Company Ltd. ', 563.5, 555, 570, 397.5, 563.1, -0.4, 200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (111, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'National Foods Ltd. ', N'National Foods Ltd. ', 234.8, 236.5, 236.5, 397.5, 233.93, -4.77, 14700)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (112, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nestle Pakistan Ltd.(XD) ', N'Nestle Pakistan Ltd.(XD) ', 6150.01, 6050, 6289.99, 397.5, 6100, -50.01, 120)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (113, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Quice Food Industries Ltd. ', N'Quice Food Industries Ltd. ', 4.85, 4.85, 4.87, 397.5, 4.79, -0.07, 170500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (114, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Rafhan Maize Products Ltd. ', N'Rafhan Maize Products Ltd. ', 8025, 8000, 8000, 397.5, 8000, -25, 40)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (115, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shezan International Ltd. ', N'Shezan International Ltd. ', 218.5, 220.99, 222, 397.5, 220, 1.5, 2400)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (116, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shield Corporation Ltd. ', N'Shield Corporation Ltd. ', 220.02, 212.01, 212.01, 397.5, 212.01, -8.01, 200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (117, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'The Organic Meat Company Ltd. ', N'The Organic Meat Company Ltd. ', 21.37, 21.26, 21.4, 397.5, 21.29, 0.01, 140000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (118, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Treet Corporation Ltd. ', N'Treet Corporation Ltd. ', 23.26, 23.2, 23.95, 397.5, 23.59, 0.29, 1485000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (119, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Baluchistan Glass Ltd. ', N'Baluchistan Glass Ltd. ', 6.37, 6.34, 6.5, 397.5, 6.34, 0.03, 184500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (120, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Frontier Ceramics Ltd. ', N'Frontier Ceramics Ltd. ', 14.5, 13.42, 14.46, 397.5, 14.35, -0.15, 12500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (121, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghani Glass Ltd. ', N'Ghani Glass Ltd. ', 49.57, 49.57, 49.57, 397.5, 49.36, -0.13, 20000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (122, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghani Global Glass Limited. ', N'Ghani Global Glass Limited. ', 14.96, 14.96, 15.7, 397.5, 15.23, 0.27, 1013000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (123, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghani Value Glass Limited. ', N'Ghani Value Glass Limited. ', 42.75, 40.93, 41.77, 397.5, 41.77, -0.98, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (124, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shabbir Tiles and Ceramics Limited. ', N'Shabbir Tiles and Ceramics Limited. ', 10.76, 10.78, 10.89, 397.5, 10.65, -0.05, 356500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (125, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Tariq Glass. ', N'Tariq Glass. ', 84.54, 84.5, 90.88, 397.5, 89.18, 6.34, 3685000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (126, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Adamjee Insurance Co. Ltd. ', N'Adamjee Insurance Co. Ltd. ', 40, 40.69, 42.8, 397.5, 42.15, 2, 261500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (127, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Askari Gen Insurance Co. ', N'Askari Gen Insurance Co. ', 22.01, 22.05, 22.05, 397.5, 22, -0.01, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (128, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Askari Life Assurance Company Ltd. ', N'Askari Life Assurance Company Ltd. ', 8.45, 8.44, 8.44, 397.5, 8.44, -0.01, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (129, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Century Insurance Co.Ltd. ', N'Century Insurance Co.Ltd. ', 23, 22.5, 22.5, 397.5, 21.81, -1, 8500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (130, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Cresent Star Insurance Ltd. ', N'Cresent Star Insurance Ltd. ', 2.31, 2.27, 2.39, 397.5, 2.35, 0.08, 198500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (131, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'E. F. U. Gen Insurance Ltd. ', N'E. F. U. Gen Insurance Ltd. ', 110, 113, 113, 397.5, 108, -2, 23600)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (132, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EFU Life Assurance Ltd. ', N'EFU Life Assurance Ltd. ', 194.1, 0, 0, 397.5, 194.1, 0, 700)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (133, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Habib Insurance Co Ltd. ', N'Habib Insurance Co Ltd. ', 9.4, 8.79, 9.81, 397.5, 9.4, 0, 85000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (134, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'IGI Holdings Limited. ', N'IGI Holdings Limited. ', 203.45, 204, 207, 397.5, 204.34, 3.55, 61200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (135, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Jubilee General Insurance Co.Ltd. ', N'Jubilee General Insurance Co.Ltd. ', 43.5, 43.21, 43.21, 397.5, 43.21, -0.29, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (136, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Jubliee Life Insurance Co Ltd. ', N'Jubliee Life Insurance Co Ltd. ', 360, 350.05, 360, 397.5, 353.06, -6.94, 6800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (137, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan General Insurance Co. [ DEFAULTER SEGMENT ]', N'Pakistan General Insurance Co. [ DEFAULTER SEGMENT ]', 2, 1.86, 2, 397.5, 2, 0, 8000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (138, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Reinsurance Comp. ', N'Pakistan Reinsurance Comp. ', 24.93, 24.79, 26.25, 397.5, 25.82, 1.07, 385000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (139, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PICIC Insurance Ltd. [ DEFAULTER SEGMENT ]', N'PICIC Insurance Ltd. [ DEFAULTER SEGMENT ]', 0.85, 0.84, 0.84, 397.5, 0.8, -0.05, 28000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (140, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Premier Insurance Limited. ', N'Premier Insurance Limited. ', 5.5, 5.5, 5.5, 397.5, 5.5, 0, 214500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (141, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Reliance Insurance. ', N'Reliance Insurance. ', 6.55, 6.35, 6.35, 397.5, 6.25, -0.3, 4000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (142, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shaheen Insurance Co Ltd. ', N'Shaheen Insurance Co Ltd. ', 3.87, 4, 4, 397.5, 4, 0.13, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (143, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TPL Insurance Ltd. ', N'TPL Insurance Ltd. ', 23.5, 24.45, 24.45, 397.5, 24.45, 0.95, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (144, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'United Insurance Company. ', N'United Insurance Company. ', 7.52, 7.9, 7.9, 397.5, 7.6, 0.08, 5000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (145, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'786 Investments Limited ', N'786 Investments Limited ', 26.57, 24.58, 24.58, 397.5, 24.58, -1.99, 88500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (146, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Arif Habib Limited. ', N'Arif Habib Limited. ', 49.11, 49.5, 49.5, 397.5, 48.94, -0.11, 7500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (147, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Cyan Limited. ', N'Cyan Limited. ', 30.67, 31.6, 31.68, 397.5, 31.66, 1.01, 11000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (148, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dawood Equities Ltd. ', N'Dawood Equities Ltd. ', 4.08, 4.05, 4.05, 397.5, 4.05, -0.03, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (149, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dawood Hercules Corporation Ltd. ', N'Dawood Hercules Corporation Ltd. ', 127.61, 128, 134.35, 397.5, 131.49, 3.88, 127000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (150, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EFG Hermes Pakistan Ltd. ', N'EFG Hermes Pakistan Ltd. ', 22.94, 22.21, 22.21, 397.5, 22.01, -0.93, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (151, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Escorts Investment Bank Ltd. ', N'Escorts Investment Bank Ltd. ', 9.33, 9.4, 9.7, 397.5, 9.4, 0.07, 60000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (152, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Capital Sec.Corp. Ltd. ', N'First Capital Sec.Corp. Ltd. ', 1.06, 1.05, 1.37, 397.5, 1.21, 0.14, 8191000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (153, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Credit & Investment Bank Ltd. ', N'First Credit & Investment Bank Ltd. ', 6.66, 7.05, 7.24, 397.5, 6.98, 0.39, 54000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (154, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Dawood Investment Bank Ltd. ', N'First Dawood Investment Bank Ltd. ', 1.59, 1.63, 1.86, 397.5, 1.84, 0.24, 827500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (155, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First National Equities Limited. ', N'First National Equities Limited. ', 12.95, 13.1, 13.18, 397.5, 13.06, 0.23, 132000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (156, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Invest Capital Investment Bank Ltd. ', N'Invest Capital Investment Bank Ltd. ', 0.77, 0.85, 0.85, 397.5, 0.82, 0.05, 213500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (157, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Jahangir Siddiqui & Company Ltd. ', N'Jahangir Siddiqui & Company Ltd. ', 16.08, 16.05, 16.5, 397.5, 16.3, 0.26, 2123000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (158, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'JS Global Capital Limited. ', N'JS Global Capital Limited. ', 63, 61, 61, 397.5, 61, -2, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (159, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'JS Investments Limited. ', N'JS Investments Limited. ', 18.08, 17.39, 17.39, 397.5, 17.25, -0.83, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (160, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MCB-Arif Habib Savings & Invest Ltd. ', N'MCB-Arif Habib Savings & Invest Ltd. ', 30.98, 32, 32.8, 397.5, 32.8, 1.82, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (161, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Next Capital Limited. ', N'Next Capital Limited. ', 9.89, 9.03, 9.35, 397.5, 9.02, -0.89, 19000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (162, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Stock Exchange Limited. ', N'Pakistan Stock Exchange Limited. ', 13.21, 13.4, 13.4, 397.5, 13.27, 0.04, 1215500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (163, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pervez Ahmed Consultancy Services Ltd. ', N'Pervez Ahmed Consultancy Services Ltd. ', 0.89, 1.03, 1.03, 397.5, 0.95, 0.06, 407500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (164, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Grays Leasing Ltd. ', N'Grays Leasing Ltd. ', 6.81, 7.8, 7.8, 397.5, 6.23, -0.66, 64500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (165, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Orix Leasing Pakistan Ltd. ', N'Orix Leasing Pakistan Ltd. ', 27.85, 27.55, 27.55, 397.5, 27.55, -0.3, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (166, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Saudi Pak Leasing Co. [ DEFAULTER SEGMENT ]', N'Saudi Pak Leasing Co. [ DEFAULTER SEGMENT ]', 0.68, 0.62, 0.7, 397.5, 0.62, -0.06, 52000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (167, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SME Leasing Ltd. ', N'SME Leasing Ltd. ', 2.1, 2.75, 2.75, 397.5, 2.75, 0.65, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (168, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bata Pakistan Ltd. ', N'Bata Pakistan Ltd. ', 1589.99, 1590, 1615, 397.5, 1610, 20.01, 1460)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (169, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Leather Up Ltd. ', N'Leather Up Ltd. ', 13.41, 13.79, 13.99, 397.5, 13.99, 0.58, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (170, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Service Industries. ', N'Service Industries. ', 710.21, 711, 714, 397.5, 709.99, -0.22, 13250)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (171, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Arpak International Investment. ', N'Arpak International Investment. ', 120, 120, 120, 397.5, 120, 0, 7000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (172, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ECOPAK Limited. ', N'ECOPAK Limited. ', 19.56, 20, 21.02, 397.5, 21.01, 1.46, 600000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (173, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Gammon Pak. [ DEFAULTER SEGMENT ]', N'Gammon Pak. [ DEFAULTER SEGMENT ]', 14.96, 15, 15, 397.5, 15, 0.04, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (174, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MACPAC Films Limited. ', N'MACPAC Films Limited. ', 21.56, 21.56, 22.49, 397.5, 21.26, 0.44, 63500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (175, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Olympia Mills Limited. [ DEFAULTER SEGMENT ]', N'Olympia Mills Limited. [ DEFAULTER SEGMENT ]', 10.65, 10.65, 10.7, 397.5, 10.7, 0.05, 8000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (176, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pace (Pakistan) Ltd. ', N'Pace (Pakistan) Ltd. ', 3.2, 3.29, 3.88, 397.5, 3.53, 0.3, 23457000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (177, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Hotels Developers Ltd. ', N'Pakistan Hotels Developers Ltd. ', 96, 97.98, 97.98, 397.5, 96, 0, 32000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (178, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shifa Int. Hospital Ltd. ', N'Shifa Int. Hospital Ltd. ', 237.13, 241, 241, 397.5, 234.7, -2.07, 30700)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (179, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Siddiqsons Tin Plate Ltd. ', N'Siddiqsons Tin Plate Ltd. ', 11.79, 11.79, 12.33, 397.5, 12.21, 0.42, 2862500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (180, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Synthetic Products Enterprises Ltd. ', N'Synthetic Products Enterprises Ltd. ', 42.41, 43, 43.25, 397.5, 42.25, -0.16, 40500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (181, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TPL Properties Limited. ', N'TPL Properties Limited. ', 10, 10.3, 10.35, 397.5, 9.44, -0.56, 2416000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (182, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Tri-Pack Films Ltd. ', N'Tri-Pack Films Ltd. ', 134.94, 134, 134.8, 397.5, 132.05, -2.44, 227500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (183, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'United Brands Limited. ', N'United Brands Limited. ', 27.5, 27.13, 27.5, 397.5, 27.5, 0, 3500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (184, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Allied Rental Modaraba. ', N'Allied Rental Modaraba. ', 10.8, 11.1, 11.1, 397.5, 11.1, 0.3, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (185, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Elite Capital Mod. ', N'First Elite Capital Mod. ', 3.05, 2.95, 2.97, 397.5, 2.85, -0.2, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (186, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Habib Mod. ', N'First Habib Mod. ', 10.95, 10.9, 10.95, 397.5, 10.95, 0, 49500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (187, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Pak Mod. ', N'First Pak Mod. ', 1.59, 1.58, 1.59, 397.5, 1.59, 0, 43000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (188, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First Prudential Mod. ', N'First Prudential Mod. ', 1.02, 1, 1.03, 397.5, 0.95, -0.07, 10500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (189, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'First UDL Mod. ', N'First UDL Mod. ', 7.5, 7.6, 7.6, 397.5, 7.6, 0.1, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (190, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Habib Metro Modaraba. ', N'Habib Metro Modaraba. ', 10, 9.06, 9.75, 397.5, 9.75, -0.25, 3500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (191, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KASB Modaraba. ', N'KASB Modaraba. ', 1, 1, 1, 397.5, 1, 0, 5000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (192, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Modarba Al-Mali. ', N'Modarba Al-Mali. ', 2.7, 2.25, 2.75, 397.5, 2.75, 0.05, 14000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (193, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Orix Modaraba. ', N'Orix Modaraba. ', 18.5, 18.21, 18.21, 397.5, 18.18, -0.4, 3000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (194, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sindh Modaraba. ', N'Sindh Modaraba. ', 10.99, 10.2, 10.85, 397.5, 10.5, -0.49, 75500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (195, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Mari Petroleum Company Ltd. ', N'Mari Petroleum Company Ltd. ', 1342.66, 1343.07, 1343.07, 397.5, 1330.88, -7.71, 11380)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (196, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Oil & Gas Development Company Ltd. ', N'Oil & Gas Development Company Ltd. ', 114.92, 114.5, 114.5, 397.5, 113.09, -1.57, 1379455)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (197, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Oilfields Limited. ', N'Pakistan Oilfields Limited. ', 421.86, 422, 424.5, 397.5, 415.21, -7.86, 461002)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (198, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Petroleum Limited. ', N'Pakistan Petroleum Limited. ', 100.26, 99.7, 99.7, 397.5, 98.84, -1.12, 4658225)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (199, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Attock Petroleum Limited. ', N'Attock Petroleum Limited. ', 340.17, 341, 341, 397.5, 340.33, 0.82, 6800)
GO
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (200, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Burshane LPG (Pakistan) Limited. ', N'Burshane LPG (Pakistan) Limited. ', 29.25, 30, 30, 397.5, 29.5, 0.25, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (201, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hascol Petroleum Ltd. ', N'Hascol Petroleum Ltd. ', 15.52, 15.45, 16.18, 397.5, 16.06, 0.54, 16813000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (202, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hi-Tech Lubricants Limited. ', N'Hi-Tech Lubricants Limited. ', 36.21, 36.79, 37.73, 397.5, 36.97, 0.8, 604000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (203, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan State Oil Co Ltd. ', N'Pakistan State Oil Co Ltd. ', 186.52, 185, 190.25, 397.5, 189.15, 2.63, 1556612)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (204, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shell Pakistan Ltd. ', N'Shell Pakistan Ltd. ', 227.51, 228.01, 244.57, 397.5, 244.57, 17.06, 329400)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (205, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sui Northern Gas Pipe Line Ltd.(XD) ', N'Sui Northern Gas Pipe Line Ltd.(XD) ', 63.34, 63.8, 64.9, 397.5, 64.58, 1.24, 4062500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (206, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sui Southern Gas Co Ltd. ', N'Sui Southern Gas Co Ltd. ', 17.36, 17.37, 17.6, 397.5, 17.25, -0.11, 2774000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (207, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Baluchistan Particle Board Ltd [ DEFAULTER SEGMENT ]', N'Baluchistan Particle Board Ltd [ DEFAULTER SEGMENT ]', 16.56, 16.5, 17.68, 397.5, 17.26, 1.12, 32000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (208, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Century Paper & Board Mills. ', N'Century Paper & Board Mills. ', 86.98, 87.9, 89.75, 397.5, 86.99, 0.01, 1867000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (209, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Cherat Packaging Limited. ', N'Cherat Packaging Limited. ', 166.01, 170, 178.46, 397.5, 178.46, 12.45, 409400)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (210, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Merit Packaging Ltd. ', N'Merit Packaging Ltd. ', 12.94, 12.85, 13, 397.5, 12.9, -0.04, 206000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (211, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Packages Ltd. ', N'Packages Ltd. ', 408.32, 407.98, 419, 397.5, 415.8, 6.68, 395300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (212, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Roshan Packages Limited. ', N'Roshan Packages Limited. ', 25.68, 25.5, 27.1, 397.5, 26.91, 1.23, 1050000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (213, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Security Papers Ltd.(XD) ', N'Security Papers Ltd.(XD) ', 153.71, 151.2, 155, 397.5, 154.47, 0.44, 10900)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (214, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Abbott Laboratories Pak Ltd. ', N'Abbott Laboratories Pak Ltd. ', 690, 690, 699, 397.5, 690.49, 0.15, 230100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (215, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'AGP Limited. ', N'AGP Limited. ', 115.8, 115.5, 116, 397.5, 114.55, -1.25, 336000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (216, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ferozsons Laboratories Ltd. ', N'Ferozsons Laboratories Ltd. ', 360.62, 363, 387.66, 397.5, 386.7, 27.04, 1180200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (217, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Glaxo SmithKline Healthcare Pak Ltd. ', N'Glaxo SmithKline Healthcare Pak Ltd. ', 310.03, 310.04, 318, 397.5, 316.19, 6.97, 24800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (218, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Glaxo SmithKline Pakistan Ltd. ', N'Glaxo SmithKline Pakistan Ltd. ', 176.54, 179.99, 187.47, 397.5, 184.96, 8.42, 301900)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (219, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Highnoon Laboratories Ltd. ', N'Highnoon Laboratories Ltd. ', 596.53, 597, 618, 397.5, 611.85, 14.97, 248700)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (220, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'IBL HealthCare Limited. ', N'IBL HealthCare Limited. ', 76.06, 72.6, 78.5, 397.5, 78.18, 2.04, 100500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (221, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Macter International Limited. ', N'Macter International Limited. ', 132, 132, 132, 397.5, 132, 0, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (222, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sanofi-Aventis Pakistan Ltd. ', N'Sanofi-Aventis Pakistan Ltd. ', 858.17, 874.98, 874.98, 397.5, 874.98, 16.81, 50)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (223, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'The Searle Company Ltd. ', N'The Searle Company Ltd. ', 248.79, 250.05, 261, 397.5, 259.97, 11.18, 2462600)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (224, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Altern Energy Ltd. ', N'Altern Energy Ltd. ', 26, 26, 26, 397.5, 25.89, -0.1, 25500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (225, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Arshad Energy Limited. ', N'Arshad Energy Limited. ', 5.97, 5.97, 5.97, 397.5, 5.9, -0.07, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (226, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Engro Powergen Qadirpur Ltd. ', N'Engro Powergen Qadirpur Ltd. ', 23.21, 23.11, 23.49, 397.5, 23.41, 0.22, 71500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (227, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hub Power Company Limited. ', N'Hub Power Company Limited. ', 80.59, 80.5, 81.65, 397.5, 80.44, -0.19, 2533165)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (228, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'K-Electric Limited. ', N'K-Electric Limited. ', 3.71, 3.72, 3.74, 397.5, 3.71, -0.01, 2900500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (229, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Energy Ltd. ', N'Kohinoor Energy Ltd. ', 36, 35.7, 35.7, 397.5, 35.7, -0.3, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (230, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Power Co Ltd. [ DEFAULTER SEGMENT ]', N'Kohinoor Power Co Ltd. [ DEFAULTER SEGMENT ]', 2.19, 2.03, 2.19, 397.5, 2.15, 0, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (231, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kot Addu Power Company. ', N'Kot Addu Power Company. ', 27.66, 27.85, 28.2, 397.5, 28.01, 0.24, 5503000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (232, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'LALPIR Power Limited. ', N'LALPIR Power Limited. ', 13.15, 13.21, 14.15, 397.5, 13.3, 0.15, 1316500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (233, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nishat Chunian Power Ltd. ', N'Nishat Chunian Power Ltd. ', 14.62, 14.55, 14.75, 397.5, 14.44, -0.2, 525000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (234, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nishat Power Limited.(XD) ', N'Nishat Power Limited.(XD) ', 23.34, 23.2, 23.5, 397.5, 22.87, -0.34, 69000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (235, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakgen Power Limited. ', N'Pakgen Power Limited. ', 18.13, 17.55, 17.56, 397.5, 17.44, -0.83, 40000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (236, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Saif Power Ltd. ', N'Saif Power Ltd. ', 17.22, 17.3, 17.3, 397.5, 17, -0.23, 137000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (237, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Tri -Star Power Ltd. ', N'Tri -Star Power Ltd. ', 3.82, 3.9, 4, 397.5, 3.99, 0.17, 27000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (238, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Attock Refinery Limited. ', N'Attock Refinery Limited. ', 166.68, 168, 173.48, 397.5, 171.98, 5.3, 3001000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (239, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BYCO Petroleum Pak Ltd. ', N'BYCO Petroleum Pak Ltd. ', 7.61, 7.6, 7.9, 397.5, 7.81, 0.19, 1832000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (240, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'National Refinary Ltd. ', N'National Refinary Ltd. ', 184.79, 185, 189.29, 397.5, 188.32, 3.49, 688000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (241, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Refinery Ltd. ', N'Pakistan Refinery Ltd. ', 15.63, 15.7, 16.8, 397.5, 16.8, 1.17, 19421500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (242, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Abdullah Shaha Ghazi Suger Mills Ltd. ', N'Abdullah Shaha Ghazi Suger Mills Ltd. ', 2.3, 2.38, 2.38, 397.5, 2.25, -0.05, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (243, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Adam Sugar Mills Limited. [ DEFAULTER SEGMENT ]', N'Adam Sugar Mills Limited. [ DEFAULTER SEGMENT ]', 21, 21, 21, 397.5, 19.9, -1.1, 58500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (244, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Al-Abbas Sugar Mills Ltd.(XD) ', N'Al-Abbas Sugar Mills Ltd.(XD) ', 317, 317, 317, 397.5, 317, 0, 100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (245, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Al-Noor Sugar Mills Ltd. ', N'Al-Noor Sugar Mills Ltd. ', 49.43, 48.1, 49.5, 397.5, 49.5, 0.07, 14500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (246, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Chashma Sugar Mills Ltd. ', N'Chashma Sugar Mills Ltd. ', 89.5, 89.9, 94.49, 397.5, 92.07, 4.5, 66500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (247, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]', N'Dewan Sugar Mills Ltd. [ DEFAULTER SEGMENT ]', 2.57, 2.75, 3, 397.5, 2.9, 0.33, 136000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (248, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Habib Sugar Mills Ltd. ', N'Habib Sugar Mills Ltd. ', 34.58, 34.88, 34.88, 397.5, 34.59, 0.01, 4000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (249, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Husein Sugar Mills Limited. ', N'Husein Sugar Mills Limited. ', 15.55, 15.5, 15.95, 397.5, 15.95, 0.4, 6000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (250, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Imperial Sugar Limited. [ DEFAULTER SEGMENT ]', N'Imperial Sugar Limited. [ DEFAULTER SEGMENT ]', 14.51, 15.49, 15.49, 397.5, 14.23, -0.01, 7000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (251, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'J. D. W. Sugar Mills Ltd. ', N'J. D. W. Sugar Mills Ltd. ', 227, 227, 227, 397.5, 225.01, -2, 600)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (252, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Jauharabad Sugar Mills Ltd. ', N'Jauharabad Sugar Mills Ltd. ', 15.39, 15.45, 15.55, 397.5, 15.55, 0.16, 9000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (253, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Khairpur Sugar Mills Ltd. ', N'Khairpur Sugar Mills Ltd. ', 97.6, 100.5, 100.5, 397.5, 90.46, -7.32, 9000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (254, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Mehran Sugar Mills Ltd. ', N'Mehran Sugar Mills Ltd. ', 60.52, 59, 59, 397.5, 59, -1.52, 7500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (255, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Noon Sugar Mills Ltd. ', N'Noon Sugar Mills Ltd. ', 70, 68.1, 68.1, 397.5, 68, -2, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (256, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Premier Sugar Mills & Distille ', N'Premier Sugar Mills & Distille ', 611.95, 0, 0, 397.5, 611.95, 0, 100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (257, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sakrand Sugar Mills Ltd. ', N'Sakrand Sugar Mills Ltd. ', 9.45, 9.55, 9.75, 397.5, 9.2, -0.3, 393000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (258, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shahmurad Sugar Mills Ltd. ', N'Shahmurad Sugar Mills Ltd. ', 104.9, 104.95, 104.95, 397.5, 103.72, -1.76, 3500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (259, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Tandliawala Sugar Mills Ltd. ', N'Tandliawala Sugar Mills Ltd. ', 175, 169.99, 187.5, 397.5, 186.25, 12.5, 5500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (260, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Thal Industries Corporation Ltd. ', N'Thal Industries Corporation Ltd. ', 248.5, 247, 247, 397.5, 248.5, -1.5, 100)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (261, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Gatron (Industries) Ltd. ', N'Gatron (Industries) Ltd. ', 630, 625.5, 625.5, 397.5, 620.07, -9.93, 1250)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (262, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Synthentics Ltd. ', N'Pakistan Synthentics Ltd. ', 17.14, 17, 17.47, 397.5, 17.47, 0.33, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (263, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Rupali Polyester Ltd. ', N'Rupali Polyester Ltd. ', 16.4, 16.4, 17.31, 397.5, 17.28, 0.88, 7000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (264, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Tri-Star Polyester Ltd. ', N'Tri-Star Polyester Ltd. ', 8.55, 8.55, 9.1, 397.5, 8.58, 0.03, 1828000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (265, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Avanceon Limited. ', N'Avanceon Limited. ', 58.42, 59.74, 59.95, 397.5, 59.65, 1.23, 970000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (266, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hum Network Limited. ', N'Hum Network Limited. ', 7.93, 8.1, 8.35, 397.5, 8.1, 0.18, 1130500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (267, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Media Times Limited. ', N'Media Times Limited. ', 1.25, 1.32, 1.68, 397.5, 1.49, 0.23, 7288500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (268, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NetSol Technologies Ltd. ', N'NetSol Technologies Ltd. ', 63, 63.4, 64.24, 397.5, 63.91, 1.24, 635000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (269, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Telecommunication Co. ', N'Pakistan Telecommunication Co. ', 9.24, 9.38, 9.38, 397.5, 9.26, 0.02, 1353500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (270, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Systems Limited. ', N'Systems Limited. ', 190.53, 193, 196.7, 397.5, 195.86, 5.47, 101900)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (271, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Telecard Ltd. ', N'Telecard Ltd. ', 1.27, 1.34, 1.39, 397.5, 1.3, 0.03, 580500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (272, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TPL CORP Limited. ', N'TPL CORP Limited. ', 7.15, 7.2, 7.2, 397.5, 6.74, -0.41, 4629500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (273, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TPL Trakker Limited. ', N'TPL Trakker Limited. ', 9.92, 9.75, 10.2, 397.5, 10.02, -0.03, 374500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (274, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TRG Pakistan Ltd. ', N'TRG Pakistan Ltd. ', 50.82, 51.01, 54.63, 397.5, 54.46, 3.81, 29893500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (275, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Worldcall Telecom Ltd. ', N'Worldcall Telecom Ltd. ', 1.21, 1.18, 1.27, 397.5, 1.18, -0.03, 20166000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (276, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Artistic Denim Mills Limited. ', N'Artistic Denim Mills Limited. ', 57.58, 0, 0, 397.5, 57.58, 0, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (277, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Azgard Nine Limited. ', N'Azgard Nine Limited. ', 17.24, 17.75, 17.75, 397.5, 17.27, 0.03, 2767000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (278, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bleesed Textile Ltd. ', N'Bleesed Textile Ltd. ', 290, 290, 290, 397.5, 290, 0, 300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (279, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Crescent Textile Mills Ltd. ', N'Crescent Textile Mills Ltd. ', 21.27, 21.25, 21.4, 397.5, 21.28, -0.01, 20000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (280, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Faisal Spinning Mills Ltd. ', N'Faisal Spinning Mills Ltd. ', 320, 329.99, 329.99, 397.5, 300, -20, 2200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (281, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Feroze1888 Mills Ltd. ', N'Feroze1888 Mills Ltd. ', 94, 94, 96.75, 397.5, 96.75, 2.75, 5000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (282, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ghazi Fabrics International Ltd. ', N'Ghazi Fabrics International Ltd. ', 3.25, 0, 0, 397.5, 3.25, 0, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (283, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Gul Ahmed Textile Mills Ltd. ', N'Gul Ahmed Textile Mills Ltd. ', 36, 36.49, 36.79, 397.5, 35.9, -0.11, 177500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (284, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Interloop Limited. ', N'Interloop Limited. ', 50.47, 51, 51.9, 397.5, 51.64, 1.17, 47500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (285, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'International Knitwear Ltd. ', N'International Knitwear Ltd. ', 17.5, 17.5, 17.5, 397.5, 17.5, 0, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (286, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Jubilee Spinning & Weaving Mil [ DEFAULTER SEGMENT ]', N'Jubilee Spinning & Weaving Mil [ DEFAULTER SEGMENT ]', 3.3, 0, 0, 397.5, 3.3, 0, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (287, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Industries Ltd. ', N'Kohinoor Industries Ltd. ', 3.7, 3.71, 3.72, 397.5, 3.72, 0.02, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (288, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Mills Ltd. ', N'Kohinoor Mills Ltd. ', 36.5, 36.85, 37, 397.5, 37, 0.5, 6500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (289, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Textile Mills Ltd. ', N'Kohinoor Textile Mills Ltd. ', 50.97, 50.5, 52, 397.5, 51.58, 1.03, 7500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (290, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Masood Textile Mills Ltd. ', N'Masood Textile Mills Ltd. ', 60.01, 64.5, 64.5, 397.5, 64.5, 4.49, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (291, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nishat (Chunia) Ltd. ', N'Nishat (Chunia) Ltd. ', 40.71, 40, 41.1, 397.5, 40.75, 0.04, 260000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (292, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nishat Mills Ltd. ', N'Nishat Mills Ltd. ', 102.94, 103.98, 106.07, 397.5, 105.06, 2.12, 1111500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (293, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Redco Textile Ltd. ', N'Redco Textile Ltd. ', 6.95, 7.14, 7.14, 397.5, 7.14, 0.19, 1000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (294, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Reliance Weaving Mills Ltd. ', N'Reliance Weaving Mills Ltd. ', 25, 25.95, 26, 397.5, 25.25, 0.25, 14500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (295, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sapphire Fibres Mills Ltd. ', N'Sapphire Fibres Mills Ltd. ', 805, 756, 770.2, 397.5, 770.1, -34.9, 200)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (296, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Sapphire Textile Mills Ltd. ', N'Sapphire Textile Mills Ltd. ', 870, 899.99, 899.99, 397.5, 899.99, 29.99, 50)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (297, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Asim Textile Mills Ltd. ', N'Asim Textile Mills Ltd. ', 8.1, 7.63, 8.48, 397.5, 8.48, 0.38, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (298, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bilal Fibres Ltd. [ DEFAULTER SEGMENT ]', N'Bilal Fibres Ltd. [ DEFAULTER SEGMENT ]', 1.15, 1.07, 1.07, 397.5, 1.07, -0.08, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (299, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Chakwal Spinning Mills Limited. ', N'Chakwal Spinning Mills Limited. ', 1.93, 1.93, 1.94, 397.5, 1.92, 0.01, 74500)
GO
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (300, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Colony Textile Mills Ltd. ', N'Colony Textile Mills Ltd. ', 3.51, 3.7, 3.7, 397.5, 3.57, 0.07, 52500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (301, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'D. S. Industries Ltd. ', N'D. S. Industries Ltd. ', 1.75, 1.8, 1.9, 397.5, 1.84, 0.09, 164500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (302, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dewan Farooque Spinning Mills Ltd. ', N'Dewan Farooque Spinning Mills Ltd. ', 1.66, 1.75, 1.77, 397.5, 1.7, 0.04, 63500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (303, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Din Textile Mills Ltd. ', N'Din Textile Mills Ltd. ', 49, 47, 50, 397.5, 50, 1, 18000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (304, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Elahi Cotton Mills Ltd. ', N'Elahi Cotton Mills Ltd. ', 33.68, 36.2, 36.2, 397.5, 36.2, 2.52, 1500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (305, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Fazal Cloth Mills Ltd. ', N'Fazal Cloth Mills Ltd. ', 139.97, 138, 138, 397.5, 135.37, -4.61, 1300)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (306, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Hira Textile Mills Ltd. [ DEFAULTER SEGMENT ]', N'Hira Textile Mills Ltd. [ DEFAULTER SEGMENT ]', 1.95, 1.91, 2.02, 397.5, 2, 0.05, 164500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (307, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Indus Dyeing & Manufacturing. ', N'Indus Dyeing & Manufacturing. ', 540, 526.01, 526.01, 397.5, 526.01, -13.99, 50)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (308, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Island Textile Mills Ltd. ', N'Island Textile Mills Ltd. ', 991.99, 1065.94, 1065.94, 397.5, 1065.94, 73.95, 40)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (309, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'J. A. Textile Mills Ltd. ', N'J. A. Textile Mills Ltd. ', 5.2, 5, 5, 397.5, 5, -0.2, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (310, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Janana De Malucha Tex Mills. ', N'Janana De Malucha Tex Mills. ', 70, 70.02, 70.02, 397.5, 70.02, 0.02, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (311, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Khurshid Spinning Mills Ltd. ', N'Khurshid Spinning Mills Ltd. ', 7.55, 7.5, 7.5, 397.5, 7.5, -0.05, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (312, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Kohinoor Spinning Mills Ltd. ', N'Kohinoor Spinning Mills Ltd. ', 2.41, 2.45, 2.8, 397.5, 2.69, 0.33, 827000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (313, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Maqbool Textile Mills Ltd. ', N'Maqbool Textile Mills Ltd. ', 32.4, 34.49, 34.49, 397.5, 34.49, 2.09, 2000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (314, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Nazir Cotton Mills Ltd. [ DEFAULTER SEGMENT ]', N'Nazir Cotton Mills Ltd. [ DEFAULTER SEGMENT ]', 3.42, 3.45, 3.45, 397.5, 3.33, -0.09, 14000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (315, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ravi Textile Mills Ltd. [ DEFAULTER SEGMENT ]', N'Ravi Textile Mills Ltd. [ DEFAULTER SEGMENT ]', 3.5, 3.5, 3.55, 397.5, 3.47, -0.03, 14000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (316, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ruby Textile Mills Ltd. ', N'Ruby Textile Mills Ltd. ', 6.83, 6.72, 7.25, 397.5, 7, 0.37, 4500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (317, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Salfi Textile Mills Ltd. ', N'Salfi Textile Mills Ltd. ', 148.35, 159, 159.47, 397.5, 159.32, 11.1, 1800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (318, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shadab Textile Mills Ltd. ', N'Shadab Textile Mills Ltd. ', 29, 29, 29.5, 397.5, 29.5, 0.5, 11000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (319, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Ashfaq Textile Mills Ltd. ', N'Ashfaq Textile Mills Ltd. ', 14, 14.02, 14.02, 397.5, 14.02, 0.02, 500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (320, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Samin Textiles Ltd. ', N'Samin Textiles Ltd. ', 3.7, 3.35, 3.35, 397.5, 3.35, -0.35, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (321, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Service Fabrics Ltd. ', N'Service Fabrics Ltd. ', 3.4, 3.45, 3.49, 397.5, 3.34, 0, 10000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (322, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Shahtaj Textile Ltd. ', N'Shahtaj Textile Ltd. ', 120.74, 111.69, 111.7, 397.5, 111.69, -9.05, 4000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (323, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Yousuf Weaving Mills Limited. ', N'Yousuf Weaving Mills Limited. ', 3.44, 3.5, 3.6, 397.5, 3.48, 0.03, 289000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (324, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Khyber Tobacco Co. Ltd. ', N'Khyber Tobacco Co. Ltd. ', 210.2, 208.01, 215, 397.5, 215, 4.8, 2800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (325, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Tobacco Co Ltd. ', N'Pakistan Tobacco Co Ltd. ', 1656, 1680, 1680, 397.5, 1680, 24, 20)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (326, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pak International Airline Corp Ltd ', N'Pak International Airline Corp Ltd ', 4.69, 4.83, 4.88, 397.5, 4.75, 0.06, 505500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (327, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Int.Container Terminal.(XD) ', N'Pakistan Int.Container Terminal.(XD) ', 232.61, 235, 235, 397.5, 215.36, -17.44, 69800)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (328, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan Intl. Bulk Terminal Ltd. ', N'Pakistan Intl. Bulk Terminal Ltd. ', 12.26, 12.35, 12.38, 397.5, 12.32, 0.06, 3811000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (329, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Pakistan National Shipping Co. ', N'Pakistan National Shipping Co. ', 88.99, 89.8, 91, 397.5, 90.48, 1.71, 7500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (330, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Unity Foods Limited. ', N'Unity Foods Limited. ', 14.37, 14.35, 14.48, 397.5, 14.29, -0.08, 9243500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (331, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Unity Foods Limited.(R2) ', N'Unity Foods Limited.(R2) ', 4.17, 4.17, 4.2, 397.5, 4.03, -0.16, 16278500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (332, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Bannu Woollen Mills Limited. ', N'Bannu Woollen Mills Limited. ', 37.8, 36.85, 39, 397.5, 36.5, -1.3, 5000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (333, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'Dolmen City REIT. ', N'Dolmen City REIT. ', 10.99, 10.86, 11, 397.5, 11, 0.01, 799500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (334, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NIT Pakistan Gateway  ETF. ', N'NIT Pakistan Gateway  ETF. ', 12, 11.16, 12, 397.5, 12, 0, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (335, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'UBL Pakistan Enterprise ETF.(XD) ', N'UBL Pakistan Enterprise ETF.(XD) ', 12.71, 12.63, 12.63, 397.5, 12.63, -0.08, 28500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (336, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ASTL-AUG', N'ASTL-AUG', 47.14, 47.1, 49.7, 397.5, 49.46, 2.56, 1349000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (337, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ASTL-SEP', N'ASTL-SEP', 47.14, 47.65, 50.1, 397.5, 49.94, 2.77, 555000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (338, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ATRL-AUG', N'ATRL-AUG', 167.09, 168, 173.4, 397.5, 172.26, 5.83, 1903500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (339, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ATRL-SEP', N'ATRL-SEP', 167.09, 168, 174.49, 397.5, 173.37, 6.91, 989500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (340, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'AVN-AUG', N'AVN-AUG', 58.52, 59.7, 59.7, 397.5, 59.62, 1.13, 569000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (341, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'AVN-SEP', N'AVN-SEP', 58.52, 59.25, 60.5, 397.5, 60.04, 1.28, 124000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (342, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BAHL-AUG', N'BAHL-AUG', 65.5, 61.1, 66.75, 397.5, 66.08, 1.25, 15000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (343, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BAFL-AUG', N'BAFL-AUG', 37, 36.45, 36.8, 397.5, 36.54, -0.44, 23000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (344, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BAFL-SEP', N'BAFL-SEP', 37, 37.3, 37.3, 397.5, 36.91, -0.09, 143000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (345, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BOP-AUG', N'BOP-AUG', 10.08, 10.1, 10.2, 397.5, 10.19, 0.11, 850500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (346, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'BOP-SEP', N'BOP-SEP', 10.08, 10.2, 10.45, 397.5, 10.35, 0.27, 775500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (347, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'CEPB-SEP', N'CEPB-SEP', 86.98, 89, 92, 397.5, 87.86, 1.21, 191000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (348, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'CHCC-AUG', N'CHCC-AUG', 132.58, 133.25, 136.39, 397.5, 133.58, 0.92, 953000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (349, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'CHCC-SEP', N'CHCC-SEP', 132.58, 136, 137, 397.5, 134.92, 2.32, 581000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (350, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'DGKC-AUG', N'DGKC-AUG', 106.61, 107, 108.3, 397.5, 107.91, 1.64, 3289000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (351, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'DGKC-SEP', N'DGKC-SEP', 106.61, 107.5, 111.75, 397.5, 108.8, 2.19, 2311000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (352, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'DOL-AUG', N'DOL-AUG', 29.95, 30.29, 30.29, 397.5, 29.99, -0.02, 91500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (353, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'DOL-SEP', N'DOL-SEP', 29.95, 30.35, 30.35, 397.5, 30.26, 0.33, 113000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (354, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ENGRO-SEP', N'ENGRO-SEP', 284.58, 286, 290, 397.5, 287.79, 3.42, 96500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (355, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ENGRO-AUG', N'ENGRO-AUG', 292.58, 292.1, 294, 397.5, 293.22, 0.67, 110000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (356, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EFERT-SEP', N'EFERT-SEP', 57.96, 61.49, 61.49, 397.5, 57.98, -0.01, 686500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (357, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EFERT-AUG', N'EFERT-AUG', 61.96, 61.52, 61.7, 397.5, 61.36, -0.56, 180000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (358, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EPCL-AUG', N'EPCL-AUG', 33.59, 34.23, 35, 397.5, 34.51, 1.01, 642000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (359, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'EPCL-SEP', N'EPCL-SEP', 33.59, 34.51, 35, 397.5, 34.78, 1.31, 551500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (360, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FCCL-AUG', N'FCCL-AUG', 20.03, 19.5, 20.91, 397.5, 20.8, 0.63, 1286500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (361, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FCCL-SEP', N'FCCL-SEP', 20.03, 20.05, 21.1, 397.5, 20.96, 0.87, 929000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (362, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FFBL-AUG', N'FFBL-AUG', 18.38, 18.5, 18.57, 397.5, 18.4, 0.02, 535000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (363, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FFBL-SEP', N'FFBL-SEP', 18.38, 18.6, 19.54, 397.5, 18.56, 0.12, 580500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (364, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FFC-SEP', N'FFC-SEP', 106.38, 108, 108.25, 397.5, 107, 0.62, 84000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (365, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FFC-AUG', N'FFC-AUG', 109.13, 109.99, 110.8, 397.5, 109.25, 0.12, 110500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (366, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FFL-AUG', N'FFL-AUG', 12.77, 12.63, 12.89, 397.5, 12.83, 0.08, 1132000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (367, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'FEROZ-SEP', N'FEROZ-SEP', 360.62, 369.65, 387.66, 397.5, 386.37, 27.04, 23000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (368, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GTYR-AUG', N'GTYR-AUG', 74.74, 73.7, 76, 397.5, 75.98, 1.24, 90500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (369, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GTYR-SEP', N'GTYR-SEP', 74.74, 75.4, 77.35, 397.5, 76.72, 2.48, 60000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (370, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GHNI-AUG', N'GHNI-AUG', 286.45, 291, 299.4, 397.5, 295.05, 8.6, 1191500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (371, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GHNI-SEP', N'GHNI-SEP', 286.45, 294.99, 301.99, 397.5, 298.01, 11.55, 434500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (372, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GHNL-AUG', N'GHNL-AUG', 113.32, 113, 115.2, 397.5, 113.82, 0.58, 229500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (373, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GHNL-SEP', N'GHNL-SEP', 113.32, 115.5, 116, 397.5, 114.96, 0.89, 87500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (374, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GATM-AUG', N'GATM-AUG', 36.25, 36.7, 36.8, 397.5, 36.24, -0.05, 20500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (375, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'GATM-SEP', N'GATM-SEP', 36.25, 36.5, 36.5, 397.5, 36.25, 0, 20000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (376, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HBL-AUG', N'HBL-AUG', 124.65, 124.5, 126, 397.5, 125.64, -0.24, 402500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (377, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HBL-SEP', N'HBL-SEP', 124.65, 125.01, 127.3, 397.5, 126.74, 1.6, 376000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (378, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HASCOL-AUG', N'HASCOL-AUG', 15.54, 15.41, 16.19, 397.5, 16.07, 0.53, 8756500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (379, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HASCOL-SEP', N'HASCOL-SEP', 15.54, 16.48, 16.5, 397.5, 16.23, 0.69, 7537000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (380, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HUBC-AUG', N'HUBC-AUG', 80.84, 80.5, 81.7, 397.5, 80.54, -0.29, 318000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (381, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'HUBC-SEP', N'HUBC-SEP', 80.84, 81.5, 82.4, 397.5, 81.08, 0.24, 251000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (382, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'INIL-AUG', N'INIL-AUG', 131.36, 132.5, 134.6, 397.5, 134.14, 3.24, 376500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (383, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'INIL-SEP', N'INIL-SEP', 131.36, 130, 136, 397.5, 135.63, 4.64, 169000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (384, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ISL-AUG', N'ISL-AUG', 71.13, 71.5, 72.5, 397.5, 72.26, 1.07, 1438500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (385, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'ISL-SEP', N'ISL-SEP', 71.13, 72, 73.25, 397.5, 72.93, 1.8, 418000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (386, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KEL-AUG', N'KEL-AUG', 3.73, 3.7, 3.75, 397.5, 3.7, 0, 305000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (387, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KEL-SEP', N'KEL-SEP', 3.73, 3.78, 3.8, 397.5, 3.74, 0.06, 238500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (388, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KOHC-AUG', N'KOHC-AUG', 167.22, 0, 0, 397.5, 167.25, 0, 2500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (389, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KOHC-SEP', N'KOHC-SEP', 167.22, 170, 170, 397.5, 170, 2.78, 5500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (390, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KAPCO-AUG', N'KAPCO-AUG', 27.72, 27.9, 28.19, 397.5, 28.02, 0.18, 1916000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (391, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'KAPCO-SEP', N'KAPCO-SEP', 27.72, 28.1, 28.45, 397.5, 28.21, 0.43, 924000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (392, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'LOTCHEM-AUG', N'LOTCHEM-AUG', 12.36, 12.21, 12.55, 397.5, 12.46, 0.19, 228000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (393, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'LOTCHEM-SEP', N'LOTCHEM-SEP', 12.36, 12.4, 12.49, 397.5, 12.49, 0.13, 175500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (394, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'LUCK-AUG', N'LUCK-AUG', 598.26, 601, 616, 397.5, 610.74, 15.85, 710000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (395, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'LUCK-SEP', N'LUCK-SEP', 598.26, 603, 622, 397.5, 616.35, 20.74, 228500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (396, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MLCF-AUG', N'MLCF-AUG', 34.2, 34.2, 35.12, 397.5, 34.9, 0.7, 5549000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (397, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MLCF-SEP', N'MLCF-SEP', 34.2, 34.6, 35.45, 397.5, 35.25, 1.2, 3587500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (398, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MCB-AUG', N'MCB-AUG', 169.99, 169.01, 169.01, 397.5, 168.5, -1.49, 4000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (399, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MEBL-AUG', N'MEBL-AUG', 86.49, 86.99, 87.9, 397.5, 86.04, -0.31, 38500)
GO
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (400, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MEBL-SEP', N'MEBL-SEP', 78.63, 82, 82, 397.5, 79.64, 1.17, 45500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (401, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MUGHAL-AUG', N'MUGHAL-AUG', 61.67, 62.5, 66.29, 397.5, 66.2, 4.62, 810000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (402, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'MUGHAL-SEP', N'MUGHAL-SEP', 61.67, 64.49, 66.29, 397.5, 66.29, 4.62, 77500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (403, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NBP-AUG', N'NBP-AUG', 35.09, 35.25, 36.55, 397.5, 36.25, 1.46, 386500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (404, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NBP-SEP', N'NBP-SEP', 35.09, 37.48, 37.48, 397.5, 36.74, 1.65, 173500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (405, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NRL-AUG', N'NRL-AUG', 185.32, 185, 189.5, 397.5, 188.77, 2.69, 445500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (406, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NRL-SEP', N'NRL-SEP', 185.32, 187.5, 191, 397.5, 190.09, 4.73, 54000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (407, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NETSOL-AUG', N'NETSOL-AUG', 63.15, 63.98, 64.3, 397.5, 63.91, 0.86, 292000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (408, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NETSOL-SEP', N'NETSOL-SEP', 63.15, 63.66, 64.7, 397.5, 64.53, 1.41, 53000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (409, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NCL-AUG', N'NCL-AUG', 40.93, 41, 41.05, 397.5, 41, 0.07, 31000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (410, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NCL-SEP', N'NCL-SEP', 40.93, 41.4, 41.45, 397.5, 41.44, 0.52, 24500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (411, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NML-AUG', N'NML-AUG', 103.09, 103.5, 106, 397.5, 105.02, 1.92, 279000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (412, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'NML-SEP', N'NML-SEP', 103.09, 105.3, 107, 397.5, 106.01, 2.91, 187500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (413, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'OGDC-AUG', N'OGDC-AUG', 115.16, 114.1, 114.5, 397.5, 113.02, -1.97, 177000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (414, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'OGDC-SEP', N'OGDC-SEP', 115.16, 115, 115, 397.5, 114.12, -1.16, 246500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (415, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PAEL-AUG', N'PAEL-AUG', 33.26, 33.12, 33.77, 397.5, 33.72, 0.45, 4289500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (416, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PAEL-SEP', N'PAEL-SEP', 33.26, 33.51, 35.6, 397.5, 34.05, 0.79, 2929000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (417, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PSMC-SEP', N'PSMC-SEP', 237.52, 244, 249.9, 397.5, 246.72, 10.48, 61000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (418, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PIBTL-AUG', N'PIBTL-AUG', 12.28, 12.22, 12.39, 397.5, 12.34, 0.02, 1499500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (419, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PIBTL-SEP', N'PIBTL-SEP', 12.28, 12.45, 12.51, 397.5, 12.44, 0.16, 709500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (420, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'POL-AUG', N'POL-AUG', 421.49, 422, 422, 397.5, 415.5, -5.99, 13000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (421, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'POL-SEP', N'POL-SEP', 421.49, 421.5, 424.06, 397.5, 418.12, -3.49, 10000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (422, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PPL-AUG', N'PPL-AUG', 100.37, 99, 99.7, 397.5, 98.71, -1.22, 2559500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (423, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PPL-SEP', N'PPL-SEP', 100.37, 100.1, 100.5, 397.5, 99.51, -0.39, 2346000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (424, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PRL-SEP', N'PRL-SEP', 15.63, 15.81, 16.8, 397.5, 16.79, 1.17, 834500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (425, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PSO-AUG', N'PSO-AUG', 186.79, 186.25, 190.25, 397.5, 189.32, 2.76, 968000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (426, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PSO-SEP', N'PSO-SEP', 186.79, 188.3, 195, 397.5, 190.96, 4.61, 727500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (427, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PIOC-AUG', N'PIOC-AUG', 87.63, 87.25, 89, 397.5, 88.56, 1.17, 1568000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (428, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'PIOC-SEP', N'PIOC-SEP', 87.63, 90, 90, 397.5, 89.36, 2.07, 965000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (429, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'POWER-SEP', N'POWER-SEP', 8.87, 9.05, 9.2, 397.5, 9.15, 0.28, 456000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (430, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SNGP-AUG', N'SNGP-AUG', 63.38, 63.99, 64.75, 397.5, 64.51, 1.27, 1093000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (431, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SNGP-SEP', N'SNGP-SEP', 63.38, 64.5, 65.44, 397.5, 65.16, 1.95, 598500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (432, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SSGC-AUG', N'SSGC-AUG', 17.36, 17.3, 17.65, 397.5, 17.24, -0.17, 968000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (433, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SSGC-SEP', N'SSGC-SEP', 17.36, 17.7, 17.75, 397.5, 17.48, -0.01, 897500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (434, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TGL-SEP', N'TGL-SEP', 84.54, 87.5, 90.88, 397.5, 88.96, 6.34, 20500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (435, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SEARL-AUG', N'SEARL-AUG', 248.93, 250, 261.4, 397.5, 260.04, 11.08, 1198000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (436, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'SEARL-SEP', N'SEARL-SEP', 248.93, 255, 264.99, 397.5, 262.11, 13.57, 772000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (437, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TRG-AUG', N'TRG-AUG', 50.93, 51.15, 54.74, 397.5, 54.53, 3.6, 29156000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (438, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'TRG-SEP', N'TRG-SEP', 50.93, 51.51, 54.74, 397.5, 54.74, 3.81, 24072000)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (439, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'UBL-AUG', N'UBL-AUG', 118.62, 118, 118.6, 397.5, 118.42, -0.37, 39500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (440, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'UBL-SEP', N'UBL-SEP', 118.62, 119.14, 119.6, 397.5, 119.33, 0.5, 47500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (441, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'UNITY-AUG', N'UNITY-AUG', 14.45, 14.48, 14.5, 397.5, 14.3, -0.1, 3953500)
INSERT [dbo].[MarketSummaryHistory] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_SECTOR], [MS_COMPANY_NAME], [MS_COMPANY_SYMBOL], [MS_COMPANY_LDCP], [MS_COMPANY_OPEN], [MS_COMPANY_HIGH], [MS_COMPANY_LOW], [MS_COMPANY_CURRENT], [MS_COMPANY_CHANGE], [MS_COMPANY_VOLUME]) VALUES (442, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N' AUTOMOBILE ASSEMBLER
', N'PostClose', N'UNITY-SEP', N'UNITY-SEP', 14.45, 13.76, 14.6, 397.5, 14.37, -0.02, 4675500)
SET IDENTITY_INSERT [dbo].[MarketSummaryHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[MarketSummaryOverview] ON 

INSERT [dbo].[MarketSummaryOverview] ([MS_ID], [MS_DATE], [MS_STATUS], [MS_VOLUME], [MS_VALUE], [MS_TRADES]) VALUES (1, CAST(N'2020-08-24T16:11:01.000' AS DateTime), N'PostClose', 14928791062, 371688649, 135910)
SET IDENTITY_INSERT [dbo].[MarketSummaryOverview] OFF
GO
SET IDENTITY_INSERT [dbo].[MarketSummaryOverviewHistory] ON 

INSERT [dbo].[MarketSummaryOverviewHistory] ([MS_ID], [MS_DATE], [MS_SYS_DATE], [MS_STATUS], [MS_VOLUME], [MS_VALUE], [MS_TRADES]) VALUES (1, CAST(N'2020-08-24T16:11:01.000' AS DateTime), CAST(N'2020-08-24T04:11:57.877' AS DateTime), N'PostClose', 14928791062, 371688649, 135910)
SET IDENTITY_INSERT [dbo].[MarketSummaryOverviewHistory] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__FUND__D9E8960E74F68395]    Script Date: 9/11/2020 5:50:26 AM ******/
ALTER TABLE [dbo].[FUND] ADD UNIQUE NONCLUSTERED 
(
	[FUND_SYMBOL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__FUND__F30D047ECE7EE0D6]    Script Date: 9/11/2020 5:50:26 AM ******/
ALTER TABLE [dbo].[FUND] ADD UNIQUE NONCLUSTERED 
(
	[FUND_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__MarketSu__B2E893CFE2C5595A]    Script Date: 9/11/2020 5:50:26 AM ******/
ALTER TABLE [dbo].[MarketSummary] ADD UNIQUE NONCLUSTERED 
(
	[MS_COMPANY_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FUND] ADD  DEFAULT ((0)) FOR [FUND_CODE]
GO
/****** Object:  StoredProcedure [dbo].[ClearTableData]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ClearTableData]
AS
BEGIN
TRUNCATE TABLE MarketSummary

TRUNCATE TABLE MarketSummaryHistory

TRUNCATE TABLE MarketSummaryOverview

TRUNCATE TABLE MarketSummaryOverviewHistory
END
GO
/****** Object:  StoredProcedure [dbo].[spGetFund]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spGetFund]
AS
BEGIN
SELECT [FUND_ID]
      ,[FUND_NAME]
      ,[FUND_SYMBOL]
      ,[FUND_DESC]
  FROM [dbo].[FUND]

--SELECT [fund_code]
--      ,[fund_name]
--  FROM [ipams_db].[dbo].[pms_funds] order by fund_code
END
GO
/****** Object:  StoredProcedure [dbo].[spGetFundIDByParamNAME]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spGetFundIDByParamNAME]
		@FUND_NAME varchar(500)
		AS
		BEGIN
SELECT [FUND_CODE] FROM [dbo].[FUND] WHERE [FUND_NAME] = @FUND_NAME
END
GO
/****** Object:  StoredProcedure [dbo].[spGetShareDetailByParamFundIdAndDate]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spGetShareDetailByParamFundIdAndDate]
		@FUND_ID int,
		@DATE date
AS
BEGIN
select share_name, share_symbol,  
(select max(movAvg_tranDate)  from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID and movAvg_tranDate<@DATE ) as DateCostLastUpdated,

(select movAvg_average from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID
and movAvg_tranDate = (select max(movAvg_tranDate)  from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID and movAvg_tranDate<@DATE)
) as LastUpdatedPerUnitCost,

(select movAvg_amtMov from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID
and movAvg_tranDate = (select max(movAvg_tranDate)  from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID and movAvg_tranDate<@DATE)
) as LastUpdatedCost,

 (select movAvg_qty from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID
and movAvg_tranDate = (select max(movAvg_tranDate)  from pms_movingAvg
where pms_movingAvg.tradInstType_code=1 and pms_movingAvg.tradInst_code = share_code and pms_movingAvg.fund_code=@FUND_ID and movAvg_tranDate<@DATE)
) as LastUpdatedHolding,

(select DMP_date from pms_securityDMPrice
where pms_securityDMPrice.tradInstType_code=1 and pms_securityDMPrice.tradInst_code = share_code
and DMP_date = (select max(DMP_date)  from pms_securityDMPrice
where pms_securityDMPrice.tradInstType_code=1 and pms_securityDMPrice.tradInst_code = share_code and DMP_date  < @DATE)
) as LastUpdatedMarketPriceDate


  
from pms_shares

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSymbolFromCompanyName]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spGetSymbolFromCompanyName]
		@CompanyName varchar(500)
		AS
		BEGIN
		DECLARE @value varchar(500)
			SELECT @value = C_SYMBOL FROM COMPANY WHERE C_NAME = @CompanyName
			if @value is null
			 SELECT 'Nil' AS C_SYMBOL 
			else
			   SELECT @value AS C_SYMBOL
		END
GO
/****** Object:  StoredProcedure [dbo].[spInsertFund]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertFund]
		@FUND_CODE int,
		@FUND_NAME varchar(500),
		@FUND_SYMBOL varchar(500),
		@FUND_DESC varchar(max)
		AS
		BEGIN
INSERT INTO [dbo].[FUND]
           ([FUND_CODE]
		   ,[FUND_NAME]
           ,[FUND_SYMBOL]
           ,[FUND_DESC])
     VALUES
           (@FUND_CODE
		   ,@FUND_NAME
           ,@FUND_SYMBOL
           ,@FUND_DESC)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketSummary]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertMarketSummary] 
		@COMPANY_NAME varchar(300),
		@COMPANY_SYMBOL varchar(300),
		@COMPANY_LDCP float,
		@COMPANY_OPEN float,
		@COMPANY_HIGH float,
		@COMPANY_LOW float,
		@COMPANY_CURRENT float,
		@COMPANY_CHANGE float,
		@COMPANY_VOLUME float
	AS
BEGIN

INSERT INTO [dbo].[MarketSummary]
           ([MS_COMPANY_NAME]
           ,[MS_COMPANY_SYMBOL]
           ,[MS_COMPANY_LDCP]
           ,[MS_COMPANY_OPEN]
           ,[MS_COMPANY_HIGH]
           ,[MS_COMPANY_LOW]
           ,[MS_COMPANY_CURRENT]
           ,[MS_COMPANY_CHANGE]
           ,[MS_COMPANY_VOLUME])
     VALUES
           (@COMPANY_NAME
           ,@COMPANY_SYMBOL
           ,@COMPANY_LDCP
           ,@COMPANY_OPEN
           ,@COMPANY_HIGH 
           ,@COMPANY_LOW
           ,@COMPANY_CURRENT
           ,@COMPANY_CHANGE
           ,@COMPANY_VOLUME)
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketSummaryHistory]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertMarketSummaryHistory]
		@DATE datetime,
		@STATUS varchar(500),
		@SECTOR varchar(500),
		@COMPANY_NAME varchar(500),
		@COMPANY_SYMBOL varchar(500),
		@COMPANY_LDCP float,
		@COMPANY_OPEN float,
		@COMPANY_HIGH float,
		@COMPANY_LOW float,
		@COMPANY_CURRENT float,
		@COMPANY_CHANGE float,
		@COMPANY_VOLUME float
	AS
BEGIN
INSERT INTO [dbo].[MarketSummaryHistory]
           ([MS_DATE]
           ,[MS_STATUS]
           ,[MS_SECTOR]
           ,[MS_COMPANY_NAME]
           ,[MS_COMPANY_SYMBOL]
           ,[MS_COMPANY_LDCP]
           ,[MS_COMPANY_OPEN]
           ,[MS_COMPANY_HIGH]
           ,[MS_COMPANY_LOW]
           ,[MS_COMPANY_CURRENT]
           ,[MS_COMPANY_CHANGE]
           ,[MS_COMPANY_VOLUME])
     VALUES
           (@DATE
           ,@SECTOR
           ,@STATUS
           ,@COMPANY_NAME
           ,@COMPANY_SYMBOL
           ,@COMPANY_LDCP
           ,@COMPANY_OPEN
           ,@COMPANY_HIGH 
           ,@COMPANY_LOW
           ,@COMPANY_CURRENT
           ,@COMPANY_CHANGE
           ,@COMPANY_VOLUME)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketSummaryOverview]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertMarketSummaryOverview]
		@DATE DATETIME,
		@STATUS varchar(300),
		@VOLUME float,
		@VALUE float,
		@TRADES float
		AS
		BEGIN
INSERT INTO [dbo].[MarketSummaryOverview]
           ([MS_DATE]
           ,[MS_STATUS]
           ,[MS_VOLUME]
           ,[MS_VALUE]
           ,[MS_TRADES])
     VALUES
           (@DATE
           ,@STATUS
           ,@VOLUME
           ,@VALUE
           ,@TRADES)
END


GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketSummaryOverviewHistory]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertMarketSummaryOverviewHistory]
		@DATE DATETIME,
		@STATUS varchar(300),
		@VOLUME float,
		@VALUE float,
		@TRADES float
		AS
		BEGIN
INSERT INTO [dbo].[MarketSummaryOverviewHistory]
            ([MS_DATE]
           ,[MS_SYS_DATE]
           ,[MS_STATUS]
           ,[MS_VOLUME]
           ,[MS_VALUE]
           ,[MS_TRADES])
     VALUES
           (@DATE
           ,GETDATE()
           ,@STATUS
           ,@VOLUME
           ,@VALUE
           ,@TRADES)
END


GO
/****** Object:  StoredProcedure [dbo].[spIsGetCompanySymbolExist]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spIsGetCompanySymbolExist]
		@SYMBOL varchar(500)
		AS
BEGIN
	DECLARE @Count bigint;
	SELECT @Count = COUNT(*) FROM [Company] WHERE [C_SYMBOL] = @SYMBOL 
	SELECT @Count AS [FUND_EXIST]
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateMarketSummary]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateMarketSummary] 
		@COMPANY_NAME varchar(300),
		@COMPANY_SYMBOL varchar(300),
		@COMPANY_LDCP float,
		@COMPANY_OPEN float,
		@COMPANY_HIGH float,
		@COMPANY_LOW float,
		@COMPANY_CURRENT float,
		@COMPANY_CHANGE float,
		@COMPANY_VOLUME float
	AS
BEGIN
UPDATE [dbo].[MarketSummary]
   SET
      [MS_COMPANY_SYMBOL] = @COMPANY_SYMBOL
      ,[MS_COMPANY_LDCP] = @COMPANY_LDCP
      ,[MS_COMPANY_OPEN] = @COMPANY_OPEN
      ,[MS_COMPANY_HIGH] = @COMPANY_HIGH
      ,[MS_COMPANY_LOW] = @COMPANY_LOW 
      ,[MS_COMPANY_CURRENT] = @COMPANY_CURRENT
      ,[MS_COMPANY_CHANGE] = @COMPANY_CHANGE
      ,[MS_COMPANY_VOLUME] = @COMPANY_VOLUME
 WHERE [MS_COMPANY_NAME] = @COMPANY_NAME
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateMarketSummaryOverview]    Script Date: 9/11/2020 5:50:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateMarketSummaryOverview]
		@DATE DATETIME,
		@STATUS varchar(300),
		@VOLUME float,
		@VALUE float,
		@TRADES float
		AS
		BEGIN
UPDATE [dbo].[MarketSummaryOverview]
   SET [MS_DATE] = @DATE
      ,[MS_STATUS] = @STATUS
      ,[MS_VOLUME] = @VOLUME
      ,[MS_VALUE] = @VALUE
      ,[MS_TRADES] = @TRADES
 WHERE MS_ID = 1
END
GO
USE [master]
GO
ALTER DATABASE [PSXDataWarehouse] SET  READ_WRITE 
GO
