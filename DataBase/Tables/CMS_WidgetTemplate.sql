IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_WidgetTemplate]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[CMS_WidgetTemplate]
END
GO
PRINT N'Create Tabel [CMS_WidgetTemplate]'
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CMS_WidgetTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[GroupName] [nvarchar](255) NULL,
	[PartialView] [nvarchar](255) NULL,
	[AssemblyName] [nvarchar](255) NULL,
	[ServiceTypeName] [nvarchar](255) NULL,
	[ViewModelTypeName] [nvarchar](255) NULL,
	[Order] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[FormView] [nvarchar](255) NULL,
	[StyleClass] [nvarchar](255) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_WidgetTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


