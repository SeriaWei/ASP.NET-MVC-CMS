/****** Object:  Table [dbo].[NavigationWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationWidget](
	[ID] [nvarchar](255) NOT NULL,
	[CustomerClass] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Logo] [nvarchar](255) NULL,
 CONSTRAINT [PK_NavigationWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NavigationWidget]  WITH CHECK ADD  CONSTRAINT [FK_NavigationWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[NavigationWidget] CHECK CONSTRAINT [FK_NavigationWidget_Widget]
GO
