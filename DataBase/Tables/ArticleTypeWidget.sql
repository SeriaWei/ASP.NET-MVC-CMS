IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleTypeWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[ArticleTypeWidget]
END
PRINT N'Create Tabel [ArticleTypeWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleTypeWidget](
	[ID] [nvarchar](255) NOT NULL,
	[ArticleTypeID] [int] NULL,
	[TargetPage] [nvarchar](255) NULL,
 CONSTRAINT [PK_ArticleTypeWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


