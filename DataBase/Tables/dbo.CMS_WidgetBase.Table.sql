/****** Object:  Table [dbo].[CMS_WidgetBase]    Script Date: 2016/03/08 ÐÇÆÚ¶þ 23:14:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_WidgetBase](
	[ID] [nvarchar](255) NOT NULL,
	[WidgetName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Position] [int] NULL,
	[LayoutId] [nvarchar](255) NULL,
	[PageId] [nvarchar](255) NULL,
	[ZoneId] [nvarchar](255) NULL,
	[PartialView] [nvarchar](255) NULL,
	[AssemblyName] [nvarchar](255) NULL,
	[ServiceTypeName] [nvarchar](255) NULL,
	[ViewModelTypeName] [nvarchar](255) NULL,
	[FormView] [nvarchar](255) NULL,
	[StyleClass] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[IsTemplate] [bit] NULL DEFAULT ((0)),
	[Thumbnail] [nvarchar](200) NULL,
	[IsSystem] [bit] NULL DEFAULT ((0)),
 CONSTRAINT [PK_CMS_WidgetBase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
