////USE[SOM]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_ModifiedDate]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_ModifiedBy]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_CreatedDate]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_CreatedBy]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_IsApproved]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth]
////DROP CONSTRAINT[DF_StarOfTheMonth_IsDisplay]
////GO
////ALTER TABLE[dbo].[Nomination]
////DROP CONSTRAINT[DF__Nominatio__Modif__65370702]
////GO
////ALTER TABLE[dbo].[Nomination]
////DROP CONSTRAINT[DF__Nominatio__Modif__6442E2C9]
////GO
////ALTER TABLE[dbo].[Nomination]
////DROP CONSTRAINT[DF__Nominatio__Creat__634EBE90]
////GO
////ALTER TABLE[dbo].[Nomination]
////DROP CONSTRAINT[DF__Nominatio__Creat__625A9A57]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability]
////DROP CONSTRAINT[DF__Evaluator__Modif__7C1A6C5A]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability]
////DROP CONSTRAINT[DF__Evaluator__Modif__7B264821]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability]
////DROP CONSTRAINT[DF__Evaluator__Creat__7A3223E8]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability]
////DROP CONSTRAINT[DF__Evaluator__Creat__793DFFAF]
////GO
////ALTER TABLE[dbo].[Evaluation]
////DROP CONSTRAINT[DF__Evaluatio__Modif__6AEFE058]
////GO
////ALTER TABLE[dbo].[Evaluation]
////DROP CONSTRAINT[DF__Evaluatio__Modif__69FBBC1F]
////GO
////ALTER TABLE[dbo].[Evaluation]
////DROP CONSTRAINT[DF__Evaluatio__Creat__690797E6]
////GO
////ALTER TABLE[dbo].[Evaluation]
////DROP CONSTRAINT[DF__Evaluatio__Creat__681373AD]
////GO
////ALTER TABLE[dbo].[EmpMaster]
////DROP CONSTRAINT[DF_EmpMaster_UserRole]
////GO
////ALTER TABLE[dbo].[DepartmentHead]
////DROP CONSTRAINT[DF__Departmen__Modif__70A8B9AE]
////GO
////ALTER TABLE[dbo].[DepartmentHead]
////DROP CONSTRAINT[DF__Departmen__Modif__6FB49575]
////GO
////ALTER TABLE[dbo].[DepartmentHead]
////DROP CONSTRAINT[DF__Departmen__Creat__6EC0713C]
////GO
////ALTER TABLE[dbo].[DepartmentHead]
////DROP CONSTRAINT[DF__Departmen__Creat__6DCC4D03]
////GO
////ALTER TABLE[dbo].[AuditLog]
////DROP CONSTRAINT[DF__AuditLog__Modifi__76619304]
////GO
////ALTER TABLE[dbo].[AuditLog]
////DROP CONSTRAINT[DF__AuditLog__Modifi__756D6ECB]
////GO
////ALTER TABLE[dbo].[AuditLog]
////DROP CONSTRAINT[DF__AuditLog__Create__74794A92]
////GO
////ALTER TABLE[dbo].[AuditLog]
////DROP CONSTRAINT[DF__AuditLog__Create__73852659]
////GO
/////****** Object:  Table [dbo].[StarOfTheMonth]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[StarOfTheMonth]
////GO
/////****** Object:  Table [dbo].[Nomination]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[Nomination]
////GO
/////****** Object:  Table [dbo].[EvaluatorAvailability]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[EvaluatorAvailability]
////GO
/////****** Object:  Table [dbo].[Evaluation]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[Evaluation]
////GO
/////****** Object:  Table [dbo].[EnumAbbreviation]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[EnumAbbreviation]
////GO
/////****** Object:  Table [dbo].[EmpMaster]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[EmpMaster]
////GO
/////****** Object:  Table [dbo].[DepartmentHead]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[DepartmentHead]
////GO
/////****** Object:  Table [dbo].[AuditLog]    Script Date: 27-Feb-20 10:41:16 PM ******/
////DROP TABLE[dbo].[AuditLog]
////GO
/////****** Object:  Table [dbo].[AuditLog]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[AuditLog]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [NominationID] [bigint] NULL,
////	[EmployeeID] [bigint] NULL,
////	[DepartmentHeadID] [bigint] NULL,
////	[TQCHeadID] [bigint] NULL,
////	[EvaluatorID] [bigint] NULL,
////	[IsNewAlert] [bit] NULL,
////	[IsNotification] [bit] NULL,
////	[CurrentStatus] [int] NULL,
////	[CreatedBy] [bigint] NULL,
////	[CreatedDate] [nvarchar] (16) NULL,
////	[ModifiedBy] [bigint] NULL,
////	[ModifiedDate] [nvarchar] (16) NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[DepartmentHead]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[DepartmentHead]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [NominationID] [bigint] NULL,
////	[EmployeeID] [bigint] NULL,
////	[CommentsByDeptHead] [nvarchar] (800) NULL,
////	[Status] [int] NULL,
////	[IsActive] [bit] NULL,
////	[CreatedBy] [bigint] NULL,
////	[CreatedDate] [nvarchar] (16) NULL,
////	[ModifiedBy] [bigint] NULL,
////	[ModifiedDate] [nvarchar] (16) NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[EmpMaster]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[EmpMaster]
////(

////   [EmpID][int] IDENTITY(1,1) NOT NULL,

////  [EmployeeNumber] [varchar] (10) NOT NULL,

////   [EmployeeName] [nvarchar] (50) NOT NULL,

////    [Password] [nvarchar] (100) NULL,
////	[LastLogedIn] [datetime] NULL,
////	[Position] [nvarchar] (50) NULL,
////	[ImagePath]
////[nvarchar]
////(max) NULL,

////    [CompanyName] [nvarchar] (50) NULL,
////	[EmployeeEmail] [nvarchar] (100) NULL,
////	[Contact] [nvarchar] (50) NULL,
////	[IsActive] [bit] NULL,
////	[PositionsId] [int] NULL,
////	[ReportingPersonId] [int] NULL,
////	[PasswordChangedOn] [datetime] NULL,
////	[UserRole]
////[int]
////NOT NULL,

////    [Address] [nvarchar] (500) NULL,
////	[DateOfBirth] [date] NULL,
////	[DOJ] [date] NULL,
////	[Department] [nvarchar] (50) NULL,
//// CONSTRAINT[PK__EmpMaste__AF2DBA79CA6E0346] PRIMARY KEY CLUSTERED
////(
////   [EmpID] ASC,
////   [EmployeeNumber] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[EnumAbbreviation]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[EnumAbbreviation]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [ParentID] [bigint] NULL,
////	[ParentName] [nvarchar] (50) NULL,
////	[ParentDescription] [nvarchar] (100) NULL,
////	[ChildID] [bigint] NULL,
////	[ChildName] [nvarchar] (50) NULL,
////	[ChildDescription] [nvarchar] (100) NULL,
////	[IsActive] [bit] NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[Evaluation]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[Evaluation]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [EmployeeID] [bigint] NULL,
////	[EvaluatorID] [bigint] NULL,
////	[ReactiveIdeaDrivenBySituation] [int] NULL,
////	[BasedOnInstruction] [int] NULL,
////	[ProactiveIdeaGeneratedBySelf] [int] NULL,
////	[Delayed] [int] NULL,
////	[AsPerPlan] [int] NULL,
////	[AheadOfPlan] [int] NULL,
////	[FollowedUp] [int] NULL,
////	[Participated] [int] NULL,
////	[Implemented] [int] NULL,
////	[CoordiantionWithInTheDept] [int] NULL,
////	[CoordinationWithAnotherFunction] [int] NULL,
////	[CoordinationWithMultipleFunctions] [int] NULL,
////	[PreventionOfaFailure] [int] NULL,
////	[ImprovementFromCurrentSituation] [int] NULL,
////	[BreakthroughImprovement] [int] NULL,
////	[ScopeIdentified] [int] NULL,
////	[ImplementedPartially] [int] NULL,
////	[ImplementedInAllApplicableAreas] [int] NULL,
////	[TotalScore] [int] NULL,
////	[Status] [int] NULL,
////	[IsActive] [bit] NULL,
////	[CreatedBy] [bigint] NULL,
////	[CreatedDate] [nvarchar] (16) NULL,
////	[ModifiedBy] [bigint] NULL,
////	[ModifiedDate] [nvarchar] (16) NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[EvaluatorAvailability]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[EvaluatorAvailability]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [EvaluatorID] [bigint] NULL,
////	[Month] [nvarchar] (15) NULL,
////	[Year] [nvarchar] (4) NULL,
////	[IsActive] [bit] NULL,
////	[CreatedBy] [bigint] NULL,
////	[CreatedDate] [nvarchar] (16) NULL,
////	[ModifiedBy] [bigint] NULL,
////	[ModifiedDate] [nvarchar] (16) NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[Nomination]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[Nomination]
////(

