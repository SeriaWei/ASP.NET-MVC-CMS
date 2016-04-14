/****** Object:  Table [dbo].[ProductCategoryWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategoryWidget](
	[ID] [nvarchar](100) NOT NULL,
	[ProductCategoryID] [int] NULL,
	[TargetPage] [nvarchar](255) NULL,
 CONSTRAINT [PK_ProductCategoryWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProductCategoryWidget]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategoryWidget_ProductCategory] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[ProductCategoryWidget] CHECK CONSTRAINT [FK_ProductCategoryWidget_ProductCategory]
GO
ALTER TABLE [dbo].[ProductCategoryWidget]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategoryWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ProductCategoryWidget] CHECK CONSTRAINT [FK_ProductCategoryWidget_Widget]
GO
