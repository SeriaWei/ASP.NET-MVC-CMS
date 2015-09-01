/****** Object:  Table [dbo].[SectionContentCallToAction]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionContentCallToAction](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](255) NOT NULL,
	[InnerText] [nvarchar](255) NULL,
	[Href] [nvarchar](255) NULL,
 CONSTRAINT [PK_SectionContentCallToAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SectionContentCallToAction]  WITH CHECK ADD  CONSTRAINT [FK_SectionContentCallToAction_SectionContent] FOREIGN KEY([ID])
REFERENCES [dbo].[SectionContent] ([ID])
GO
ALTER TABLE [dbo].[SectionContentCallToAction] CHECK CONSTRAINT [FK_SectionContentCallToAction_SectionContent]
GO
ALTER TABLE [dbo].[SectionContentCallToAction]  WITH CHECK ADD  CONSTRAINT [FK_SectionContentCallToAction_SectionWidget] FOREIGN KEY([SectionWidgetId])
REFERENCES [dbo].[SectionWidget] ([ID])
GO
ALTER TABLE [dbo].[SectionContentCallToAction] CHECK CONSTRAINT [FK_SectionContentCallToAction_SectionWidget]
GO
