SET IDENTITY_INSERT [dbo].[CMS_WidgetTemplate] ON 

INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (1, N'HTML组件', N'1.通用', N'Widget.HTML', N'Easy.Web.CMS', N'Easy.Web.CMS.Widget.HtmlWidgetService', N'Easy.Web.CMS.Widget.HtmlWidget', N'~/Content/Images/Widget.HTML.png', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (2, N'导航', N'1.通用', N'Widget.Navigation', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.NavigationWidgetService', N'Easy.CMS.Common.Models.NavigationWidget', N'~/Content/Images/Widget.Navigation.png', 2, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (3, N'文章列表', N'2.文章', N'Widget.ArticleList', N'Easy.CMS.Article', N'Easy.CMS.Article.Service.ArticleListWidgetService', N'Easy.CMS.Article.Models.ArticleListWidget', N'~/Content/Images/Widget.ArticleList.png', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (4, N'文章内容', N'2.文章', N'Widget.ArticleDetail', N'Easy.CMS.Article', N'Easy.CMS.Article.Service.ArticleDetailWidgetService', N'Easy.CMS.Article.Models.ArticleDetailWidget', N'~/Content/Images/Widget.ArticleDetail.png', 3, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (5, N'焦点图', N'1.通用', N'Widget.Carousel', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.CarouselWidgetService', N'Easy.CMS.Common.Models.CarouselWidget', N'~/Content/Images/Widget.Carousel.png', 3, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (6, N'置顶文章', N'2.文章', N'Widget.ArticleTops', N'Easy.CMS.Article', N'Easy.CMS.Article.Service.ArticleTopWidgetService', N'Easy.CMS.Article.Models.ArticleTopWidget', N'~/Content/Images/Widget.ArticleTops.png', 4, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (7, N'文章概览', N'2.文章', N'Widget.ArticleSummary', N'Easy.CMS.Article', N'Easy.CMS.Article.Service.ArticleSummaryWidgetService', N'Easy.CMS.Article.Models.ArticleSummaryWidget', N'~/Content/Images/Widget.ArticleSummary.png', 5, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (9, N'产品列表', N'3.产品', N'Widget.ProductList', N'Easy.CMS.Product', N'Easy.CMS.Product.Service.ProductListWidgetService', N'Easy.CMS.Product.Models.ProductListWidget', N'~/Content/Images/Widget.ProductList.png', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (10, N'产品类别', N'3.产品', N'Widget.ProductCategory', N'Easy.CMS.Product', N'Easy.CMS.Product.Service.ProductCategoryWidgetService', N'Easy.CMS.Product.Models.ProductCategoryWidget', N'~/Content/Images/Widget.ProductCategory.png', 2, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (11, N'文章类别', N'2.文章', N'Widget.ArticleType', N'Easy.CMS.Article', N'Easy.CMS.Article.Service.ArticleTypeWidgetService', N'Easy.CMS.Article.Models.ArticleTypeWidget', N'~/Content/Images/Widget.ArticleType.png', 2, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (12, N'图片', N'1.通用', N'Widget.Image', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.ImageWidgetService', N'Easy.CMS.Common.Models.ImageWidget', N'~/Content/Images/Widget.Image.png', 4, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (13, N'产品内容', N'3.产品', N'Widget.ProductDetail', N'Easy.CMS.Product', N'Easy.CMS.Product.Service.ProductDetailWidgetService', N'Easy.CMS.Product.Models.ProductDetailWidget', N'~/Content/Images/Widget.ProductDetail.png', 3, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (14, N'模版组件', N'1.通用', N'Widget.Section', N'Easy.CMS.Section', N'Easy.CMS.Section.Service.SectionWidgetService', N'Easy.CMS.Section.Models.SectionWidget', N'~/Content/Images/Widget.Section.png', 6, NULL, 1, N'SectionWidgetForm', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (15, N'脚本', N'1.通用', N'Widget.Script', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.ScriptWidgetService', N'Easy.CMS.Common.Models.ScriptWidget', N'~/Content/Images/Widget.Script.png', 7, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (16, N'视频', N'1.通用', N'Widget.Video', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.VideoWidgetService', N'Easy.CMS.Common.Models.VideoWidget', N'~/Content/Images/Widget.Video.png', 5, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[CMS_WidgetTemplate] ([ID], [Title], [GroupName], [PartialView], [AssemblyName], [ServiceTypeName], [ViewModelTypeName], [Thumbnail], [Order], [Description], [Status], [FormView], [StyleClass], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (18, N'样式', N'1.通用', N'Widget.StyleSheet', N'Easy.CMS.Common', N'Easy.CMS.Common.Service.StyleSheetWidgetService', N'Easy.CMS.Common.Models.StyleSheetWidget', N'~/Content/Images/Widget.StyleSheet.png', 8, N'', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CMS_WidgetTemplate] OFF
