/****** Object:  Table [dbo].[SectionContentImage]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionContentImage](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](100) NOT NULL,
	[ImageSrc] [nvarchar](255) NULL,
	[ImageAlt] [nvarchar](255) NULL,
	[ImageTitle] [nvarchar](255) NULL,
	[Href] [nvarchar](255) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
 CONSTRAINT [PK_SectionContentImage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SectionContentImage]  WITH CHECK ADD  CONSTRAINT [FK_SectionContentImage_SectionContent] FOREIGN KEY([ID])
REFERENCES [dbo].[SectionContent] ([ID])
GO
ALTER TABLE [dbo].[SectionContentImage] CHECK CONSTRAINT [FK_SectionContentImage_SectionContent]
GO
ALTER TABLE [dbo].[SectionContentImage]  WITH CHECK ADD  CONSTRAINT [FK_SectionContentImage_SectionWidget] FOREIGN KEY([SectionWidgetId])
REFERENCES [dbo].[SectionWidget] ([ID])
GO
ALTER TABLE [dbo].[SectionContentImage] CHECK CONSTRAINT [FK_SectionContentImage_SectionWidget]
GO
