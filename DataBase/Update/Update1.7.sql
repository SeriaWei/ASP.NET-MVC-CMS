IF EXISTS ( SELECT  *
            FROM    syscolumns
            WHERE   id = OBJECT_ID('Article')
                    AND name = 'ArticleCategoryID' )
    BEGIN
        EXEC sys.sp_rename @objname = N'Article.ArticleCategoryID',
            @newname = N'ArticleTypeID', @objtype = 'COLUMN';
    END;
	GO

IF EXISTS ( SELECT  *
            FROM    syscolumns
            WHERE   id = OBJECT_ID('Product')
                    AND name = 'ProductCategory' )
    BEGIN
        EXEC sys.sp_rename @objname = N'Product.ProductCategory',
            @newname = N'ProductCategoryID', @objtype = 'COLUMN';
    END;
	GO
