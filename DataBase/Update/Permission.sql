CREATE TABLE [Roles] (
  [ID] INTEGER NOT NULL
, [Title] nvarchar(100) NULL
, [Description] nvarchar(500) NULL
, [Status] int NULL
, [CreateBy] nvarchar(255) NULL
, [CreatebyName] nvarchar(255) NULL
, [CreateDate] datetime NULL
, [LastUpdateBy] nvarchar(255) NULL
, [LastUpdateByName] nvarchar(255) NULL
, [LastUpdateDate] datetime NULL
, CONSTRAINT [PK__Roles__3214EC2756140FC0] PRIMARY KEY ([ID])
);
CREATE TABLE [UserRoleRelation] (
  [ID] INTEGER NOT NULL
, [RoleID] int NULL
, [UserID] nvarchar(255) NULL
, CONSTRAINT [PK__UserRole__3214EC27E329C91E] PRIMARY KEY ([ID])
);
CREATE TABLE [Permission] (
  [PermissionKey] nvarchar(100) NOT NULL
, [RoleId] int NOT NULL
, [Title] nvarchar(100) NULL
, [Description] nvarchar(500) NULL
, [Module] nvarchar(100) NULL
, [Status] int NULL
, [CreateBy] nvarchar(255) NULL
, [CreatebyName] nvarchar(255) NULL
, [CreateDate] datetime NULL
, [LastUpdateBy] nvarchar(255) NULL
, [LastUpdateByName] nvarchar(255) NULL
, [LastUpdateDate] datetime NULL
, CONSTRAINT [PK__Permission_PermissionKey_RoleId] PRIMARY KEY ([PermissionKey],[RoleId])
);

