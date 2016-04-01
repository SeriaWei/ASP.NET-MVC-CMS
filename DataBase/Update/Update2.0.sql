IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('CMS_Page')
                        AND name = 'IsPublishedPage' )
    BEGIN
        ALTER TABLE dbo.CMS_Page ADD IsPublishedPage BIT DEFAULT 0;
        UPDATE  CMS_Page
        SET     IsPublishedPage = 0 ,
                IsPublish = 0;
    END;
GO


IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('CMS_WidgetBase')
                        AND name = 'IsTemplate' )
    BEGIN
        ALTER TABLE dbo.CMS_WidgetBase ADD IsTemplate BIT DEFAULT 0;
        UPDATE  CMS_WidgetBase
        SET     IsTemplate = 0;
    END;
GO
IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('CMS_WidgetBase')
                        AND name = 'IsSystem' )
    BEGIN
        ALTER TABLE dbo.CMS_WidgetBase ADD IsSystem BIT DEFAULT 0;
        UPDATE  CMS_WidgetBase
        SET     IsSystem = 0;
    END;
GO
IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('CMS_WidgetBase')
                        AND name = 'Thumbnail' )
    BEGIN
        ALTER TABLE dbo.CMS_WidgetBase ADD Thumbnail NVARCHAR(200);
    END;
GO

IF EXISTS ( SELECT  *
            FROM    syscolumns
            WHERE   id = OBJECT_ID('ArticleListWidget')
                    AND name = 'ArticleCategoryID' )
    BEGIN
        EXEC sys.sp_rename @objname = N'ArticleListWidget.ArticleCategoryID',
            @newname = N'ArticleTypeID', @objtype = 'COLUMN';
    END;
	GO

IF EXISTS ( SELECT  *
            FROM    syscolumns
            WHERE   id = OBJECT_ID('ArticleTopWidget')
                    AND name = 'ArticleCategoryID' )
    BEGIN
        EXEC sys.sp_rename @objname = N'ArticleTopWidget.ArticleCategoryID',
            @newname = N'ArticleTypeID', @objtype = 'COLUMN';
    END;
	GO

	IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('CMS_Page')
                        AND name = 'ReferencePageID' )
    BEGIN
        ALTER TABLE dbo.CMS_Page ADD ReferencePageID NVARCHAR(255);
    END;
	GO
	UPDATE T0 SET T0.ReferencePageID=(SELECT TOP 1 ID FROM dbo.CMS_Page WHERE IsPublishedPage=0 AND Url=T0.Url) FROM dbo.CMS_Page T0 WHERE T0.IsPublishedPage=1
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Media](
	[ID] [nvarchar](50) NOT NULL,
	[ParentID] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[MediaType] [int] NULL,
	[Url] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreatebyName] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](50) NULL,
	[LastUpdateByName] [nvarchar](50) NULL,
	[LastUpdateDate] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_CMS_Media] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[CMS_Media] ([ID], [ParentID], [Title], [MediaType], [Url], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate], [Description]) VALUES (N'6056810a7ede46bb94b55b2756323640', N'#', N'图片', 1, NULL, NULL, N'admin', N'ZKEASOFT', CAST(N'2016-04-01 21:42:14.960' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-04-01 21:42:14.960' AS DateTime), NULL)
GO

