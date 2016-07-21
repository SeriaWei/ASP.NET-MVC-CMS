CREATE TABLE [dbo].[StyleSheetWidget](
	[ID] [nvarchar](100) NOT NULL,
	[StyleSheet] [nvarchar](max) NULL,
 CONSTRAINT [PK_StyleSheetWidget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[StyleSheetWidget]  WITH CHECK ADD  CONSTRAINT [FK_StyleSheetWidget_CMS_WidgetBase] FOREIGN KEY([ID])
REFERENCES [dbo].[CMS_WidgetBase] ([ID])
GO
ALTER TABLE [dbo].[StyleSheetWidget] CHECK CONSTRAINT [FK_StyleSheetWidget_CMS_WidgetBase]
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
VALUES  ( N'样式' , 
          N'1.通用' ,
          N'Widget.StyleSheet' , 
          N'Easy.CMS.Common' , 
          N'Easy.CMS.Common.Service.StyleSheetWidgetService' ,
          N'Easy.CMS.Common.Models.StyleSheetWidget' ,
          N'~/Content/Images/Widget.StyleSheet.png' ,
          8 , -- Order - int
          N'' ,
          1
        )

DELETE dbo.Language WHERE LanKey=N'StyleSheetWidget@StyleSheet'
INSERT INTO dbo.Language
		( LanKey ,
		    LanID ,
		    LanValue ,
		    Module ,
		    LanType
		)
VALUES  ( N'StyleSheetWidget@StyleSheet' , 
		    2052 , 
		    N'样式' , 
		    N'StyleSheetWidget' , 
		    N'EntityProperty'  
		)