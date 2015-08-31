IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SectionContentTitle]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[SectionContentTitle]
END
SET ANSI_NULLS ON
GO
Print N'Create Table [SectionContentTitle]'
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionContentTitle](
	[ID] [int] NOT NULL,
	[SectionWidgetId] [nvarchar](255) NULL,
	[InnerText] [nvarchar](255) NULL,
	[Href] [nvarchar](255) NULL,
 CONSTRAINT [PK_SectionContentTitle] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


