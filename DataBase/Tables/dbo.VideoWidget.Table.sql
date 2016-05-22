/****** Object:  Table [dbo].[VideoWidget]    Script Date: 2016/04/21 ÐÇÆÚËÄ 22:49:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoWidget](
	[ID] [nvarchar](100) NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Url] [nvarchar](255) NULL,
	[Code] [nvarchar](500) NULL,
 CONSTRAINT [PK_VideoWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
