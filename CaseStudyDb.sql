USE [CaseStudy]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 11.03.2021 19:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusDesc] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11.03.2021 19:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTasks]    Script Date: 11.03.2021 19:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Priority] [int] NULL,
	[Description] [text] NULL,
	[StatusId] [int] NULL,
	[AppointedId] [int] NOT NULL,
	[AssignedId] [int] NOT NULL,
	[StartingDate] [date] NOT NULL,
	[DueDate] [date] NOT NULL,
	[ExpectedDueDate] [date] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserTasks] ADD  CONSTRAINT [DF_UserTasks_Title]  DEFAULT (N'Belirtilmedi') FOR [Title]
GO
ALTER TABLE [dbo].[UserTasks] ADD  CONSTRAINT [DF_UserTasks_Description]  DEFAULT ('Belirtilmedi') FOR [Description]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [fk_AppointedUser] FOREIGN KEY([AppointedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [fk_AppointedUser]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [fk_AssignedUser] FOREIGN KEY([AssignedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [fk_AssignedUser]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [fk_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [fk_Statuses]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [ck_Priority] CHECK  (([Priority]>=(0) AND [Priority]<=(100)))
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [ck_Priority]
GO
