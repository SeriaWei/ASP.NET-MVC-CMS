/****** Object:  Table [dbo].[ArticleTypeDetailWidget]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleTypeDetailWidget](
	[ID] [nvarchar](255) NOT NULL,
	[ArticleType] [int] NULL,
 CONSTRAINT [PK_ArticleTypeDetailWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ArticleTypeDetailWidget]  WITH CHECK ADD  CONSTRAINT [FK_ArticleTypeDetailWidget_ArticleType] FOREIGN KEY([ArticleType])
REFERENCES [dbo].[ArticleType] ([ID])
GO
ALTER TABLE [dbo].[ArticleTypeDetailWidget] CHECK CONSTRAINT [FK_ArticleTypeDetailWidget_ArticleType]
GO
ALTER TABLE [dbo].[ArticleTypeDetailWidget]  WITH CHECK ADD  CONSTRAINT [FK_ArticleTypeDetailWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ArticleTypeDetailWidget] CHECK CONSTRAINT [FK_ArticleTypeDetailWidget_Widget]
GO
