ALTER TABLE dbo.SectionContentTitle ADD TitleLevel NVARCHAR(10)
GO
DELETE FROM dbo.Language WHERE LanKey = N'SectionContentTitle@TitleLevel'
INSERT INTO dbo.Language( LanKey ,LanID ,LanValue ,Module ,LanType)VALUES  ( N'SectionContentTitle@TitleLevel' ,2052 , N'µÈ¼¶' , N'SectionContentTitle' ,N'EntityProperty')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageView](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageUrl] [nvarchar](500) NULL,
	[PageTitle] [nvarchar](200) NULL,
	[IPAddress] [nvarchar](50) NULL,
	[SessionID] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PageView] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