INSERT INTO [Roles] ([ID],[Title],[Description],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES (1,'超级管理员','超级管理员',1,NULL,NULL,NULL,'admin','ZKEASOFT','2016-08-01 14:12:07.433');
INSERT INTO UserRoleRelation( UserID,  RoleID)
SELECT UserID,1 FROM Users;

INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Article_Manage',1,'管理文章',NULL,'文章',NULL,'admin','ZKEASOFT','2016-08-01 13:18:40.493','admin','ZKEASOFT','2016-08-01 14:12:07.437');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Article_View',1,'查看文章',NULL,'文章',NULL,'admin','ZKEASOFT','2016-08-01 13:17:59.120','admin','ZKEASOFT','2016-08-01 14:12:07.447');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('ArticleType_Manage',1,'管理文章类别',NULL,'文章',NULL,'admin','ZKEASOFT','2016-08-01 13:22:24.887','admin','ZKEASOFT','2016-08-01 14:12:07.450');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('ArticleType_View',1,'查看文章类别',NULL,'文章',NULL,'admin','ZKEASOFT','2016-08-01 13:19:54.500','admin','ZKEASOFT','2016-08-01 14:12:07.453');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Carousel_Manage',1,'管理焦点图',NULL,'焦点图',NULL,'admin','ZKEASOFT','2016-08-01 13:45:46.190','admin','ZKEASOFT','2016-08-01 14:12:07.473');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Carousel_View',1,'查看焦点图',NULL,'焦点图',NULL,'admin','ZKEASOFT','2016-08-01 13:37:56.097','admin','ZKEASOFT','2016-08-01 14:12:07.477');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Layout_Manage',1,'管理布局',NULL,'布局',NULL,'admin','ZKEASOFT','2016-08-01 12:43:52.697','admin','ZKEASOFT','2016-08-01 14:12:07.480');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Layout_View',1,'查看布局',NULL,'布局',NULL,'admin','ZKEASOFT','2016-08-01 12:37:56.690','admin','ZKEASOFT','2016-08-01 14:12:07.483');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Media_Manage',1,'管理媒体库',NULL,'媒体库',NULL,'admin','ZKEASOFT','2016-08-01 14:12:07.553','admin','ZKEASOFT','2016-08-01 14:12:07.553');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Media_View',1,'查看媒体库',NULL,'媒体库',NULL,'admin','ZKEASOFT','2016-08-01 13:37:56.090','admin','ZKEASOFT','2016-08-01 14:12:07.483');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Navigation_Manage',1,'管理导航',NULL,'导航',NULL,'admin','ZKEASOFT','2016-08-01 13:40:38.230','admin','ZKEASOFT','2016-08-01 14:12:07.487');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Navigation_View',1,'查看导航',NULL,'导航',NULL,'admin','ZKEASOFT','2016-08-01 13:38:21.943','admin','ZKEASOFT','2016-08-01 14:12:07.493');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Page_Manage',1,'管理页面',NULL,'页面',NULL,'admin','ZKEASOFT','2016-08-01 13:41:08.463','admin','ZKEASOFT','2016-08-01 14:12:07.497');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Page_View',1,'查看页面',NULL,'页面',NULL,'admin','ZKEASOFT','2016-08-01 12:37:56.683','admin','ZKEASOFT','2016-08-01 14:12:07.500');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Product_Manage',1,'管理产品',NULL,'产品',NULL,'admin','ZKEASOFT','2016-08-01 13:27:58.400','admin','ZKEASOFT','2016-08-01 14:12:07.507');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Product_View',1,'查看产品',NULL,'产品',NULL,'admin','ZKEASOFT','2016-08-01 13:27:41.483','admin','ZKEASOFT','2016-08-01 14:12:07.513');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('ProductCategory_Manage',1,'管理产品类别',NULL,'产品',NULL,'admin','ZKEASOFT','2016-08-01 13:28:21.047','admin','ZKEASOFT','2016-08-01 14:12:07.517');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('ProductCategory_View',1,'查看产品类别',NULL,'产品',NULL,'admin','ZKEASOFT','2016-08-01 13:28:08.737','admin','ZKEASOFT','2016-08-01 14:12:07.520');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Role_Manage',1,'管理角色',NULL,'用户/安全',NULL,'admin','ZKEASOFT','2016-08-01 14:12:07.567','admin','ZKEASOFT','2016-08-01 14:12:07.567');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Role_View',1,'查看角色',NULL,'用户/安全',NULL,'admin','ZKEASOFT','2016-08-01 13:37:46.660','admin','ZKEASOFT','2016-08-01 14:12:07.527');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Theme_Manage',1,'管理主题',NULL,'主题',NULL,'admin','ZKEASOFT','2016-08-01 13:42:48.600','admin','ZKEASOFT','2016-08-01 14:12:07.533');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('Theme_View',1,'查看主题',NULL,'主题',NULL,'admin','ZKEASOFT','2016-08-01 13:37:56.083','admin','ZKEASOFT','2016-08-01 14:12:07.537');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('User_Manage',1,'管理用户',NULL,'用户/安全',NULL,'admin','ZKEASOFT','2016-08-01 14:12:07.567','admin','ZKEASOFT','2016-08-01 14:12:07.567');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('User_View',1,'查看用户',NULL,'用户/安全',NULL,'admin','ZKEASOFT','2016-08-01 13:37:46.647','admin','ZKEASOFT','2016-08-01 14:12:07.540');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('SearchEngine_Manage',1,'搜索引擎优化管理',NULL,'搜索引擎',NULL,'admin','ZKEASOFT','2016-08-01 14:12:07.567','admin','ZKEASOFT','2016-08-01 14:12:07.567');
INSERT INTO [Permission] ([PermissionKey],[RoleId],[Title],[Description],[Module],[Status],[CreateBy],[CreatebyName],[CreateDate],[LastUpdateBy],[LastUpdateByName],[LastUpdateDate]) VALUES ('AddedValueService_Manage',1,'增值服务管理',NULL,'增值服务',NULL,'admin','ZKEASOFT','2016-08-01 13:37:46.647','admin','ZKEASOFT','2016-08-01 14:12:07.540');


DELETE FROM [Language] WHERE LanKey LIKE 'UserEntity@Roles';
INSERT INTO [Language]( LanKey ,LanID ,LanValue ,Module ,LanType) VALUES ( 'UserEntity@Roles' , 2052 , '角色' , 'UserEntity' , 'EntityProperty' );

DELETE FROM [Language] WHERE LanKey LIKE 'UserRoleRelation@RoleID';
INSERT INTO [Language]( LanKey ,LanID ,LanValue ,Module ,LanType) VALUES ( 'UserRoleRelation@RoleID' , 2052 , '角色' , 'UserRoleRelation' , 'EntityProperty' );
