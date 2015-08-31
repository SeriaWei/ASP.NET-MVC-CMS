IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SectionContentCallToAction]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[SectionContentCallToAction]
END
Print N'Create Table [SectionContentCallToAction]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionContentCallToAction](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](255) NULL,
	[InnerText] [nvarchar](255) NULL,
	[Href] [nvarchar](255) NULL,
 CONSTRAINT [PK_SectionContentCallToAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


