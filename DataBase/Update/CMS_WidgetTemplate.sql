CREATE TABLE [StyleSheetWidget] (
  [ID] nvarchar(100) NOT NULL
, [StyleSheet] ntext NULL
, CONSTRAINT [PK_StyleSheetWidget] PRIMARY KEY ([ID])
, FOREIGN KEY ([ID]) REFERENCES [CMS_WidgetBase] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);


INSERT INTO CMS_WidgetTemplate
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
VALUES  ( '样式' , 
		  '1.通用' ,
		  'Widget.StyleSheet' , 
		  'Easy.CMS.Common' , 
		  'Easy.CMS.Common.Service.StyleSheetWidgetService' ,
		  'Easy.CMS.Common.Models.StyleSheetWidget' ,
		  '~/Content/Images/Widget.StyleSheet.png' ,
		  8 , -- Order - int
		  '' ,
		  1
		);

DELETE FROM [Language] WHERE LanKey='StyleSheetWidget@StyleSheet';
INSERT INTO [Language]
		( LanKey ,
		    LanID ,
		    LanValue ,
		    Module ,
		    LanType
		)
VALUES  ( 'StyleSheetWidget@StyleSheet' , 
		    2052 , 
		    '样式' , 
		    'StyleSheetWidget' , 
		    'EntityProperty'  
		);