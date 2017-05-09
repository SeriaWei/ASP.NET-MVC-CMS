IF NOT EXISTS ( SELECT  *
                FROM    syscolumns
                WHERE   id = OBJECT_ID('NavigationWidget')
                        AND name = 'RootID' )
    BEGIN
        ALTER TABLE dbo.NavigationWidget ADD RootID NVARCHAR(100) NULL;
    END;

IF NOT EXISTS ( SELECT  *
                FROM    dbo.Language
                WHERE   LanKey = N'NavigationWidget@RootID' )
    BEGIN
        INSERT  INTO dbo.Language
                ( LanKey ,
                  LanID ,
                  LanValue ,
                  Module ,
                  LanType
                )
        VALUES  ( N'NavigationWidget@RootID' ,
                  2052 ,
                  N'¸ù½áµã' ,
                  N'NavigationWidget' ,
                  N'EntityProperty' 
                );
    END;
