TRUNCATE TABLE dbo.DataDictionary
GO
SET IDENTITY_INSERT [dbo].[DataDictionary] ON 

INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (1, N'RecordStatus', N'有效', N'1', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (2, N'RecordStatus', N'无效', N'2', 2, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (3, N'ArticleCategory', N'新闻', N'1', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (4, N'ArticleCategory', N'公司新闻', N'2', 2, 3, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (5, N'ArticleCategory', N'行业新闻', N'3', 3, 3, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (6, N'UserEntity@Sex', N'男', N'1', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (7, N'UserEntity@Sex', N'女', N'2', 2, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (8, N'UserEntity@MaritalStatus', N'未婚', N'1', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (9, N'UserEntity@MaritalStatus', N'已婚', N'2', 2, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (11, N'UserEntity@UserTypeCD', N'超级管理员', N'1', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (12, N'ArticleTopWidget@PartialView', N'默认', N'Widget.ArticleTops', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (13, N'ArticleListWidget@PartialView', N'默认', N'Widget.ArticleList', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (14, N'ArticleListWidget@PartialView', N'横幅', N'Widget.ArticleList-Snippet', 2, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (15, N'ProductListWidget@PartialView', N'默认', N'Widget.ProductList', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (16, N'ProductListWidget@Columns', N'3 列', N'col-xs-12 col-sm-6 col-md-4', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (17, N'ProductListWidget@Columns', N'4 列', N'col-xs-12 col-sm-6 col-md-4 col-lg-3', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (18, N'CarouselWidget@PartialView', N'默认', N'Widget.Carousel', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (19, N'ArticleSummaryWidget@Style', N'默认', N'bs-callout-default', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (20, N'ArticleSummaryWidget@Style', N'危险', N'bs-callout-danger', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (21, N'ArticleSummaryWidget@Style', N'警告', N'bs-callout-warning', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (22, N'ArticleSummaryWidget@Style', N'信息', N'bs-callout-info', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (23, N'ArticleSummaryWidget@Style', N'成功', N'bs-callout-success', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (24, N'ArticleSummaryWidget@PartialView', N'默认', N'Widget.ArticleSummary', 1, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[DataDictionary] ([ID], [DicName], [Title], [DicValue], [Order], [Pid], [IsSystem], [ImageUrl], [ImageThumbUrl], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (25, N'CarouselWidget@PartialView', N'横幅', N'Widget.CarouselFullSlide', 2, 0, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DataDictionary] OFF
GO

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
                  N'根结点' ,
                  N'NavigationWidget' ,
                  N'EntityProperty' 
                );
    END;
