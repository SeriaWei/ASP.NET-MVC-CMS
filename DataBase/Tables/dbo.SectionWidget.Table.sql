/****** Object:  Table [dbo].[SectionWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionWidget](
	[ID] [nvarchar](100) NOT NULL,
	[SectionTitle] [nvarchar](255) NULL,
	[IsHorizontal] [bit] NULL,
 CONSTRAINT [PK_SectionWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SectionWidget]  WITH CHECK ADD  CONSTRAINT [FK_SectionWidget_CMS_WidgetBase] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[SectionWidget] CHECK CONSTRAINT [FK_SectionWidget_CMS_WidgetBase]
GO
