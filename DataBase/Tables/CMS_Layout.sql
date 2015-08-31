IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_Layout]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[CMS_Layout]
END
PRINT N'Create Tabel [CMS_Layout]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CMS_Layout](
	[ID] [nvarchar](255) NOT NULL,
	[LayoutName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[ContainerClass] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Script] [nvarchar](255) NULL,
	[Style] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ImageThumbUrl] [nvarchar](255) NULL,
	[Theme] [nvarchar](255) NULL,
 CONSTRAINT [PK_CMS_Layout] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


