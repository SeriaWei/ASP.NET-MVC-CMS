IF NOT EXISTS(select 1 FROM syscolumns where id=object_id('CMS_WidgetBase') and name='ExtendData')
BEGIN
	ALTER TABLE dbo.CMS_WidgetBase ADD ExtendData NVARCHAR(max)
END
GO
CREATE TABLE [dbo].[CMS_Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PostMessage] [nvarchar](max) NOT NULL,
	[Reply] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreatebyName] [nvarchar](100) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateBy] [nvarchar](50) NULL,
	[LastUpdateByName] [nvarchar](100) NULL,
	[LastUpdateDate] [datetime] NULL,
	[Description] [nchar](200) NULL,
 CONSTRAINT [PK_CMS_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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
          Description ,
          Status
        )
SELECT N'留言板' ,N'1.通用' , N'Widget.Message' ,N'Easy.CMS.Message' , N'Easy.CMS.Message.Service.MessageWidgetService' , N'Easy.CMS.Message.Models.MessageWidget' ,N'~/Content/Images/Widget.Message.png' , 9 , N'' ,1 UNION ALL 
SELECT N'留言箱' ,N'1.通用' , N'Widget.MessageBox' ,N'Easy.CMS.Message' , N'Easy.CMS.Message.Service.MessageBoxWidgetService' , N'Easy.CMS.Message.Models.MessageBoxWidget' ,N'~/Content/Images/Widget.MessageBox.png' , 10 , N'' ,1

GO
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ActionType', 2052, N'ActionType', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@AssemblyName', 2052, N'AssemblyName', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@CreateBy', 2052, N'CreateBy', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@CreatebyName', 2052, N'创建人', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@CreateDate', 2052, N'创建日期', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@CustomClass', 2052, N'CustomClass', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@CustomStyle', 2052, N'CustomStyle', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@Description', 2052, N'描述', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ExtendData', 2052, N'ExtendData', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ExtendFields', 2052, N'扩展属性', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@FormView', 2052, N'FormView', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ID', 2052, N'ID', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@IsSystem', 2052, N'IsSystem', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@IsTemplate', 2052, N'保存为模板', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@LastUpdateBy', 2052, N'LastUpdateBy', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@LastUpdateByName', 2052, N'更新人', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@LastUpdateDate', 2052, N'更新日期', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@LayoutID', 2052, N'LayoutID', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@PageID', 2052, N'PageID', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@PartialView', 2052, N'显示模板', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@Position', 2052, N'排序', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ServiceTypeName', 2052, N'ServiceTypeName', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@Status', 2052, N'状态', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@StyleClass', 2052, N'自定义样式', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@Thumbnail', 2052, N'模板缩略图', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@Title', 2052, N'标题', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ViewModelTypeName', 2052, N'ViewModelTypeName', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@WidgetName', 2052, N'组件名称', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageBoxWidget@ZoneID', 2052, N'区域', N'MessageBoxWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@ActionType', 2052, N'ActionType', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@CreateBy', 2052, N'CreateBy', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@CreatebyName', 2052, N'创建人', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@CreateDate', 2052, N'创建日期', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@Description', 2052, N'描述', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@Email', 2052, N'邮箱', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@ID', 2052, N'ID', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@LastUpdateBy', 2052, N'LastUpdateBy', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@LastUpdateByName', 2052, N'更新人', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@LastUpdateDate', 2052, N'更新日期', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@PostMessage', 2052, N'留言内容', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@Reply', 2052, N'回复', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@Status', 2052, N'状态', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageEntity@Title', 2052, N'姓名', N'MessageEntity', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ActionType', 2052, N'ActionType', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@AssemblyName', 2052, N'AssemblyName', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@CreateBy', 2052, N'CreateBy', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@CreatebyName', 2052, N'创建人', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@CreateDate', 2052, N'创建日期', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@CustomClass', 2052, N'CustomClass', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@CustomStyle', 2052, N'CustomStyle', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@Description', 2052, N'描述', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ExtendData', 2052, N'ExtendData', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ExtendFields', 2052, N'扩展属性', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@FormView', 2052, N'FormView', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ID', 2052, N'ID', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@IsSystem', 2052, N'IsSystem', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@IsTemplate', 2052, N'保存为模板', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@LastUpdateBy', 2052, N'LastUpdateBy', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@LastUpdateByName', 2052, N'更新人', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@LastUpdateDate', 2052, N'更新日期', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@LayoutID', 2052, N'LayoutID', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@PageID', 2052, N'PageID', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@PartialView', 2052, N'显示模板', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@Position', 2052, N'排序', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ServiceTypeName', 2052, N'ServiceTypeName', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@Status', 2052, N'状态', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@StyleClass', 2052, N'自定义样式', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@Thumbnail', 2052, N'模板缩略图', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@Title', 2052, N'标题', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ViewModelTypeName', 2052, N'ViewModelTypeName', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@WidgetName', 2052, N'组件名称', N'MessageWidget', N'EntityProperty')
INSERT [dbo].[Language] ([LanKey], [LanID], [LanValue], [Module], [LanType]) VALUES (N'MessageWidget@ZoneID', 2052, N'区域', N'MessageWidget', N'EntityProperty')
GO