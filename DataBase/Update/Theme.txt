INSERT [dbo].[CMS_Theme] ([ID], [Title], [Url], [UrlDebugger], [Thumbnail], [IsActived], [Status], [Description], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (N'Default', N'默认', N'~/Themes/Default/css/Theme.min.css', N'~/Themes/Default/css/Theme.css', N'~/Themes/Default/thumbnail.jpg', 1, 1, NULL, N'admin', N'ZKEASOFT', CAST(N'2016-04-04 22:17:10.790' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-04-04 22:21:01.487' AS DateTime))
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Simplex' )
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
        VALUES  ( N'Simplex' ,
                  N'简约' ,
                  N'~/Themes/Simplex/css/Theme.min.css' ,
                  N'~/Themes/Simplex/css/Theme.css' ,
                  N'~/Themes/Simplex/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Cerulean' )
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
        VALUES  ( N'Cerulean' ,
                  N'天蓝色' ,
                  N'~/Themes/Cerulean/css/Theme.min.css' ,
                  N'~/Themes/Cerulean/css/Theme.css' ,
                  N'~/Themes/Cerulean/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;
	
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Cosmo' )
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
        VALUES  ( N'Cosmo' ,
                  N'宇宙' ,
                  N'~/Themes/Cosmo/css/Theme.min.css' ,
                  N'~/Themes/Cosmo/css/Theme.css' ,
                  N'~/Themes/Cosmo/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;
	
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Cyborg' )
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
        VALUES  ( N'Cyborg' ,
                  N'机械' ,
                  N'~/Themes/Cyborg/css/Theme.min.css' ,
                  N'~/Themes/Cyborg/css/Theme.css' ,
                  N'~/Themes/Cyborg/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

		
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Darkly' )
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
        VALUES  ( N'Darkly' ,
                  N'暗黑' ,
                  N'~/Themes/Darkly/css/Theme.min.css' ,
                  N'~/Themes/Darkly/css/Theme.css' ,
                  N'~/Themes/Darkly/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Flatly' )
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
        VALUES  ( N'Flatly' ,
                  N'扁平' ,
                  N'~/Themes/Flatly/css/Theme.min.css' ,
                  N'~/Themes/Flatly/css/Theme.css' ,
                  N'~/Themes/Flatly/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

	
IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Journal' )
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
        VALUES  ( N'Journal' ,
                  N'日报' ,
                  N'~/Themes/Journal/css/Theme.min.css' ,
                  N'~/Themes/Journal/css/Theme.css' ,
                  N'~/Themes/Journal/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Lumen' )
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
        VALUES  ( N'Lumen' ,
                  N'光明' ,
                  N'~/Themes/Lumen/css/Theme.min.css' ,
                  N'~/Themes/Lumen/css/Theme.css' ,
                  N'~/Themes/Lumen/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Lumen' )
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
        VALUES  ( N'Paper' ,
                  N'纸张' ,
                  N'~/Themes/Paper/css/Theme.min.css' ,
                  N'~/Themes/Paper/css/Theme.css' ,
                  N'~/Themes/Paper/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Readable' )
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
        VALUES  ( N'Readable' ,
                  N'清晰' ,
                  N'~/Themes/Readable/css/Theme.min.css' ,
                  N'~/Themes/Readable/css/Theme.css' ,
                  N'~/Themes/Readable/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Sandstone' )
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
        VALUES  ( N'Sandstone' ,
                  N'砂岩' ,
                  N'~/Themes/Sandstone/css/Theme.min.css' ,
                  N'~/Themes/Sandstone/css/Theme.css' ,
                  N'~/Themes/Sandstone/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Slate' )
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
        VALUES  ( N'Slate' ,
                  N'石板' ,
                  N'~/Themes/Slate/css/Theme.min.css' ,
                  N'~/Themes/Slate/css/Theme.css' ,
                  N'~/Themes/Slate/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

	

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Spacelab' )
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
        VALUES  ( N'Spacelab' ,
                  N'实验室' ,
                  N'~/Themes/Spacelab/css/Theme.min.css' ,
                  N'~/Themes/Spacelab/css/Theme.css' ,
                  N'~/Themes/Spacelab/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Superhero' )
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
        VALUES  ( N'Superhero' ,
                  N'超级英雄' ,
                  N'~/Themes/Superhero/css/Theme.min.css' ,
                  N'~/Themes/Superhero/css/Theme.css' ,
                  N'~/Themes/Superhero/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'United' )
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
        VALUES  ( N'United' ,
                  N'联合' ,
                  N'~/Themes/United/css/Theme.min.css' ,
                  N'~/Themes/United/css/Theme.css' ,
                  N'~/Themes/United/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Yeti' )
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
        VALUES  ( N'Yeti' ,
                  N'雪人' ,
                  N'~/Themes/Yeti/css/Theme.min.css' ,
                  N'~/Themes/Yeti/css/Theme.css' ,
                  N'~/Themes/Yeti/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;

	IF NOT EXISTS ( SELECT  1
                FROM    dbo.CMS_Theme
                WHERE   ID = N'Paper' )
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
        VALUES  ( N'Paper' ,
                  N'报纸' ,
                  N'~/Themes/Paper/css/Theme.min.css' ,
                  N'~/Themes/Paper/css/Theme.css' ,
                  N'~/Themes/Paper/thumbnail.jpg' ,
                  0 ,
                  1           
                );
    END;