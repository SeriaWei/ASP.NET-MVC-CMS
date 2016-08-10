ALTER TABLE dbo.SectionContentTitle ADD TitleLevel NVARCHAR(10)
GO
DELETE FROM dbo.Language WHERE LanKey = N'SectionContentTitle@TitleLevel'
INSERT INTO dbo.Language( LanKey ,LanID ,LanValue ,Module ,LanType)VALUES  ( N'SectionContentTitle@TitleLevel' ,2052 , N'µÈ¼¶' , N'SectionContentTitle' ,N'EntityProperty')
GO