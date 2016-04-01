/****** Object:  Table [dbo].[CMS_Zone]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Zone](
	[ID] [nvarchar](255) NOT NULL,
	[LayoutId] [nvarchar](255) NULL,
	[ZoneName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_CMS_Zone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CMS_Zone]  WITH CHECK ADD  CONSTRAINT [FK_CMS_Zone_CMS_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[CMS_Layout] ([ID])
GO
ALTER TABLE [dbo].[CMS_Zone] CHECK CONSTRAINT [FK_CMS_Zone_CMS_Layout]
GO
