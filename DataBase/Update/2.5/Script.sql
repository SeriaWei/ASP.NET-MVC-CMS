IF NOT EXISTS ( SELECT  1
                FROM    sys.syscolumns
                WHERE   id = OBJECT_ID(N'CMS_Page')
                        AND name = N'IsStaticCache' )
    BEGIN
        ALTER TABLE dbo.CMS_Page ADD IsStaticCache BIT NULL;
    END;
GO

DELETE FROM dbo.Language WHERE LanKey=N'PageEntity@IsStaticCache'
INSERT INTO dbo.Language
        ( LanKey ,
          LanID ,
          LanValue ,
          Module ,
          LanType
        )
VALUES  ( N'PageEntity@IsStaticCache' , 
          2052 ,
          N' π”√æ≤Ã¨ª∫¥Ê' , 
          N'PageEntity' , 
          N'EntityProperty' 
        )