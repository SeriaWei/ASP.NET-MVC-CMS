CREATE TABLE BreadcrumbWidget
    (
      ID NVARCHAR(100) PRIMARY KEY
                       NOT NULL ,
      IsLinkAble BIT NULL
    );
GO
INSERT INTO dbo.CMS_WidgetTemplate
        ( Title ,
          GroupName ,
          PartialView ,
          AssemblyName ,
          ServiceTypeName ,
          ViewModelTypeName ,
          Thumbnail ,
          [Order] ,
          Status
        )
VALUES  ( N'路径导航' ,
          N'1.通用' , 
          N'Widget.Breadcrumb' ,
          N'Easy.CMS.Breadcrumb' , 
          N'Easy.CMS.Breadcrumb.Service.BreadcrumbWidgetService' , 
          N'Easy.CMS.Breadcrumb.Models.BreadcrumbWidget' , 
          N'~/Modules/Breadcrumb/Content/breadcrumb.png' , 
          6 , 
          1
        )

GO

INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@ActionType', 2052, N'ActionType', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@AssemblyName', 2052, N'AssemblyName', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@CreateBy', 2052, N'CreateBy', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@CreatebyName', 2052, N'创建人', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@CreateDate', 2052, N'创建日期', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@CustomClass', 2052, N'CustomClass', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@CustomStyle', 2052, N'CustomStyle', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@Description', 2052, N'描述', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@FormView', 2052, N'FormView', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@ID', 2052, N'ID', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@IsLinkAble', 2052, N'IsLinkAble', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@IsSystem', 2052, N'IsSystem', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@IsTemplate', 2052, N'保存为模板', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@LastUpdateBy', 2052, N'LastUpdateBy', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@LastUpdateByName', 2052, N'更新人', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@LastUpdateDate', 2052, N'更新日期', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@LayoutID', 2052, N'布局', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@PageID', 2052, N'页面', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@PartialView', 2052, N'模版', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@Position', 2052, N'排序', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@ServiceTypeName', 2052, N'ServiceTypeName', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@Status', 2052, N'状态', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@StyleClass', 2052, N'自定义样式', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@Thumbnail', 2052, N'模板缩略图', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@Title', 2052, N'标题', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@ViewModelTypeName', 2052, N'ViewModelTypeName', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@WidgetName', 2052, N'组件名称', N'BreadcrumbWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'BreadcrumbWidget@ZoneID', 2052, N'区域', N'BreadcrumbWidget', N'EntityProperty')
GO
