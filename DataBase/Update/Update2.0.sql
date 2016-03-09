IF NOT EXISTS(SELECT * FROM syscolumns where id=object_id('CMS_Page') and name='IsPublishedPage')
BEGIN
	ALTER TABLE dbo.CMS_Page ADD IsPublishedPage BIT DEFAULT 0
END
GO
UPDATE CMS_Page SET IsPublishedPage=0
GO
UPDATE dbo.CMS_Page SET IsPublish=0
GO

IF NOT EXISTS(SELECT * FROM syscolumns where id=object_id('CMS_WidgetBase') and name='IsTemplate')
BEGIN
	ALTER TABLE dbo.CMS_WidgetBase ADD IsTemplate BIT DEFAULT 0
END
GO
IF NOT EXISTS(SELECT * FROM syscolumns where id=object_id('CMS_WidgetBase') and name='Thumbnail')
BEGIN
	ALTER TABLE dbo.CMS_WidgetBase ADD Thumbnail NVARCHAR(200)
END
GO