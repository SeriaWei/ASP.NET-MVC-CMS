/****** Object:  Table [dbo].[ScriptWidget]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScriptWidget](
	[ID] [nvarchar](100) NOT NULL,
	[Script] [nvarchar](max) NULL,
 CONSTRAINT [PK_ScriptWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ScriptWidget]  WITH CHECK ADD  CONSTRAINT [FK_ScriptWidget_CMS_WidgetBase] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ScriptWidget] CHECK CONSTRAINT [FK_ScriptWidget_CMS_WidgetBase]
GO
