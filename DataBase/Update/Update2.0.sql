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