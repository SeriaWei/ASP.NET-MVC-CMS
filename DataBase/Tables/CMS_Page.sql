IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_Page]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[CMS_Page]
END
PRINT N'Create Tabel [CMS_Page]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CMS_Page](
	[ID] [nvarchar](255) NOT NULL,
	[ParentId] [nvarchar](255) NULL,
	[PageName] [nvarchar](255) NULL,
	[IsHomePage] [bit] NOT NULL,
	[Url] [nvarchar](255) NULL,
	[LayoutId] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Content] [nvarchar](max) NULL,
	[DisplayOrder] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[IsPublish] [bit] NOT NULL,
	[PublishDate] [datetime] NULL,
	[MetaKeyWorlds] [nvarchar](255) NULL,
	[MetaDescription] [nvarchar](255) NULL,
	[Script] [nvarchar](255) NULL,
	[Style] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


