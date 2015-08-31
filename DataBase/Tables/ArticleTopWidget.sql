IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleTopWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[ArticleTopWidget]
END
PRINT N'Create Tabel [ArticleTopWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleTopWidget](
	[ID] [nvarchar](255) NOT NULL,
	[ArticleCategoryID] [int] NULL,
	[Tops] [int] NULL,
	[SubTitle] [nvarchar](255) NULL,
	[MoreLink] [nvarchar](255) NULL,
	[DetailPageUrl] [nvarchar](255) NULL,
 CONSTRAINT [PK_ArticleTopWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


