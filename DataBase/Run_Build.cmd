@echo off

set server=wayne
set dataBase=EasyCMS
set dbUserId=sa
set dbPassword=sa
set dbPath=E:\GitHub\Easy.CMS\Easy.CMS.Web\App_Data
@echo Please check the information below,then press "Enter" to confirm...

@echo Server=%server%
@echo DataBase=%dataBase%
@echo User Name=%dbUserId%
@echo PassWord=%dbPassword%
@echo DataBase Folder=%dbPath%

@pause

@echo Creating DataBase %dataBase%
sqlcmd -S %server% -d master -U %dbUserId% -P %dbPassword% -b -i "CreateDataBase.sql"

@echo Creating Tables...
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\Article.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleDetailWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleListWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleSummaryWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleTopWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleType.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleTypeDetailWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ArticleTypeWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\Carousel.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CarouselItem.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CarouselWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_Layout.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_LayoutHtml.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_Page.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_WidgetBase.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_WidgetTemplate.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\CMS_Zone.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\DataDictionary.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\HtmlWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ImageWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\Navigation.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\NavigationWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\Product.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ProductCategory.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ProductCategoryWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ProductDetailWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\ProductListWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionContent.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionContentCallToAction.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionContentImage.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionContentParagraph.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionContentTitle.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionGroup.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\SectionWidget.sql"
sqlcmd -S %server% -d %dataBase% -U %dbUserId% -P %dbPassword% -b -i "Tables\Users.sql"

@echo Finished...
@pause