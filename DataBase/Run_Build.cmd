@echo off
REM: Modify parameters below to build database.
set server=(local)
set dataBase=EasyCMS
set dbUserId=sa
set dbPassword=sa
set dbPath=E:\GitHub\Easy.CMS\Easy.CMS.Web\App_Data
@echo -----------------------------------------------------------------------------
@echo *** Welcome to use EasyCMS ***
@echo -----------------------------------------------------------------------------
@echo This command will help you to build the database.
@echo Before start, Please according to your personal situation to be modified.
@echo -----------------------------------------------------------------------------
@echo Please check the information below
@echo -----------------------------------------------------------------------------
@echo Server: %server%
@echo DataBase: %dataBase%
@echo UserName: %dbUserId%
@echo PassWord: %dbPassword%
@echo Save database to: %dbPath%\%database%.mdf
@echo *** Make sure the folder is already exists ***
@echo -----------------------------------------------------------------------------
@echo Do you want to continue?[y/n]
@echo -----------------------------------------------------------------------------
set /p isStart=
if "%isStart%" NEQ "y" goto done
@echo -----------------------------------------------------------------------------
@echo Creating DataBase %dataBase%
sqlcmd -S %server% -d master -U %dbUserId% -P %dbPassword% -b -i "CreateDataBase.sql"
if %ERRORLEVEL% NEQ 0 goto errors

@echo Creating Tables...
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_Layout.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_LayoutHtml.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_Zone.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_Page.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_WidgetBase.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CMS_WidgetTemplate.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleType.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Article.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleListWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleSummaryWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleTopWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleTypeDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ArticleTypeWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Carousel.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CarouselItem.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.CarouselWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.DataDictionary.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.HtmlWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ImageWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Navigation.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.NavigationWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ProductCategory.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Product.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ProductCategoryWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ProductDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.ProductListWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionGroup.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionContent.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionContentCallToAction.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionContentImage.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionContentParagraph.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.SectionContentTitle.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Language.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\dbo.Users.Table.sql"

@echo InitailData...
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleType.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Article.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Carousel.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CarouselItem.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductCategory.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Product.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.DataDictionary.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Layout.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_LayoutHtml.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Zone.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_WidgetTemplate.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_Page.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Navigation.Table.sql"

sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CMS_WidgetBase.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleListWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleSummaryWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleTopWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleTypeDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ArticleTypeWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.CarouselWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.HtmlWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductCategoryWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductDetailWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ProductListWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.NavigationWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionGroup.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContent.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentCallToAction.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentTitle.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentImage.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.SectionContentParagraph.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.ImageWidget.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Language.Table.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "InitialData\dbo.Users.Table.sql"

@echo -----------------------------------------------------------------------------
@echo Database build completed.
goto done

:errors
@echo -----------------------------------------------------------------------------
@echo WARNING! Error(s) were detected!
goto done
:done
@pause