////   [ID][bigint] IDENTITY(1,1) NOT NULL,

////  [EmployeeID] [bigint] NULL,
////	[StartDate] [nvarchar] (9) NULL,
////	[EndDate] [nvarchar] (9) NULL,
////	[Idea] [nvarchar] (200) NULL,
////	[PrimaryObjective] [nvarchar] (200) NULL,
////	[BriefDescription] [nvarchar] (800) NULL,
////	[Benefits] [nvarchar] (200) NULL,
////	[AreaOfInterest_1] [nvarchar] (200) NULL,
////	[ImplementationStatus_1] [nvarchar] (200) NULL,
////	[AreaOfInterest_2] [nvarchar] (200) NULL,
////	[ImplementationStatus_2] [nvarchar] (200) NULL,
////	[AreaOfInterest_3] [nvarchar] (200) NULL,
////	[ImplementationStatus_3] [nvarchar] (200) NULL,
////	[Acknowledgement] [nvarchar] (200) NULL,
////	[SubmittedMonth] [nvarchar] (15) NULL,
////	[SubmittedYear] [nvarchar] (4) NULL,
////	[Status] [int] NULL,
////	[IsActive] [bit] NULL,
////	[CreatedBy] [bigint] NULL,
////	[CreatedDate] [nvarchar] (16) NULL,
////	[ModifiedBy] [bigint] NULL,
////	[ModifiedDate] [nvarchar] (16) NULL,
////PRIMARY KEY CLUSTERED
////(
////    [ID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY]
////GO
/////****** Object:  Table [dbo].[StarOfTheMonth]    Script Date: 27-Feb-20 10:41:16 PM ******/
////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////CREATE TABLE[dbo].[StarOfTheMonth]
////(

////   [TransID][int] IDENTITY(1,1) NOT NULL,

////  [Month] [nvarchar] (50) NOT NULL,

////   [Description] [nvarchar]
////(max) NULL,

////   [EmpId] [int]
////NOT NULL,

////   [IsDisplay] [bit]
////NOT NULL,

////   [IsApproved] [bit]
////NOT NULL,

////   [ApprovedDate] [datetime] NULL,
////	[ApprovedBy] [int] NULL,
////	[CreatedBy]
////[int]
////NOT NULL,

////    [CreatedDate] [datetime]
////NOT NULL,

////    [ModifiedBy] [int]
////NOT NULL,

