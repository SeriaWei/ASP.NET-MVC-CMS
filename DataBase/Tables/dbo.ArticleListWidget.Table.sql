/****** Object:  Table [dbo].[ArticleListWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleListWidget](
	[ID] [nvarchar](100) NOT NULL,
	[ArticleTypeID] [int] NULL,
	[DetailPageUrl] [nvarchar](255) NULL,
	[IsPageable] [bit] NOT NULL,
	[PageSize] [int] NULL,
 CONSTRAINT [PK_ArticleListWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ArticleListWidget]  WITH CHECK ADD  CONSTRAINT [FK_ArticleListWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ArticleListWidget] CHECK CONSTRAINT [FK_ArticleListWidget_Widget]
GO
