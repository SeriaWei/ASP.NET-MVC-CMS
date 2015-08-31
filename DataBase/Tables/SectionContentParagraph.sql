IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SectionContentParagraph]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[SectionContentParagraph]
END
SET ANSI_NULLS ON
GO
Print N'Create Table [SectionContentParagraph]'
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionContentParagraph](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](255) NULL,
	[HtmlContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_SectionContentParagraph] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


