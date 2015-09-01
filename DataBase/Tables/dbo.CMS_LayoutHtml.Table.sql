/****** Object:  Table [dbo].[CMS_LayoutHtml]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_LayoutHtml](
	[LayoutHtmlId] [int] IDENTITY(1,1) NOT NULL,
	[LayoutId] [nvarchar](255) NOT NULL,
	[Html] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_LayoutHtml] PRIMARY KEY CLUSTERED 
(
	[LayoutHtmlId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CMS_LayoutHtml]  WITH CHECK ADD  CONSTRAINT [FK_CMS_LayoutHtml_CMS_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[CMS_Layout] ([ID])
GO
ALTER TABLE [dbo].[CMS_LayoutHtml] CHECK CONSTRAINT [FK_CMS_LayoutHtml_CMS_Layout]
GO
