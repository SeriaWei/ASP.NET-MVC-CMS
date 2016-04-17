/****** Object:  Table [dbo].[CMS_Page]    Script Date: 2016/04/03 ÐÇÆÚÈÕ 20:18:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Page](
	[ID] [nvarchar](255) NOT NULL,
	[ReferencePageID] [nvarchar](255) NULL,
	[IsPublishedPage] [bit] NULL,
	[ParentId] [nvarchar](255) NULL,
	[PageName] [nvarchar](255) NULL,
	[IsHomePage] [bit] NOT NULL,
	[Url] [nvarchar](255) NULL,
	[LayoutId] [nvarchar](100) NULL,
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
ALTER TABLE [dbo].[CMS_Page]  WITH CHECK ADD  CONSTRAINT [FK_CMS_Page_CMS_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[CMS_Layout] ([ID])
GO
ALTER TABLE [dbo].[CMS_Page] CHECK CONSTRAINT [FK_CMS_Page_CMS_Layout]
GO
