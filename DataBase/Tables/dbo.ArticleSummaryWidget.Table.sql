/****** Object:  Table [dbo].[ArticleSummaryWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleSummaryWidget](
	[ID] [nvarchar](255) NOT NULL,
	[SubTitle] [nvarchar](255) NULL,
	[Style] [nvarchar](255) NULL,
	[DetailLink] [nvarchar](255) NULL,
	[Summary] [nvarchar](max) NULL,
 CONSTRAINT [PK_ArticleSummaryWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ArticleSummaryWidget]  WITH CHECK ADD  CONSTRAINT [FK_ArticleSummaryWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ArticleSummaryWidget] CHECK CONSTRAINT [FK_ArticleSummaryWidget_Widget]
GO
