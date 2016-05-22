@echo off
REM: 请根据您的个人实际情况修改以下信息：
set server=(local)
set dataBase=ZKEACMS
set dbUserId=sa
set dbPassword=sa
@echo -----------------------------------------------------------------------------
@echo *** 欢迎使用 ZKEACMS***
@echo -----------------------------------------------------------------------------
@echo 运行该命令将初始化 ZKEACMS 数据库的数据。
@echo 在开始之前，请根据您的个人实际情况修改以下信息。（用记事打开这个文件）
@echo -----------------------------------------------------------------------------
@echo 请认真核对以下信息，确保正确：
@echo -----------------------------------------------------------------------------
@echo 数据库服务器: %server%
@echo 数据库名称: %dataBase%
@echo 登录名: %dbUserId%
@echo 密码: %dbPassword%
@echo -----------------------------------------------------------------------------
@pause
@echo ClearData...
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Users.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Language.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ImageWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.VideoWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ScriptWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionContentParagraph.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionContentImage.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionContentTitle.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionContentCallToAction.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionContent.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionGroup.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.SectionWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.NavigationWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ProductListWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ProductDetailWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ProductCategoryWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.HtmlWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CarouselWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleTypeWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleTopWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleSummaryWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleListWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleDetailWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_WidgetBase.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Navigation.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_Theme.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_Page.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_Media.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_WidgetTemplate.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_Zone.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_LayoutHtml.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CMS_Layout.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.DataDictionary.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Product.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ProductCategory.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.CarouselItem.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Carousel.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.Article.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "ClearData\dbo.ArticleType.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors

@echo InitailData...
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleType.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Article.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Carousel.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CarouselItem.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductCategory.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Product.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.DataDictionary.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Layout.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_LayoutHtml.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Zone.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_WidgetTemplate.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Media.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Page.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Theme.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Navigation.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_WidgetBase.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleDetailWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleListWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleSummaryWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleTopWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleTypeWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CarouselWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.HtmlWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductCategoryWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductDetailWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductListWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.NavigationWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionGroup.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContent.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentCallToAction.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentTitle.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentImage.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentParagraph.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ScriptWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.VideoWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ImageWidget.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Language.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Users.Table.sql"
if %ERRORLEVEL% NEQ 0 goto errors

@echo -----------------------------------------------------------------------------
@echo 数据初始化成功。
goto done

:errors
@echo -----------------------------------------------------------------------------
@echo 警告，在数据库创建过程中，出现了错误。
goto done
:done
@pause