USE [StudentDB]
GO

/****** Object:  Table [dbo].[StudentDetail]    Script Date: 4/24/2015 12:42:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StudentDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[Mark1] [float] NULL,
	[Mark2] [float] NULL,
	[Mark3] [float] NULL,
	[Total] [float] NULL,
	[Average] [float] NULL,
 CONSTRAINT [PK_StudentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[StudentDetail] ADD  CONSTRAINT [DF_StudentDetail_Mark1]  DEFAULT ((0)) FOR [Mark1]
GO

ALTER TABLE [dbo].[StudentDetail] ADD  CONSTRAINT [DF_StudentDetail_Mark2]  DEFAULT ((0)) FOR [Mark2]
GO

ALTER TABLE [dbo].[StudentDetail] ADD  CONSTRAINT [DF_StudentDetail_Mark3]  DEFAULT ((0)) FOR [Mark3]
GO


