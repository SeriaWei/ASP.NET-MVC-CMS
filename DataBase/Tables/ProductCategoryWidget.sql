IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategoryWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[ProductCategoryWidget]
END
Print N'Create Table [ProductCategoryWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductCategoryWidget](
	[ID] [nvarchar](255) NOT NULL,
	[ProductCategoryID] [int] NULL,
	[TargetPage] [nvarchar](255) NULL,
 CONSTRAINT [PK_ProductCategoryWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


