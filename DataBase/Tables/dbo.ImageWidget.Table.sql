/****** Object:  Table [dbo].[ImageWidget]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageWidget](
	[ID] [nvarchar](100) NOT NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[AltText] [nvarchar](255) NULL,
	[Link] [nvarchar](255) NULL,
 CONSTRAINT [PK_ImageWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ImageWidget]  WITH CHECK ADD  CONSTRAINT [FK_ImageWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[ImageWidget] CHECK CONSTRAINT [FK_ImageWidget_Widget]
GO
