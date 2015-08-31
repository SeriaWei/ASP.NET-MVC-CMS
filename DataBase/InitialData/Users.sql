IF NOT EXISTS (SELECT * FROM Users AS u WHERE u.UserID=N'admin')
BEGIN
	INSERT INTO Users(UserID,[PassWord]) VALUES (N'admin',N'/4Fcc4D1YLMSTIudqNzdAA==')
END
GO
