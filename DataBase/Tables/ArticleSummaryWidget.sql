IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleSummaryWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[ArticleSummaryWidget]
END
PRINT N'Create Tabel [ArticleSummaryWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleSummaryWidget](
	[ID] [nvarchar](255) NOT NULL,
	[SubTitle] [nvarchar](255) NULL,
	[Style] [nvarchar](255) NULL,
	[DetailLink] [nvarchar](255) NULL,
	[Summary] [nvarchar](max) NULL,
 CONSTRAINT [PK_ArticleSummaryWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


