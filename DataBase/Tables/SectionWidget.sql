IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SectionWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[SectionWidget]
END
Print N'Create Table [SectionWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SectionWidget](
	[ID] [nvarchar](255) NOT NULL,
	[SectionTitle] [nvarchar](255) NULL,
 CONSTRAINT [PK_SectionWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