////    [ModifiedDate] [datetime]
////NOT NULL,
//// CONSTRAINT[PK__StarOfTheMonth__9E5DDB1C7D1F2802] PRIMARY KEY CLUSTERED
////(
////   [TransID] ASC
////)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
////) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
////GO
////SET IDENTITY_INSERT[dbo].[EmpMaster]
////ON
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(1, N'ADMIN', N'Intranet Admin', N'fc0iUkg331qk3V8HY6MWvQ==', CAST(N'2020-01-09T12:36:46.140' AS DateTime), N'None', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'csrAdmin@tvs.com', N'+91 8888888888', 1, 0, 7500003, CAST(N'2020-01-09T00:29:45.057' AS DateTime), 102, NULL, CAST(N'2019-11-13' AS Date), CAST(N'2019-10-01' AS Date), N'IT')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(2, N'USER', N'Intranet User', N'fc0iUkg331qk3V8HY6MWvQ==', CAST(N'2019-12-03T06:00:00.000' AS DateTime), N'None', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Csruser@tvs.com', N'900000000', 1, 0, 0, CAST(N'2020-01-09T00:30:03.927' AS DateTime), 101, NULL, CAST(N'2009-11-16' AS Date), CAST(N'2019-10-18' AS Date), N'IT')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(3, N'7500003', N'S K Rajesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'rajesh@tvsemerald.com', N'9677040627', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1983-12-29' AS Date), CAST(N'2013-10-14' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(4, N'7500006', N'S Vimal', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N's.vimal@tvsemerald.com', N'8754495742', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1987-02-14' AS Date), CAST(N'2013-10-14' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(5, N'7500007', N'S Palanikumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Stores', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N's.palanikumar@tvsemerald.com', N'9677042218', 1, 0, 989, NULL, 101, NULL, CAST(N'1979-08-28' AS Date), CAST(N'2013-10-14' AS Date), N'Purchase')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(6, N'7500008', N'R Thangaraj', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'r.thangaraj@tvsemerald.com', N'9677040624', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1980-05-07' AS Date), CAST(N'2013-10-14' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(7, N'7500009', N'S Muthukumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Maintenance', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'muthu@tvsemerald.com', N'9677040865', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1984-02-03' AS Date), CAST(N'2013-10-14' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(8, N'7500010', N'D Abiraman', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Accounts', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'abiraman@tvsemerald.com', N'9677125626', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1987-08-21' AS Date), CAST(N'2013-09-11' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(9, N'7500020', N'K Padmapriya', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Personal Secretary', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'padmapriya@tvsemerald.com', N'9791031957', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1973-04-03' AS Date), CAST(N'2013-09-05' AS Date), N'Admin')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(10, N'7500021', N'P Senthil Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Documentation', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'senthil@tvsemerald.com', N'8056241128', 1, 0, 7500045, NULL, 101, NULL, CAST(N'1985-08-20' AS Date), CAST(N'2013-11-13' AS Date), N'Legal')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(11, N'7500024', N'A R Pazhaniappan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - Safety', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'pazhaniappan@tvsemerald.com', N'9566222385', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1987-09-23' AS Date), CAST(N'2014-03-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(12, N'7500027', N'O M Kathavarayan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager - Purchase', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'omk@tvsemerald.com', N'9840772328', 1, 0, 989, NULL, 101, NULL, CAST(N'1968-07-13' AS Date), CAST(N'2014-07-16' AS Date), N'Purchase')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(13, N'7500034', N'A Prasad', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'prasad@tvsemerald.com', N'8754543784', 1, 0, 7500041, NULL, 101, NULL, CAST(N'1988-09-19' AS Date), CAST(N'2014-12-15' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(14, N'7500036', N'P G Shivshankar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Architecture', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'pgs@tvsemerald.com', N'9677252371', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1982-12-24' AS Date), CAST(N'2015-02-02' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(15, N'7500037', N'K Amuthan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'kamudhan@tvsemerald.com', N'9566228369', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1990-09-10' AS Date), CAST(N'2015-02-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(16, N'7500038', N'A Rajaraman', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive Vice President', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'arr@tvsemerald.com', N'9840617603', 1, 0, NULL, NULL, 101, NULL, CAST(N'1976-06-09' AS Date), CAST(N'2004-01-21' AS Date), N'EVP')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(17, N'7500040', N'S Maheswaran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'maheswaran@tvsemerald.com', N'9677064447', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1988-03-15' AS Date), CAST(N'2015-03-11' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(18, N'7500041', N'G Ramesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager- Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'ramesh@tvsemerald.com', N'9500126701', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1959-06-01' AS Date), CAST(N'2015-04-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(19, N'7500042', N'V Ganesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive Vice President', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'ganesh@tvsemerald.com', N'9840220885', 1, 0, NULL, NULL, 101, NULL, CAST(N'1966-07-21' AS Date), CAST(N'2015-05-18' AS Date), N'EVP')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(20, N'7500045', N'K K Viswanathan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant General Manager - Legal', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'kkv@tvsemerald.com', N'9600040753', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1975-03-01' AS Date), CAST(N'2015-07-01' AS Date), N'Legal')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(21, N'7500047', N'R Saravanaperumal', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'saravana@tvsemerald.com', N'7358767801', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1997-03-17' AS Date), CAST(N'2015-07-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(22, N'7500048', N'T Sathishkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Mechanical', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'tsathish@tvsemerald.com', N'7358767802', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1995-05-05' AS Date), CAST(N'2015-07-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(23, N'7500049', N'B Selvaramji', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'selvaramji@tvsemerald.com', N'7358767803', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1997-02-04' AS Date), CAST(N'2015-07-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(24, N'7500050', N'K Gomathi Eswaran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Electrical', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'gomathi@tvsemerald.com', N'7358767804', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1996-12-24' AS Date), CAST(N'2015-07-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(25, N'7500051', N'N Sureshkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'nsk@tvsemerald.com', N'7358767805', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1995-04-17' AS Date), CAST(N'2015-07-09' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(26, N'7500054', N'A R Arunkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Systems', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'arun@tvsemerald.com', N'9500159835', 1, 0, 1210, NULL, 102, NULL, CAST(N'1992-09-25' AS Date), CAST(N'2015-07-22' AS Date), N'IT')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(27, N'7500056', N'K Magesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manger - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'magesh@tvsemerald.com', N'7358040536', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1982-01-10' AS Date), CAST(N'2015-08-17' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(28, N'7500057', N'S M Sowmya', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Executive - Customer Care', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'sowmya@tvsemerald.com', N'7358034083 / 9677240593', 1, 0, 7500107, NULL, 101, NULL, CAST(N'1992-12-29' AS Date), CAST(N'2014-08-13' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(29, N'7500060', N'S Vijayaraghavan', N'fc0iUkg331qk3V8HY6MWvQ==', CAST(N'2019-12-05T19:29:33.917' AS DateTime), N'Executive - Administration', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'svr@tvsemerald.com', N'9940417231', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1984-04-16' AS Date), CAST(N'2015-12-30' AS Date), N'Admin')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(30, N'7500061', N'A John Rose', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Security', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'john@tvsemerald.com', N'9940144944', 1, 0, 7500131, CAST(N'2020-01-09T00:25:39.027' AS DateTime), 101, NULL, CAST(N'1957-04-29' AS Date), CAST(N'2016-02-03' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(31, N'7500063', N'E Tamil Selvan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'AGM - Construction Engineering', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'tamilselvan@tvsemerald.com', N'8754017552', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1974-06-21' AS Date), CAST(N'2016-04-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(32, N'7500066', N'Raja Nikhil Annavaram', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Business Planning', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'nikhil@tvsemerald.com', N'9500076243', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1991-08-22' AS Date), CAST(N'2016-05-02' AS Date), N'BPL')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(33, N'7500068', N'M Veeraragavan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - Land Acquisition', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'veera@tvsemerald.com', N'9500040196', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1978-03-10' AS Date), CAST(N'2016-05-06' AS Date), N'Land')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(34, N'7500071', N'M Jayaprabhu', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'mjp@tvsemerald.com', N'8056049060', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1982-12-18' AS Date), CAST(N'2016-07-04' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(35, N'7500072', N'L Sugumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Facilities', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'lsugumar@tvsemerald.com', N'7358040442', 1, 0, 7500107, NULL, 101, NULL, CAST(N'1987-12-12' AS Date), CAST(N'2016-11-02' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(36, N'7500076', N'N Akash Ram', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Akash@tvsemerald.com', N'7338858728', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1991-03-27' AS Date), CAST(N'2017-02-01' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(37, N'7500077', N'Rajitha R Rao', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Accounts', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'rajitha@tvsemerald.com', N'7358005253', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1992-03-28' AS Date), CAST(N'2017-03-20' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(38, N'7500078', N'S Jayabalaji', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Jayabalaji@tvsemerald.com', N'7358333263', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1994-05-09' AS Date), CAST(N'2017-06-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(39, N'7500081', N'P Boobalan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Boobalan@tvsemerald.com', N'7358081029', 1, 0, 7500041, NULL, 101, NULL, CAST(N'1995-06-03' AS Date), CAST(N'2017-06-09' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(40, N'7500082', N'R S Sanjay', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Sanjay@tvsemerald.com', N'7358081569', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1994-02-13' AS Date), CAST(N'2017-06-12' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(41, N'7500083', N'C Ayyanar Raja', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Engineer - Electrical', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Ayyanar@tvsemerald.com', N'7358081570', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1996-02-11' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(42, N'7500085', N'K Muthumani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Muthumani@tvsemerald.com', N'7358377951', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1998-06-04' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(43, N'7500086', N'V Moulesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Engineer - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Moulesh@tvsemerald.com', N'7358377952', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1998-03-12' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(44, N'7500087', N'S Boopathy', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Boopathy@tvsemerald.com', N'7358377953', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1998-11-20' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(45, N'7500088', N'G Manoj Sundar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Junior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Manoj@tvsemerald.com', N'7358377961', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1999-03-01' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(46, N'7500091', N'R Prakash', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Prakash@tvsemerald.com', NULL, 1, 0, 7500008, NULL, 101, NULL, CAST(N'1996-06-18' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(47, N'7500092', N'S Chandra Sekar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Chandrasekar@tvsemerald.com', N'7358377965', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1996-03-16' AS Date), CAST(N'2017-06-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(48, N'7500095', N'I Prasanna', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'i.prasanna@tvsemerald.com', N'7397222513', 1, 0, 7500199, NULL, 101, NULL, CAST(N'1994-06-03' AS Date), CAST(N'2017-06-08' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(49, N'7500096', N'S Govindaraju', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager - Projects', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'sgr@tvsemerald.com', N'7397222528', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1968-04-04' AS Date), CAST(N'2017-08-02' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(50, N'7500097', N'S Prabakaran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'prabakaran@tvsemerald.com', N'73959 58111', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1976-07-23' AS Date), CAST(N'2017-08-18' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(51, N'7500098', N'M S Sabarinath', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'sabarinath@tvsemerald.com', N'739739 9222', 1, 0, 7500166, NULL, 101, NULL, CAST(N'1988-08-29' AS Date), CAST(N'2017-09-25' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(52, N'7500099', N'C A Yesudas', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Head - Land Acquisition (Bangalore)', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Yesudas@tvsemerald.com', N'7338883382', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1969-07-22' AS Date), CAST(N'2017-11-01' AS Date), N'Land')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(53, N'7500101', N'P Vishal Anand', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Associate Vice President - Finance', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'vishal@tvsemerald.com', N'9384804321', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1978-11-02' AS Date), CAST(N'2017-11-15' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(54, N'7500102', N'R Ganesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'ganeshruthran@tvsemerald.com', N'9384806543', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1989-05-09' AS Date), CAST(N'2018-02-19' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(55, N'7500103', N'C Prabhu', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'cprabhu@tvsemerald.com', N'9940666828', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1983-05-09' AS Date), CAST(N'2018-02-19' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(56, N'7500105', N'P Deepak', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Accounts', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'deepak@tvsemerald.com', N'9840933841', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1990-07-20' AS Date), CAST(N'2018-03-28' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(57, N'7500107', N'Kartik Sundaravadanam', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Head - Facility Management', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Kartik@tvsemerald.com', N'96000 33789', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1982-05-15' AS Date), CAST(N'2018-04-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(58, N'7500109', N'K Senthil Rajan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - MEP', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'k.senthil@tvsemerald.com', N'7338989866', 1, 0, 989, NULL, 101, NULL, CAST(N'1976-01-12' AS Date), CAST(N'2018-05-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(59, N'7500110', N'Arjun Pandyanathan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Marketing', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'arjun@tvsemerald.com', N'73389 88166', 1, 0, 7500172, NULL, 101, NULL, CAST(N'1985-01-07' AS Date), CAST(N'2018-05-14' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(60, N'7500111', N'T Nirmal Balaji', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'nirmal@tvsemerald.com', N'73389 88366', 1, 0, 7500172, NULL, 101, NULL, CAST(N'1988-09-17' AS Date), CAST(N'2018-06-01' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(61, N'7500112', N'S Nishanth Varma', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Nishanth@tvsemerald.com', N'7358034073', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1996-08-22' AS Date), CAST(N'2018-06-11' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(62, N'7500113', N'C Praveen Kishore', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Praveen@tvsemerald.com', N'7358034074', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1997-02-15' AS Date), CAST(N'2018-06-11' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(63, N'7500114', N'A V Balaji', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'AV.Balaji@tvsemerald.com', N'7358034075', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1998-10-27' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(64, N'7500115', N'C Manikandan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Manikandan@tvsemerald.com', N'7358034076', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1999-01-29' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(65, N'7500116', N'S Muthuraj', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'S.Muthuraj@tvsemerald.com', N'7358034077', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1999-05-22' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(66, N'7500117', N'S Shyamkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Shyamkumar@tvsemerald.com', N'7358034078', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1999-06-21' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(67, N'7500118', N'K Mahendran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Mahendran@tvsemerald.com', N'7358034079', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1999-07-21' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(68, N'7500119', N'P R Balaji', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'PR.Balaji@tvsemerald.com', N'7358034080', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1999-08-22' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(69, N'7500120', N'M Muthuraj', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'M.Muthuraj@tvsemerald.com', N'7358034081', 1, 0, 7500008, NULL, 101, NULL, CAST(N'2000-03-08' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(70, N'7500121', N'J Mahalingaraja', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Mahalingaraja@tvsemerald.com', N'7358034082', 1, 0, 7500097, NULL, 101, NULL, CAST(N'2000-03-17' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(71, N'7500123', N'P Karthi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Karthi@tvsemerald.com', N'7358034084', 1, 0, 7500179, NULL, 101, NULL, CAST(N'2000-05-18' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(72, N'7500124', N'S Arunjunai Karthick', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Arunjunai@tvsemerald.com', N'7358034085', 1, 0, 7500063, NULL, 101, NULL, CAST(N'2000-05-30' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(73, N'7500125', N'M Balasubramani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Balasubramani@tvsemerald.com', N'7358034086', 1, 0, 7500063, NULL, 101, NULL, CAST(N'2000-06-15' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(74, N'7500126', N'R Vallinayagam', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Vice-President - Operations', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'rvn@tvsemerald.com', N'7338800556', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1964-06-05' AS Date), CAST(N'2018-06-21' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(75, N'7500127', N'S Mariappan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Purchase', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Mariappan@tvsemerald.com', N'7358034072', 1, 0, 989, NULL, 101, NULL, CAST(N'1984-12-25' AS Date), CAST(N'2018-07-09' AS Date), N'Purchase')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(76, N'7500128', N'S Dinesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Dinesh@tvsemerald.com', N'7358034071', 1, 0, 7500199, NULL, 101, NULL, CAST(N'1992-07-05' AS Date), CAST(N'2018-08-02' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(77, N'7500129', N'N Srikanth', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Srikanth@tvsemerald.com', N'9384666112', 1, 0, 7500132, NULL, 101, NULL, CAST(N'1987-05-30' AS Date), CAST(N'2018-08-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(78, N'7500130', N'R Saravanan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Saravanan@tvsemerald.com', N'9384666113', 1, 0, 7500132, NULL, 101, NULL, CAST(N'1982-10-31' AS Date), CAST(N'2018-08-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(79, N'7500131', N'V A Ravikumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Director - Security&Intelligence', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'ravikumar@tvsemerald.com', N'7550286444', 1, 0, NULL, NULL, 101, NULL, CAST(N'1955-12-04' AS Date), CAST(N'2018-08-20' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(80, N'7500132', N'N Kalaivani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Kalaivani@tvsemerald.com', N'7550285999', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1980-04-24' AS Date), CAST(N'2018-08-20' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(81, N'7500133', N'S Prabanjani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Prabanjani@tvsemerald.com', N'7338885634', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1986-07-12' AS Date), CAST(N'2018-08-23' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(82, N'7500134', N'M Vignesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'vignesh@tvsemerald.comm', N'9384898482', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1998-08-24' AS Date), CAST(N'2018-08-27' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(83, N'7500135', N'M Vikram', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'vikram@tvsemerald.com', N'9384898480', 1, 0, 7500096, NULL, 101, NULL, CAST(N'2000-01-15' AS Date), CAST(N'2018-09-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(84, N'7500136', N'G Gayathri', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Gayathri@tvsemerald.com', N'9384898481', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1978-04-02' AS Date), CAST(N'2018-09-14' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(85, N'7500137', N'M Santhosh Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'santhosh@tvsemerald.com', N'9384898479', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1997-07-13' AS Date), CAST(N'2018-09-14' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(86, N'7500138', N'G Muthukumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Diploma Engineer Trainee', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'muthukumar@tvsemerald.com', N'8056000443', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1996-03-30' AS Date), CAST(N'2018-10-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(87, N'7500139', N'Kintali Kiran Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Finance', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Kiran@tvsemerald.com', N'9384898475', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1995-06-03' AS Date), CAST(N'2018-10-03' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(88, N'7500140', N'D Karthick', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Karthick@tvsemerald.com', N'9384898476', 1, 0, 7500166, NULL, 101, NULL, CAST(N'1988-03-24' AS Date), CAST(N'2018-10-03' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(89, N'7500141', N'D Sankar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Security Officer', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', NULL, N'9384898476', 1, 0, 7500131, NULL, 101, NULL, CAST(N'1966-06-16' AS Date), CAST(N'2018-10-11' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(90, N'7500142', N'R Sunil', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Finance', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'sunil@tvsemerald.com', N'93848 83233', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1996-07-12' AS Date), CAST(N'2018-10-15' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(91, N'7500143', N'P Mohanraj', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'mohanraj@tvsemerald.com', N'93848 83244', 1, 0, 7500102, NULL, 101, NULL, CAST(N'1990-11-11' AS Date), CAST(N'2018-01-10' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(92, N'7500144', N'G Murugan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - MEP', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'murugan@tvsemerald.com', N'7397445511', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1977-12-09' AS Date), CAST(N'2018-10-22' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(93, N'7500145', N'V Kishorekumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'kishorekumar@tvsemerald.com', N'7397225588', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1992-11-25' AS Date), CAST(N'2018-10-22' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(94, N'7500146', N'M Ashwin Prathap', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'ashwin@tvsemerald.com', N'7397396004', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1994-09-29' AS Date), CAST(N'2018-11-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(95, N'7500147', N'S Karthi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - MEP', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'skarthi@tvsemerald.com', N'8056000883', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1990-12-21' AS Date), CAST(N'2018-11-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(96, N'7500148', N'U Saran Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - MEP', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'saran@tvsemerald.com', N'7397222538', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1988-01-29' AS Date), CAST(N'2018-11-12' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(97, N'7500149', N'R Ramesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - MEP', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'R.Ramesh@tvsemerald.com', N'7338825888', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1991-03-24' AS Date), CAST(N'2018-11-16' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(98, N'7500150', N'S Ramesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Planning', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'S.Ramesh@tvsemerald.com', N'7338826444', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1982-05-05' AS Date), CAST(N'2018-12-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(99, N'7500151', N'Basavaraj Dasamani G', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Basavaraj@tvsemerald.com', N'7338801555', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1987-05-05' AS Date), CAST(N'2019-01-21' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(100, N'7500152', N'Mariya Lucy', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager -  HRD', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'mariya@tvsemerald.com', N'9566192060', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1974-09-22' AS Date), CAST(N'2019-01-23' AS Date), N'HR')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(101, N'7500153', N'N Murugadhas', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Security', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Murugadhas@tvsemerald.com', N'7338801166', 1, 0, 7500131, NULL, 101, NULL, CAST(N'1960-06-17' AS Date), CAST(N'2019-01-23' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(102, N'7500154', N'P Vijaya Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager - EHS', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'PVK@tvsemerald.com', N'7550022757', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1973-02-26' AS Date), CAST(N'2019-02-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(103, N'7500155', N'P Saravanan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Finance', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N' P.Saravanan@tvsemerald.com', N'6385172345', 1, 0, 7500101, NULL, 101, NULL, CAST(N'1986-05-07' AS Date), CAST(N'2019-02-01' AS Date), N'Finance')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(104, N'7500156', N'A Venkatesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Quantity Surveyor', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Venkatesh@tvsemerald.com', N'7397362345', 1, 0, 989, NULL, 101, NULL, CAST(N'1992-07-07' AS Date), CAST(N'2019-02-04' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(105, N'7500157', N'Sahul Hameed', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Projects', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N' Sahul@tvsemerald.com', N'7358680999', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1982-01-05' AS Date), CAST(N'2019-02-18' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(106, N'7500158', N'D Selvakumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Architecture', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Selvakumar@tvsemerald.com', N'6385161234', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1979-08-11' AS Date), CAST(N'2019-02-18' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(107, N'7500160', N'P R Anjith Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Anjith@tvsemerald.com', N'7397492686', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1995-01-09' AS Date), CAST(N'2019-02-18' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(108, N'7500161', N'G Dhandabani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Dhandabani@tvsemerald.com', N'7397492684', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1980-04-24' AS Date), CAST(N'2019-02-20' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(109, N'7500162', N'D Arunkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N' Arunkumar@tvsemerald.com', N'7397492685', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1992-07-11' AS Date), CAST(N'2019-03-04' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(110, N'7500163', N'G Vinayagamani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - QS & Planning', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Vinayagamani@tvsemerald.com', N'7397492683', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1983-05-25' AS Date), CAST(N'2019-03-04' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(111, N'7500164', N'D Pravesh Bhaiya', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Pravesh@tvsemerald.com', N'7397492687', 1, 0, 7500102, NULL, 101, NULL, CAST(N'1988-05-26' AS Date), CAST(N'2019-03-04' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(112, N'7500165', N'S Karthikeyan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - CRM', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N' skarthikeyan@tvsemerald.com', N'9840487265', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1982-10-08' AS Date), CAST(N'2019-03-04' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(113, N'7500166', N'P S Jothiganesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Manager - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Jothiganesh@tvsemerald.com', N'9384046444', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1984-03-18' AS Date), CAST(N'2019-03-06' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(114, N'7500167', N'P Rajasekaran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - QA/QC', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Rajasekaran@tvsemerald.com', N'9384046600', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1989-02-22' AS Date), CAST(N'2019-03-18' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(115, N'7500168', N'R Raghul Vasanthan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Trainee', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', NULL, N'7550069222', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1995-12-11' AS Date), CAST(N'2019-03-20' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(116, N'7500169', N'R Thiruvikramanan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Safety', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Thiruvikramanan@tvsemerald.com', N'6385125544', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1990-04-12' AS Date), CAST(N'2019-03-20' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(117, N'7500170', N'S Lakshmi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - CRM', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Lakshmi@tvsemerald.com', N'9840502381', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1983-09-09' AS Date), CAST(N'2019-04-01' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(118, N'7500171', N'G Saravanan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - QA/QC', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'GSaravanan@tvsemerald.com', N'6385125566', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1984-10-12' AS Date), CAST(N'2019-04-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(119, N'7500172', N'Badri Narayan B Davay', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager - Marketing', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Badri@tvsemerald.com', N'9600054873', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1978-08-15' AS Date), CAST(N'2019-04-01' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(120, N'7500173', N'G Nanthakumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer-Facilities', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Nanthakumar@tvsemerald.com', N'9384052481', 1, 0, 7500107, NULL, 101, NULL, CAST(N'1990-06-10' AS Date), CAST(N'2019-04-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(121, N'7500174', N'V T Sridhar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Data Scientist', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Sridhar@tvsemerald.com', N'9384052482', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1992-09-16' AS Date), CAST(N'2011-04-18' AS Date), N'Analytics')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(122, N'7500175', N'V Pavan Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant General Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Pavan@tvsemerald.com', N'9384052483', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1977-04-24' AS Date), CAST(N'2019-04-08' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(123, N'7500176', N'D Soundarya', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Trainee', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Soundarya@tvsemerald.com', N'8667603469', 1, 0, 7500152, NULL, 101, NULL, CAST(N'1996-01-12' AS Date), CAST(N'2019-04-08' AS Date), N'HR')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(124, N'7500177', N'S Vikas', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Vikas@tvsemerald.com', N'9384052484', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1989-09-20' AS Date), CAST(N'2019-04-10' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(125, N'7500178', N'N Sivanantham', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - Facilities', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Sivanantham@tvsemerald.com', N'9384052485', 1, 0, 7500107, NULL, 101, NULL, CAST(N'1977-05-20' AS Date), CAST(N'2019-04-15' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(126, N'7500179', N'S Velumani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Projects', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Velumani@tvsemerald.com', N'9384052486', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1980-06-10' AS Date), CAST(N'2019-04-15' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(127, N'7500180', N'P Subramanya', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy General Manager - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Subramanya@tvsemerald.com', N'7397700959', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1968-06-12' AS Date), CAST(N'2019-04-15' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(128, N'7500181', N'A Rajkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Rajkumar@tvsemerald.com', N'9176339938', 1, 0, 7500179, NULL, 101, NULL, CAST(N'1990-10-31' AS Date), CAST(N'2019-04-15' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(129, N'7500182', N'S Murugesan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Murugesan@tvsemerald.com', N'9384061512', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1986-07-13' AS Date), CAST(N'2019-04-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(130, N'7500183', N'B Raghavan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Raghavan@tvsemerald.com', N'9384061515', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1992-12-19' AS Date), CAST(N'2019-05-08' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(131, N'7500184', N'C Arunkumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'c.arun@tvsemerald.com', N'9384061513', 1, 0, 7500157, NULL, 101, NULL, CAST(N'1991-02-21' AS Date), CAST(N'2019-05-16' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(132, N'7500185', N'K Kamalakannan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - QA/QC', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Kamalakannan@tvsemerald.com', N'9384061514', 1, 0, 7500097, NULL, 101, NULL, CAST(N'1995-03-11' AS Date), CAST(N'2019-05-22' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(133, N'7500186', N'Anitha Balakrishnan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Contract', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Anitha@tvsemerald.com', N'9384061516', 1, 0, 989, NULL, 101, NULL, CAST(N'1978-10-02' AS Date), CAST(N'2019-05-22' AS Date), N'Purchase')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(134, N'7500187', N'P Nishanthi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N' Nishanthi@tvsemerald.com', N'9384091797', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1991-06-06' AS Date), CAST(N'2019-05-22' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(135, N'7500188', N'T Dilli Babu', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - MEP', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Dillibabu@tvsemerald.com', N'9384061517', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1990-05-29' AS Date), CAST(N'2019-05-29' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(136, N'7500189', N'Benleon Joshua', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Benleon@tvsemerald.com', N'9384091796', 1, 0, 7500157, NULL, 101, NULL, CAST(N'1993-06-07' AS Date), CAST(N'2019-06-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(137, N'7500190', N'V Prakash', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Quantity Surveyor', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'V.Prakash@tvsemerald.com', N'9384091795', 1, 0, NULL, NULL, 101, NULL, CAST(N'1986-11-30' AS Date), CAST(N'2019-06-05' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(138, N'7500191', N'A Dhanakodi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Quantity Surveyor', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Dhanakodi@tvsemerald.com', N'9384091794', 1, 0, 7500008, CAST(N'2020-01-09T00:24:37.283' AS DateTime), 101, NULL, CAST(N'1981-07-10' AS Date), CAST(N'2019-06-12' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(139, N'7500192', N'A Mohanakrishnan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Mohanakrishnan@tvsemerald.com', N'9384091793', 1, 0, 7500175, NULL, 101, NULL, CAST(N'1985-10-10' AS Date), CAST(N'2019-06-19' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(140, N'7500193', N'B Gowthamraj', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Gowthamraj@tvsemerald.com', N'8553233783', 1, 0, 7500180, NULL, 101, NULL, CAST(N'1987-02-12' AS Date), CAST(N'2019-06-19' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(141, N'7500194', N'V Bharathi Raja', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Bharathiraja@tvsemerald.com', N'9384091791', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1992-06-08' AS Date), CAST(N'2019-06-20' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(142, N'7500195', N'Prakash G', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Billing', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'G.Prakash@tvsemerald.com', N'9384091790', 1, 0, 989, NULL, 101, NULL, CAST(N'1979-12-13' AS Date), CAST(N'2019-06-24' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(144, N'7500197', N'N Jeganathan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Quantity Surveyor', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N' Jeganathan@tvsemerald.com', N'9384091798', 1, 0, 7500063, NULL, 101, NULL, CAST(N'1990-10-25' AS Date), CAST(N'2019-06-26' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(145, N'7500198', N'Dinesh Kumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Dineshkumar@tvsemerald.com', N'9384091788', 1, 0, 7500166, NULL, 101, NULL, CAST(N'1986-01-06' AS Date), CAST(N'2019-07-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(146, N'7500199', N'Arunan Shankar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Arunan@tvsemerald.com', N'9384050317', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1985-04-14' AS Date), CAST(N'2019-07-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(147, N'7500200', N'D Durai Murugan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Projects', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Durai@tvsemerald.com', N'9384050318', 1, 0, 7500096, NULL, 101, NULL, CAST(N'1983-06-24' AS Date), CAST(N'2019-07-03' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(148, N'7500201', N'Prasannakumar Bhat', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Projects', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Prasannakumar@tvsemerald.com', N'9384050319', 1, 0, 7500180, NULL, 101, NULL, CAST(N'1987-08-24' AS Date), CAST(N'2019-07-10' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(149, N'7500202', N'T S Chandrika', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Chandrika@tvsemerald.com', N'9384050320', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1983-12-09' AS Date), CAST(N'2019-07-10' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(150, N'7500203', N'A P Selvakumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant General Manager - QA & QC', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'ap.selvakumar@tvsemerald.com', N'9384050314', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1982-06-06' AS Date), CAST(N'2019-07-10' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(151, N'7500204', N'R Rajeswari', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant General Manager - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Rajeswari@tvsemerald.com', N'9384050315', 1, 0, 7500038, NULL, 101, NULL, CAST(N'1978-06-27' AS Date), CAST(N'2019-07-10' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(152, N'7500205', N'E Prasanth', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Prasanth@tvsemerald.com', N'7904430068', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1994-03-28' AS Date), CAST(N'2019-07-11' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(153, N'7500206', N'A Udayakumar', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Engineer - Civil', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Udayakumar@tvsemerald.com', N'9940195235', 1, 0, 7500008, NULL, 101, NULL, CAST(N'1986-04-21' AS Date), CAST(N'2019-07-15' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(154, N'7500207', N'P S Ramkishore', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Safety', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Ramkishore@tvsemerald.com', N'7708728327 ', 1, 0, 7500180, NULL, 101, NULL, CAST(N'1991-09-20' AS Date), CAST(N'2019-07-17' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(155, N'7500209', N'S Vivek', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Engineer - Civil', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N's.vivek@tvsemerald.com', N'9742826698', 1, 0, 7500180, NULL, 101, NULL, CAST(N'1993-03-01' AS Date), CAST(N'2019-07-22' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(156, N'7500210', N'Kannan Mahalingam', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant General Manager - Planning', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Kannan@tvsemerald.com', N'93840 63841 ', 1, 0, 7500126, NULL, 101, NULL, CAST(N'1979-12-14' AS Date), CAST(N'2019-07-22' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(157, N'7500211', N'R Premnath', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Digital Marketing', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Premnath@tvsemerald.com', N'9384063842', 1, 0, 7500172, NULL, 101, NULL, CAST(N'1986-08-08' AS Date), CAST(N'2019-07-25' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(158, N'7500213', N'S Karthik', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'S.Karthik@tvsemerald.com', N'9384050316', 1, 0, 7500041, NULL, 101, NULL, CAST(N'1989-01-10' AS Date), CAST(N'2019-08-05' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(159, N'7500214', N'G Natarajan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - Facilities', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Natarajan@tvsemerald.com', N'9384063843', 1, 0, 7500107, NULL, 101, NULL, CAST(N'1992-05-20' AS Date), CAST(N'2019-08-05' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(160, N'7500215', N'C Karthikeyan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - QS & Planning', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'ckarthikeyan@tvsemerald.com', N'9443824597', 1, 0, 7500210, NULL, 101, NULL, CAST(N'1985-07-09' AS Date), CAST(N'2019-11-01' AS Date), N'Operations')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(161, N'7500216', N'C K Karpagavalli', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - CRM', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Karpagavalli@tvsemerald.com', N'8056230222', 1, 0, 7500204, NULL, 101, NULL, CAST(N'1993-01-22' AS Date), CAST(N'2019-11-01' AS Date), N'CRM')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(162, N'7500217', N'S Vignesh', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'svignesh@tvsemerald.com', N'8056210555', 1, 0, 7500132, NULL, 101, NULL, CAST(N'1986-09-10' AS Date), CAST(N'2019-11-01' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(163, N'7500218', N'M Stalin', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Stalin@tvsemerald.com', N'8056250888', 1, 0, 7500175, NULL, 101, NULL, CAST(N'1987-07-16' AS Date), CAST(N'2019-11-04' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(164, N'7500219', N'S Sathish', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'SSathish@tvsemerald.com', N'8056186555', 1, 0, 7500102, NULL, 101, NULL, CAST(N'1982-09-08' AS Date), CAST(N'2019-11-13' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(165, N'7500220', N'M G Balasivamurugan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Balasivamurugan@tvsemerald.com', N'8754417890', 1, 0, 7500102, NULL, 101, NULL, CAST(N'1990-08-04' AS Date), CAST(N'2019-11-14' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(166, N'7500221', N'S Deepak Pandian', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Deputy Manager - Sales', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'SDeepak@tvsmemerald.com', N'8754427890', 1, 0, 7500041, NULL, 101, NULL, CAST(N'1988-10-24' AS Date), CAST(N'2019-11-22' AS Date), N'Sales')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(167, N'7500222', N'A Ganesan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - Planner', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'A.Ganesan@tvsemerald.com', N'9841848575', 1, 0, 7500172, NULL, 101, NULL, CAST(N'1983-06-05' AS Date), CAST(N'2019-11-27' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(168, N'7500223', N'F Glyton', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Assistant Manager - Graphic Design', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', N'Glyton@tvsemerald.com', N'9790719944', 1, 0, 7500172, NULL, 101, NULL, CAST(N'1990-10-06' AS Date), CAST(N'2019-11-27' AS Date), N'Marketing')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(169, N'9500001', N'V Deenadayalan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Consultant', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'v.deenadayalan@tvsemerald.com', N'8056067860', 1, 0, 7500042, NULL, 101, NULL, CAST(N'1952-06-26' AS Date), CAST(N'2011-02-16' AS Date), N'Land')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(170, N'9500002', N'K Ganesan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Security Supervisor', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', NULL, N'8754420812', 1, 0, 7500061, NULL, 101, NULL, CAST(N'1955-05-29' AS Date), CAST(N'2013-10-01' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(171, N'9500006', N'S Palani', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Security Supervisor', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', NULL, N'9840371878', 1, 0, 7500153, NULL, 101, NULL, CAST(N'1958-06-07' AS Date), CAST(N'2018-12-01' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(172, N'9500007', N'S Dinesh Babu', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Executive - HRD', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'Dineshbabu@tvsemerald.com', N'9384052487', 1, 0, 7500152, NULL, 101, NULL, CAST(N'1991-05-26' AS Date), CAST(N'2019-04-15' AS Date), N'HR')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(173, N'9500008', N'S Gunasekaran', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Security Supervisor', N'/Data/User/user_20191014021755.png', N'TVS EMERALD', NULL, N'9840212433', 1, 0, 7500153, NULL, 101, NULL, CAST(N'1958-05-01' AS Date), CAST(N'2019-07-01' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(174, N'9500009', N'G Mohan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Security Supervisor', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', NULL, N'9884304656', 1, 0, 7500153, NULL, 101, NULL, CAST(N'1958-05-15' AS Date), CAST(N'2019-07-01' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(175, N'9500010', N'R Revathan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Security Supervisor', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', NULL, N'9598140205', 1, 0, 7500061, NULL, 101, NULL, CAST(N'1960-12-21' AS Date), CAST(N'2019-09-01' AS Date), N'Security')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(176, N'512254', N'D Kalpana Devi', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Senior Executive - IT', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'd.kalpanadevi@scl.co.in', NULL, 1, 0, 1210, NULL, 102, NULL, CAST(N'1991-10-02' AS Date), CAST(N'2016-03-10' AS Date), N'IT Hardware')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(177, N'989', N'W Wilfred Jerome', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Head - TQC & Purchase', N'/Data/User/user_20191014021728.png', N'TVS EMERALD', N'Jerome@tvsmotor.com', NULL, 1, 0, NULL, NULL, 101, NULL, CAST(N'1972-10-17' AS Date), CAST(N'2017-01-04' AS Date), N'Purchase')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(178, N'1210', N'N Vasudevan', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'Head - IT', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'N.Vasudevan@tvsmotor.com', NULL, 1, 0, NULL, NULL, 102, NULL, CAST(N'1961-10-21' AS Date), CAST(N'2019-01-06' AS Date), N'IT Hardware')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(179, N'511071', N'K Suseel', N'fc0iUkg331qk3V8HY6MWvQ==', CAST(N'2019-12-04T12:24:20.413' AS DateTime), N'SCL', N'/Data/User/user_20191014021739.png', N'TVS EMERALD', N'KS.Suseel@scl.co.in', NULL, 1, 0, NULL, CAST(N'2019-12-04T12:24:32.623' AS DateTime), 102, NULL, CAST(N'1961-10-21' AS Date), CAST(N'2019-01-06' AS Date), N'IT Hardware')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(180, N'test', N'test', N'123456', NULL, NULL, NULL, NULL, N'test@t.com', N'7500003', 1, NULL, 0, NULL, 101, NULL, CAST(N'2018-12-12' AS Date), CAST(N'2018-12-12' AS Date), N'test')
////GO
////INSERT[dbo].[EmpMaster] ([EmpID], [EmployeeNumber], [EmployeeName], [Password], [LastLogedIn], [Position], [ImagePath], [CompanyName], [EmployeeEmail], [Contact], [IsActive], [PositionsId], [ReportingPersonId], [PasswordChangedOn], [UserRole], [Address], [DateOfBirth], [DOJ], [Department]) VALUES(181, N'test123', N'test', N'fc0iUkg331qk3V8HY6MWvQ==', NULL, N'test', NULL, NULL, N'test@t.com', N'test', 1, NULL, 7500007, NULL, 101, NULL, CAST(N'2020-01-01' AS Date), CAST(N'2020-01-24' AS Date), N'test')
////GO
////SET IDENTITY_INSERT[dbo].[EmpMaster]
////OFF
////GO
////SET IDENTITY_INSERT[dbo].[EnumAbbreviation]
////ON
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(1, 1000, N'EmployeeRole', N'Employee Role', 1001, N'Employee', N'Employee', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(2, 1000, N'EmployeeRole', N'Employee Role', 1002, N'DepartmentHead', N'Department Head', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(3, 1000, N'EmployeeRole', N'Employee Role', 1003, N'TQCHead', N'TQC Head', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(4, 1000, N'EmployeeRole', N'Employee Role', 1004, N'Evalutors', N'Evalutors', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(5, 1000, N'EmployeeRole', N'Employee Role', 1005, N'Admin', N'Admin', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(6, 2000, N'NominationStatus', N'Nomination Status', 2001, N'Employee_Save', N'Employee Save', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(7, 2000, N'NominationStatus', N'Nomination Status', 2002, N'Employee_To_DH', N'Employee Assign DH', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(8, 2000, N'NominationStatus', N'Nomination Status', 2003, N'DH_TO_EmployeeClarication', N'DH Assign EmployeeClarication', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(9, 2000, N'NominationStatus', N'Nomination Status', 2004, N'DH_To_TQC', N'DH Assign TQC', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(10, 2000, N'NominationStatus', N'Nomination Status', 2005, N'TQC_To_Evaluators', N'TQC Assign Evaluators', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(11, 2000, N'NominationStatus', N'Nomination Status', 2006, N'Evaluators_Save', N'Evaluators Save', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(12, 2000, N'NominationStatus', N'Nomination Status', 2007, N'Evaluators_To_TQC', N'Evaluators Assign TQC', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(13, 2000, N'NominationStatus', N'Nomination Status', 2008, N'HoD_Reject', N'DH Reject', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(14, 2000, N'NominationStatus', N'Nomination Status', 2009, N'TQC_Reject', N'TQC Reject', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(15, 2000, N'NominationStatus', N'Nomination Status', 2010, N'Evaluator_Reject', N'Evaluator Reject', 1)
////GO
////INSERT[dbo].[EnumAbbreviation]
////([ID], [ParentID], [ParentName], [ParentDescription], [ChildID], [ChildName], [ChildDescription], [IsActive]) VALUES(16, 2000, N'NominationStatus', N'Nomination Status', 2011, N'Completed', N'Completed', 1)
////GO
////SET IDENTITY_INSERT[dbo].[EnumAbbreviation]
////OFF
////GO
////SET IDENTITY_INSERT[dbo].[StarOfTheMonth]
////ON
////GO
////INSERT[dbo].[StarOfTheMonth] ([TransID], [Month], [Description], [EmpId], [IsDisplay], [IsApproved], [ApprovedDate], [ApprovedBy], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES(5, N'10', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate posuere...', 3, 1, 0, NULL, 0, 2, CAST(N'2019-10-13T13:45:51.740' AS DateTime), 2, CAST(N'2019-11-12T13:04:50.690' AS DateTime))
////GO
////INSERT[dbo].[StarOfTheMonth] ([TransID], [Month], [Description], [EmpId], [IsDisplay], [IsApproved], [ApprovedDate], [ApprovedBy], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES(6, N'10', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate posuere...', 2, 1, 0, NULL, 0, 2, CAST(N'2019-10-15T06:44:50.480' AS DateTime), 2, CAST(N'2019-11-12T13:05:07.783' AS DateTime))
////GO
////SET IDENTITY_INSERT[dbo].[StarOfTheMonth]
////OFF
////GO
////ALTER TABLE[dbo].[AuditLog] ADD DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[AuditLog] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[AuditLog] ADD DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[AuditLog] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[ModifiedDate]
////GO
////ALTER TABLE[dbo].[DepartmentHead] ADD DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[DepartmentHead] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[DepartmentHead] ADD DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[DepartmentHead] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[ModifiedDate]
////GO
////ALTER TABLE[dbo].[EmpMaster] ADD CONSTRAINT[DF_EmpMaster_UserRole]  DEFAULT((101)) FOR[UserRole]
////GO
////ALTER TABLE[dbo].[Evaluation] ADD DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[Evaluation] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[Evaluation] ADD DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[Evaluation] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[ModifiedDate]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability] ADD DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability] ADD DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[EvaluatorAvailability] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[ModifiedDate]
////GO
////ALTER TABLE[dbo].[Nomination] ADD DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[Nomination] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[Nomination] ADD DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[Nomination] ADD DEFAULT(format(getdate(),'ddMMMyyyyTHHmmss')) FOR[ModifiedDate]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_IsDisplay]  DEFAULT((0)) FOR[IsDisplay]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_IsApproved]  DEFAULT((0)) FOR[IsApproved]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_CreatedBy]  DEFAULT((0)) FOR[CreatedBy]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_CreatedDate]  DEFAULT(getdate()) FOR[CreatedDate]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_ModifiedBy]  DEFAULT((0)) FOR[ModifiedBy]
////GO
////ALTER TABLE[dbo].[StarOfTheMonth] ADD CONSTRAINT[DF_StarOfTheMonth_ModifiedDate]  DEFAULT(getdate()) FOR[ModifiedDate]
////GO


////My script

//DROP TABLE EnumAbbreviation
//DROP TABLE AuditLog
//DROP TABLE DepartmentHead
//DROP TABLE Evaluation
//DROP TABLE EvaluatorAvailability
//DROP TABLE Nomination
//--------1 . Employee
//--Create Table Employee
//--(
//--ID BIGINT IDENTITY(1,1),
//--EmployeeID BIGINT,
//--Name NVARCHAR(200),
//--DateOfBirth NVARCHAR(9),
//--Gender NVARCHAR(1),
//--PhoneNo NVARCHAR(10),
//--EmailID NVARCHAR(100),
//--UserName NVARCHAR(12),
//--Password NVARCHAR(8),
//--Role INT,
//--Department INT,
//--Position NVARCHAR(100),
//--IsActive BIT,
//--CreatedBy BIGINT DEFAULT((0)),
//--CreatedDate NVARCHAR(9),
//--ModifiedBy BIGINT,
//--ModifiedDate NVARCHAR(9),
//--PRIMARY KEY(ID)
//--)
//------ 2. Nomination
//CREATE TABLE Nomination
//(
//ID BIGINT IDENTITY(1,1) primary key,
//EmployeeID BIGINT,
//StartDate NVARCHAR(9),
//EndDate NVARCHAR(9),
//Idea NVARCHAR(200),
//PrimaryObjective NVARCHAR(200),
//BriefDescription NVARCHAR(800),
//Benefits NVARCHAR(200),
//AreaOfInterest_1 NVARCHAR(200),
//ImplementationStatus_1 NVARCHAR(200),
//AreaOfInterest_2 NVARCHAR(200),
//ImplementationStatus_2 NVARCHAR(200),
//AreaOfInterest_3 NVARCHAR(200),
//ImplementationStatus_3 NVARCHAR(200),
//Acknowledgement NVARCHAR(200),
//SubmittedMonth NVARCHAR(15),
//SubmittedYear NVARCHAR(4),
//Status INT,
//IsActive BIT,
//CreatedBy BIGINT DEFAULT((0)),
//CreatedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss'),
//ModifiedBy BIGINT DEFAULT((0)),
//ModifiedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss')
//--PRIMARY KEY(ID)--,
//--FOREIGN KEY(EmployeeID)    REFERENCES Employee(ID),
//--FOREIGN KEY(CreatedBy)     REFERENCES Employee(ID),
//--FOREIGN KEY(ModifiedBy)    REFERENCES Employee(ID)
//)

//------3. Evaluation
//CREATE TABLE Evaluation
//(
//ID BIGINT IDENTITY(1,1) primary key,
//EmployeeID BIGINT,
//EvaluatorID BIGINT,
//ReactiveIdeaDrivenBySituation INT,
//BasedOnInstruction INT,
//ProactiveIdeaGeneratedBySelf INT,
//Delayed INT,
//AsPerPlan INT,
//AheadOfPlan INT,
//FollowedUp INT,
//Participated INT,
//Implemented INT,
//CoordiantionWithInTheDept INT,
//CoordinationWithAnotherFunction INT,
//CoordinationWithMultipleFunctions INT,
//PreventionOfaFailure INT,
//ImprovementFromCurrentSituation INT,
//BreakthroughImprovement INT,
//ScopeIdentified INT,
//ImplementedPartially INT,
//ImplementedInAllApplicableAreas INT,
//TotalScore INT,
//Status INT,
//IsActive BIT,
//CreatedBy BIGINT DEFAULT((0)),
//CreatedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss'),
//ModifiedBy BIGINT DEFAULT((0)),
//ModifiedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss')--,
//--PRIMARY KEY(ID),
//--FOREIGN KEY(EmployeeID)    REFERENCES Employee(ID),
//--FOREIGN KEY(EvaluatorID)   REFERENCES Employee(ID),
//--FOREIGN KEY(CreatedBy)     REFERENCES Employee(ID),
//--FOREIGN KEY(ModifiedBy)    REFERENCES Employee(ID)
//)

//------4. DepartmentHead
//CREATE TABLE DepartmentHead
//(
//ID BIGINT IDENTITY(1,1) primary key,
//NominationID BIGINT,
//EmployeeID BIGINT,
//CommentsByDeptHead NVARCHAR(800),
//Status INT,
//IsActive BIT,
//CreatedBy BIGINT DEFAULT((0)),
//CreatedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss'),
//ModifiedBy BIGINT DEFAULT((0)),
//ModifiedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss')
//--PRIMARY KEY(ID),
//--FOREIGN KEY(NominationID)  REFERENCES Employee(ID),
//--FOREIGN KEY(EmployeeID)    REFERENCES Employee(ID),
//--FOREIGN KEY(CreatedBy)     REFERENCES Employee(ID),
//--FOREIGN KEY(ModifiedBy)    REFERENCES Employee(ID)
//)

//------5. AuditLog
//CREATE TABLE AuditLog
//(
//ID BIGINT IDENTITY(1,1) primary key,
//NominationID BIGINT,
//EmployeeID BIGINT,
//DepartmentHeadID BIGINT,
//TQCHeadID BIGINT,
//EvaluatorID BIGINT,
//IsNewAlert BIT,
//IsNotification BIT,
//CurrentStatus INT,
//CreatedBy BIGINT DEFAULT((0)),
//CreatedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss'),
//ModifiedBy BIGINT DEFAULT((0)),
//ModifiedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss')
//--PRIMARY KEY(ID),
//--FOREIGN KEY(NominationID)      REFERENCES Employee(ID),
//--FOREIGN KEY(EmployeeID)        REFERENCES Employee(ID),
//--FOREIGN KEY(DepartmentHeadID)  REFERENCES Employee(ID),
//--FOREIGN KEY(TQCHeadID)         REFERENCES Employee(ID),
//--FOREIGN KEY(EvaluatorID)       REFERENCES Employee(ID),
//--FOREIGN KEY(CreatedBy)         REFERENCES Employee(ID),
//--FOREIGN KEY(ModifiedBy)        REFERENCES Employee(ID)
//)


//------6. EvaluatorAvailability
//CREATE TABLE EvaluatorAvailability
//(
//ID BIGINT IDENTITY(1,1) primary key,
//EvaluatorID BIGINT,
//Month NVARCHAR(15),
//Year NVARCHAR(4),
//IsActive BIT,
//CreatedBy BIGINT DEFAULT((0)),
//CreatedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss'),
//ModifiedBy BIGINT DEFAULT((0)),
//ModifiedDate NVARCHAR(16) DEFAULT FORMAT(getdate(), 'ddMMMyyyyTHHmmss')
//--PRIMARY KEY(ID),
//--FOREIGN KEY(CreatedBy)     REFERENCES Employee(ID),
//--FOREIGN KEY(ModifiedBy)    REFERENCES Employee(ID)
//)


//CREATE TABLE EnumAbbreviation
//(
//ID BIGINT IDENTITY(1,1),
//ParentID BIGINT,
//ParentName NVARCHAR(50),
//ParentDescription NVARCHAR(100),
//ChildID BIGINT,
//ChildName NVARCHAR(50),
//ChildDescription NVARCHAR(100),
//IsActive Bit,
//PRIMARY KEY(ID)
//)

//INSERT INTO EnumAbbreviation VALUES(1000,'EmployeeRole','Employee Role', 1001, 'Employee' ,'Employee', 1)
//INSERT INTO EnumAbbreviation VALUES(1000,'EmployeeRole','Employee Role', 1002, 'DepartmentHead' ,'Department Head', 1)
//INSERT INTO EnumAbbreviation VALUES(1000,'EmployeeRole','Employee Role', 1003, 'TQCHead' ,'TQC Head', 1)
//INSERT INTO EnumAbbreviation VALUES(1000,'EmployeeRole','Employee Role', 1004, 'Evalutors' ,'Evalutors', 1)
//INSERT INTO EnumAbbreviation VALUES(1000,'EmployeeRole','Employee Role', 1005, 'Admin' ,'Admin', 1)

//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2001, 'Employee_Save' ,'Employee Save', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2002, 'Employee_To_DH' ,'Employee Assign DH', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2003, 'DH_TO_EmployeeClarication' ,'DH Assign EmployeeClarication', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2004, 'DH_To_TQC' ,'DH Assign TQC', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2005, 'TQC_To_Evaluators' ,'TQC Assign Evaluators', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2006, 'Evaluators_Save' ,'Evaluators Save', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2007, 'Evaluators_To_TQC' ,'Evaluators Assign TQC', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2008, 'HoD_Reject' ,'DH Reject', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2009, 'TQC_Reject' ,'TQC Reject', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2010, 'Evaluator_Reject' ,'Evaluator Reject', 1)
//INSERT INTO EnumAbbreviation VALUES(2000,'NominationStatus','Nomination Status', 2011, 'Completed' ,'Completed', 1)

//select* from EnumAbbreviation