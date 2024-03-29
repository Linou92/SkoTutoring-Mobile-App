USE [OnlineCourse]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[SessionId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Datetime] [datetime] NULL,
 CONSTRAINT [PK_dbo.Assignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Availability](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [int] NOT NULL,
	[EndTime] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Availability] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Level] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[LevelId] [int] NOT NULL,
	[TeacherId] [nvarchar](max) NULL,
	[StudentId] [nvarchar](max) NULL,
	[LangId] [int] NOT NULL,
	[Purpose] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[AvailabilityId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Notes] [text] NULL,
	[Student_UserId] [nvarchar](128) NOT NULL,
	[Teacher_UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[UserId] [nvarchar](128) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[LevelId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Student] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentLang]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentLang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](max) NULL,
	[LangId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.StudentLang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubj]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubj](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](max) NULL,
	[SubjId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.StudentSubj] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[UserId] [nvarchar](128) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CountryId] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Teacher] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherAvailability]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherAvailability](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [nvarchar](max) NULL,
	[AvailabilityId] [int] NOT NULL,
	[Teacher_UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TeacherAvailability] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherLang]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherLang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [nvarchar](max) NULL,
	[LangId] [int] NOT NULL,
	[Teacher_UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TeacherLang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherLevel]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [nvarchar](max) NULL,
	[LevelId] [int] NOT NULL,
	[Teacher_UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TeacherLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherSubj]    Script Date: 2/8/2021 10:32:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherSubj](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [nvarchar](max) NULL,
	[SubjectId] [int] NOT NULL,
	[Teacher_UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.TeacherSubj] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'74c8c33e-1793-4c07-b14b-eaaec9642acf', N'Student')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b3123042-03c9-4f14-8769-6241def7c79b', N'SuperAdmin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd80f1290-898d-4fc2-9884-018c8f282ed0', N'Teacher')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'da1b8a5a-36f5-4bf9-9c37-0aa01c8e3052', N'b3123042-03c9-4f14-8769-6241def7c79b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'286004df-7b5f-44e0-84ac-6240e6699558', N'd80f1290-898d-4fc2-9884-018c8f282ed0')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'286004df-7b5f-44e0-84ac-6240e6699558', N'Basel@courses.com', 1, N'AOXZ0bHK8Ud8gasJ5tc1V0nYEgyFNEll8tpw+NJ5Ha4NtEQbKdJftxvzRkcfmePwUg==', N'1f361fb6-26bc-4481-bbad-4192e3db2a35', NULL, 0, 0, NULL, 0, 0, N'Basel@courses.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'da1b8a5a-36f5-4bf9-9c37-0aa01c8e3052', N'Admin@courses.com', 0, N'AL1m4db8ju2bT1JNpdRcTcYpIdRzxrRDAdcExYIFd9cLo3u+iweuCJMaRw0vCX4I7w==', N'6aaa0bd3-8cff-4000-bed2-77e3207767a8', NULL, 0, 0, NULL, 0, 0, N'Admin@courses.com')
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'Sweden')
SET IDENTITY_INSERT [dbo].[Country] OFF
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([UserId], [Id], [FirstName], [LastName], [CountryId], [ImageUrl]) VALUES (N'286004df-7b5f-44e0-84ac-6240e6699558', 1, N'Basel', N'Mariam', 1, NULL)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Session_SessionId] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Session_SessionId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Availability_AvailabilityId] FOREIGN KEY([AvailabilityId])
REFERENCES [dbo].[Availability] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Availability_AvailabilityId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Language_LangId] FOREIGN KEY([LangId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Language_LangId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Level_LevelId] FOREIGN KEY([LevelId])
REFERENCES [dbo].[Level] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Level_LevelId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Student_Student_UserId] FOREIGN KEY([Student_UserId])
REFERENCES [dbo].[Student] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Student_Student_UserId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Subject_SubjectId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Teacher_Teacher_UserId] FOREIGN KEY([Teacher_UserId])
REFERENCES [dbo].[Teacher] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Teacher_Teacher_UserId]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Student_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_dbo.Student_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Student_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_dbo.Student_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Teacher_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_dbo.Teacher_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Teacher_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_dbo.Teacher_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[TeacherAvailability]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherAvailability_dbo.Availability_AvailabilityId] FOREIGN KEY([AvailabilityId])
REFERENCES [dbo].[Availability] ([Id])
GO
ALTER TABLE [dbo].[TeacherAvailability] CHECK CONSTRAINT [FK_dbo.TeacherAvailability_dbo.Availability_AvailabilityId]
GO
ALTER TABLE [dbo].[TeacherAvailability]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherAvailability_dbo.Teacher_Teacher_UserId] FOREIGN KEY([Teacher_UserId])
REFERENCES [dbo].[Teacher] ([UserId])
GO
ALTER TABLE [dbo].[TeacherAvailability] CHECK CONSTRAINT [FK_dbo.TeacherAvailability_dbo.Teacher_Teacher_UserId]
GO
ALTER TABLE [dbo].[TeacherLang]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherLang_dbo.Language_LangId] FOREIGN KEY([LangId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[TeacherLang] CHECK CONSTRAINT [FK_dbo.TeacherLang_dbo.Language_LangId]
GO
ALTER TABLE [dbo].[TeacherLang]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherLang_dbo.Teacher_Teacher_UserId] FOREIGN KEY([Teacher_UserId])
REFERENCES [dbo].[Teacher] ([UserId])
GO
ALTER TABLE [dbo].[TeacherLang] CHECK CONSTRAINT [FK_dbo.TeacherLang_dbo.Teacher_Teacher_UserId]
GO
ALTER TABLE [dbo].[TeacherLevel]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherLevel_dbo.Level_LevelId] FOREIGN KEY([LevelId])
REFERENCES [dbo].[Level] ([Id])
GO
ALTER TABLE [dbo].[TeacherLevel] CHECK CONSTRAINT [FK_dbo.TeacherLevel_dbo.Level_LevelId]
GO
ALTER TABLE [dbo].[TeacherLevel]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherLevel_dbo.Teacher_Teacher_UserId] FOREIGN KEY([Teacher_UserId])
REFERENCES [dbo].[Teacher] ([UserId])
GO
ALTER TABLE [dbo].[TeacherLevel] CHECK CONSTRAINT [FK_dbo.TeacherLevel_dbo.Teacher_Teacher_UserId]
GO
ALTER TABLE [dbo].[TeacherSubj]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherSubj_dbo.Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[TeacherSubj] CHECK CONSTRAINT [FK_dbo.TeacherSubj_dbo.Subject_SubjectId]
GO
ALTER TABLE [dbo].[TeacherSubj]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherSubj_dbo.Teacher_Teacher_UserId] FOREIGN KEY([Teacher_UserId])
REFERENCES [dbo].[Teacher] ([UserId])
GO
ALTER TABLE [dbo].[TeacherSubj] CHECK CONSTRAINT [FK_dbo.TeacherSubj_dbo.Teacher_Teacher_UserId]
GO
