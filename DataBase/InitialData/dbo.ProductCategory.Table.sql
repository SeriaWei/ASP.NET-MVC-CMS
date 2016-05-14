SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (1, N'ZKEASOFT', NULL, 0, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:32.553' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:32.553' AS DateTime))
INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (2, N'开源软件', NULL, 1, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:50.573' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:50.573' AS DateTime))
INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (3, N'免费软件', NULL, 1, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:07.140' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:07.140' AS DateTime))
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
