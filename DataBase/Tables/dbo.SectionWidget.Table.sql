/****** Object:  Table [dbo].[SectionWidget]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionWidget](
	[ID] [nvarchar](255) NOT NULL,
	[SectionTitle] [nvarchar](255) NULL,
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
