USE [master]
GO
/****** Object:  Database [ProjectSem3]    Script Date: 01-Jun-20 8:38:50 AM ******/
CREATE DATABASE [ProjectSem3]
GO
USE [ProjectSem3]
GO
/****** Object:  Table [dbo].[AboutUs]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AboutUs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AboutName] [varchar](200) NOT NULL,
	[AboutContent] [varchar](max) NOT NULL,
	[AboutImage] [nvarchar](500) NOT NULL,
	[AboutHide] [bit] NOT NULL,
 CONSTRAINT [PK_AboutUs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donate]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DonateName] [varchar](200) NOT NULL,
	[DonateContent] [varchar](max) NOT NULL,
	[StartDay] [datetime] NOT NULL,
	[EndDay] [datetime] NOT NULL,
	[DonateStatus] [int] NOT NULL,
	[DonateHide] [bit] NOT NULL,
	[DonateDateCreate] [datetime] NOT NULL,
	[CateID] [int] NOT NULL,
	[TotalMoney] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Donate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Partner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartnerName] [varchar](200) NOT NULL,
	[PartnerImage] [nvarchar](500) NOT NULL,
	[PartnerLink] [varchar](200) NOT NULL,
	[PartnerActive] [bit] NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Program]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Program](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProName] [varchar](200) NOT NULL,
	[ProContent] [varchar](max) NOT NULL,
	[ProDateCreate] [datetime] NOT NULL,
	[ProHide] [bit] NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProgramImage]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramImage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProID] [int] NOT NULL,
	[ImgFileName] [nvarchar](500) NOT NULL,
	[ImgMain] [bit] NOT NULL,
 CONSTRAINT [PK_ProgramImage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeProgram]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeProgram](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_TypeProgram] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[UserGender] [bit] NOT NULL,
	[UserMail] [varchar](100) NOT NULL,
	[UserDOB] [date] NOT NULL,
	[UserPwd] [varchar](200) NOT NULL,
	[UserDateCreate] [datetime] NOT NULL,
	[UserActive] [bit] NOT NULL,
	[RoleID] [int] NOT NULL,
	[UserVolunteer] [bit] NOT NULL,
	[MoneyDonate] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDonate]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserDonate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DonateID] [int] NOT NULL,
	[TypeCard] [varchar](50) NOT NULL,
	[Money] [decimal](18, 0) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserDonate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[UserID] ASC,
	[DonateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserQuestion]    Script Date: 01-Jun-20 8:38:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserQuestion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[UserMail] [varchar](200) NOT NULL,
	[Title] [varchar](200) NOT NULL,
	[QuesContent] [varchar](500) NOT NULL,
	[QuesDateCreate] [datetime] NOT NULL,
	[QuesNew] [bit] NOT NULL,
	[AnswerContent] [varchar](500) NULL,
	[AnswerDateCreate] [datetime] NULL,
 CONSTRAINT [PK_UserQuestion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Donate] ADD  CONSTRAINT [DF_Donate_DonateDateCreate]  DEFAULT (getdate()) FOR [DonateDateCreate]
GO
ALTER TABLE [dbo].[Program] ADD  CONSTRAINT [DF_Program_ProDateCreate]  DEFAULT (getdate()) FOR [ProDateCreate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserDateCreate]  DEFAULT (getdate()) FOR [UserDateCreate]
GO
ALTER TABLE [dbo].[UserDonate] ADD  CONSTRAINT [DF_UserDonate_DateCreate]  DEFAULT (getdate()) FOR [DateCreate]
GO
ALTER TABLE [dbo].[UserQuestion] ADD  CONSTRAINT [DF_UserQuestion_QuesDateCreate_1]  DEFAULT (getdate()) FOR [QuesDateCreate]
GO
ALTER TABLE [dbo].[Donate]  WITH CHECK ADD  CONSTRAINT [FK_Donate_Category] FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Donate] CHECK CONSTRAINT [FK_Donate_Category]
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_TypeProgram] FOREIGN KEY([TypeID])
REFERENCES [dbo].[TypeProgram] ([ID])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_TypeProgram]
GO
ALTER TABLE [dbo].[ProgramImage]  WITH CHECK ADD  CONSTRAINT [FK_ProgramImage_Program] FOREIGN KEY([ProID])
REFERENCES [dbo].[Program] ([ID])
GO
ALTER TABLE [dbo].[ProgramImage] CHECK CONSTRAINT [FK_ProgramImage_Program]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[UserDonate]  WITH CHECK ADD  CONSTRAINT [FK_UserDonate_Donate] FOREIGN KEY([DonateID])
REFERENCES [dbo].[Donate] ([ID])
GO
ALTER TABLE [dbo].[UserDonate] CHECK CONSTRAINT [FK_UserDonate_Donate]
GO
ALTER TABLE [dbo].[UserDonate]  WITH CHECK ADD  CONSTRAINT [FK_UserDonate_User1] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserDonate] CHECK CONSTRAINT [FK_UserDonate_User1]
GO
insert into Role
values ('ADMIN'),('USER')
GO
insert into [User]
values ('Admin',1,'admin@mail.com','2000-02-29','21232F297A57A5A743894A0E4A801FC3','2020-05-24 10:52:24.483',1,1,0,0)
