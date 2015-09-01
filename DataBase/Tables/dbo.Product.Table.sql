/****** Object:  Table [dbo].[Product]    Script Date: 2015/9/1 ÐÇÆÚ¶þ 16:54:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ImageThumbUrl] [nvarchar](255) NULL,
	[BrandCD] [int] NULL,
	[ProductCategory] [int] NULL,
	[Color] [nvarchar](255) NULL,
	[Price] [money] NULL,
	[RebatePrice] [money] NULL,
	[PurchasePrice] [money] NULL,
	[Norm] [nvarchar](255) NULL,
	[ShelfLife] [nvarchar](255) NULL,
	[ProductContent] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](255) NULL,
	[CreatebyName] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](255) NULL,
	[LastUpdateByName] [nvarchar](255) NULL,
	[LastUpdateDate] [datetime] NULL,
	[IsPublish] [bit] NOT NULL,
	[PublishDate] [datetime] NULL,
	[Status] [int] NULL,
	[SEOTitle] [nvarchar](255) NULL,
	[SEOKeyWord] [nvarchar](255) NULL,
	[SEODescription] [nvarchar](max) NULL,
	[OrderIndex] [int] NULL,
	[SourceFrom] [nvarchar](255) NULL,
	[Url] [nvarchar](255) NULL,
	[TargetFrom] [nvarchar](255) NULL,
	[TargetUrl] [nvarchar](255) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([ProductCategory])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
