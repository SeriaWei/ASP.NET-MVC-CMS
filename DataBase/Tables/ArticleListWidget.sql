IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleListWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[ArticleListWidget]
END
PRINT N'Create Tabel [ArticleListWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleListWidget](
	[ID] [nvarchar](255) NOT NULL,
	[ArticleCategoryID] [int] NULL,
	[DetailPageUrl] [nvarchar](255) NULL,
	[IsPageable] [bit] NOT NULL,
	[PageSize] [int] NULL,
 CONSTRAINT [PK_ArticleListWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


