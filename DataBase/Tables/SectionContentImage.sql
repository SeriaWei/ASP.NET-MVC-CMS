IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SectionContentImage]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[SectionContentImage]
END
Print N'Create Table [SectionContentImage]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionContentImage](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](255) NULL,
	[ImageSrc] [nvarchar](255) NULL,
	[ImageAlt] [nvarchar](255) NULL,
	[ImageTitle] [nvarchar](255) NULL,
	[Href] [nvarchar](255) NULL,
 CONSTRAINT [PK_SectionContentImage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


