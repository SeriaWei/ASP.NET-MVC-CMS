/****** Object:  Table [dbo].[CarouselWidget]    Script Date: 2015/09/16 ÐÇÆÚÈý 22:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarouselWidget](
	[ID] [nvarchar](255) NOT NULL,
	[CarouselID] [int] NULL,
 CONSTRAINT [PK_CarouselWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CarouselWidget]  WITH CHECK ADD  CONSTRAINT [FK_CarouselWidget_Carousel] FOREIGN KEY([CarouselID])
REFERENCES [dbo].[Carousel] ([ID])
GO
ALTER TABLE [dbo].[CarouselWidget] CHECK CONSTRAINT [FK_CarouselWidget_Carousel]
GO
ALTER TABLE [dbo].[CarouselWidget]  WITH CHECK ADD  CONSTRAINT [FK_CarouselWidget_Widget] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[CarouselWidget] CHECK CONSTRAINT [FK_CarouselWidget_Widget]
GO
