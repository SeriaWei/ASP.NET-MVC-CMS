/****** Object:  Table [dbo].[DataDictionary]    Script Date: 2016/4/1 ÐÇÆÚÎå 17:21:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataDictionary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DicName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[DicValue] [nvarchar](255) NULL,
	[Order] [nvarchar](255) NULL,
	[Pid] [int] NULL,
	[IsSystem] [bit] NOT NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ImageThumbUrl] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DataDictionary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
