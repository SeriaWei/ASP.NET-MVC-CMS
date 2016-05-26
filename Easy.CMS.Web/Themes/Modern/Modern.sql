IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Modern' )
    BEGIN
        INSERT  INTO dbo.CMS_Theme
                ( ID ,
                  Title ,
                  Url ,
                  UrlDebugger ,
                  Thumbnail ,
                  IsActived ,
                  Status          
                )
        VALUES  ( N'Modern' ,
                  N'ож╢З' ,
                  N'~/Themes/Modern/css/Theme.min.css' ,
                  N'~/Themes/Modern/css/Theme.css' ,
                  N'~/Themes/Modern/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;