IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CarouselWidget]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[CarouselWidget]
END
PRINT N'Create Tabel [CarouselWidget]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarouselWidget](
	[ID] [nvarchar](255) NOT NULL,
	[CarouselID] [int] NULL,
 CONSTRAINT [PK_CarouselWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


