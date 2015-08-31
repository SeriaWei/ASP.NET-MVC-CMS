IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_LayoutHtml]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[CMS_LayoutHtml]
END
PRINT N'Create Tabel [CMS_LayoutHtml]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CMS_LayoutHtml](
	[LayoutHtmlId] [int] NOT NULL,
	[LayoutId] [nvarchar](255) NULL,
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


