IF NOT EXISTS(select 1 FROM syscolumns where id=object_id('CMS_WidgetBase') and name='ExtendData')
BEGIN
	ALTER TABLE dbo.CMS_WidgetBase ADD ExtendData NVARCHAR(max)
END
