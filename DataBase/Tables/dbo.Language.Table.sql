/****** Object:  Table [dbo].[Language]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanKey] [nvarchar](50) NOT NULL,
	[LanID] [int] NOT NULL,
	[LanValue] [nvarchar](500) NULL,
	[Module] [nvarchar](50) NULL,
	[LanType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanKey] ASC,
	[LanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
