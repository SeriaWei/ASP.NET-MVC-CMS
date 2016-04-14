/****** Object:  Table [dbo].[SectionContent]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionContent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SectionWidgetId] [nvarchar](255) NOT NULL,
	[SectionGroupId] [int] NOT NULL,
	[SectionContentType] [int] NULL,
	[Order] [int] NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_SectionContent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SectionContent]  WITH CHECK ADD  CONSTRAINT [FK_SectionContent_SectionGroup] FOREIGN KEY([SectionGroupId])
REFERENCES [dbo].[SectionGroup] ([ID])
GO
ALTER TABLE [dbo].[SectionContent] CHECK CONSTRAINT [FK_SectionContent_SectionGroup]
GO
ALTER TABLE [dbo].[SectionContent]  WITH CHECK ADD  CONSTRAINT [FK_SectionContent_Widget] FOREIGN KEY([SectionWidgetId])
REFERENCES [dbo].[SectionWidget] ([ID])
GO
ALTER TABLE [dbo].[SectionContent] CHECK CONSTRAINT [FK_SectionContent_Widget]
GO
