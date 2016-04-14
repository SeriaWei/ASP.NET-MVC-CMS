/****** Object:  Table [dbo].[HtmlWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HtmlWidget](
	[ID] [nvarchar](100) NOT NULL,
	[HTML] [nvarchar](max) NULL,
 CONSTRAINT [PK_HtmlWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[HtmlWidget]  WITH CHECK ADD  CONSTRAINT [FK_HtmlWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[HtmlWidget] CHECK CONSTRAINT [FK_HtmlWidget_Widget]
GO
