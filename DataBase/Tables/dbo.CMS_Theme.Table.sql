/****** Object:  Table [dbo].[CMS_Theme]    Script Date: 2016/04/05 ÐÇÆÚ¶þ 10:21:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Theme](
	[ID] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Url] [nvarchar](100) NULL,
	[UrlDebugger] [nvarchar](100) NULL,
	[Thumbnail] [nvarchar](100) NULL,
	[IsActived] [bit] NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_Theme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
