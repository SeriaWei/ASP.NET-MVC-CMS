SET IDENTITY_INSERT [dbo].[ArticleType] ON 

INSERT [dbo].[ArticleType] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (1, N'ZKEASOFT', NULL, 0, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:18.183' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:18.183' AS DateTime))
INSERT [dbo].[ArticleType] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (2, N'ZKEACMS', NULL, 1, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:24.810' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:24.810' AS DateTime))
INSERT [dbo].[ArticleType] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (3, N'EasyFrameWork', NULL, 1, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:29.843' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:29.843' AS DateTime))
SET IDENTITY_INSERT [dbo].[ArticleType] OFF
