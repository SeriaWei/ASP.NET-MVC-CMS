IF NOT EXISTS ( SELECT  1
                FROM    sys.syscolumns
                WHERE   id = OBJECT_ID(N'Navigation')
                        AND name = N'IsMobile' )
    BEGIN
        ALTER TABLE dbo.Navigation ADD IsMobile BIT NULL;
    END;
GO
IF NOT EXISTS ( SELECT  1
                FROM    dbo.Language
                WHERE   LanKey = N'NavigationEntity@IsMobile' )
    BEGIN
        INSERT  INTO dbo.Language
                ( LanKey ,
                  LanID ,
                  LanValue ,
                  Module ,
                  LanType
	            )
        VALUES  ( N'NavigationEntity@IsMobile' ,
                  2052 ,
                  N'ÊÖ»úµ¼º½' ,
                  N'NavigationEntity' ,
                  N'EntityProperty'  
	            );
    END;
GO