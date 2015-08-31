IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[Users]
END
Print N'Create Table [Users]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [nvarchar](255) NOT NULL,
	[PassWord] [nvarchar](255) NULL,
	[ApiLoginToken] [nvarchar](255) NULL,
	[LastLoginDate] [datetime] NULL,
	[LoginIP] [nvarchar](255) NULL,
	[PhotoUrl] [nvarchar](255) NULL,
	[Timestamp] [nvarchar](255) NULL,
	[UserName] [nvarchar](255) NULL,
	[UserTypeCD] [int] NULL,
	[Address] [nvarchar](255) NULL,
	[Age] [int] NULL,
	[Birthday] [datetime] NULL,
	[Birthplace] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[EnglishName] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[Hobby] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[MaritalStatus] [int] NULL,
	[MobilePhone] [nvarchar](255) NULL,
	[NickName] [nvarchar](255) NULL,
	[Profession] [nvarchar](255) NULL,
	[QQ] [nvarchar](255) NULL,
	[School] [nvarchar](255) NULL,
	[Sex] [nvarchar](255) NULL,
	[Telephone] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


