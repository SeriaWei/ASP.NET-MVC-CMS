CREATE TABLE [Roles](
ID INT IDENTITY(1,1) PRIMARY KEY,
Title NVARCHAR(100),
[Description] NVARCHAR(500),
[Status] INT,
CreateBy NVARCHAR(255),
CreatebyName NVARCHAR(255),
CreateDate DATETIME,
LastUpdateBy NVARCHAR(255),
LastUpdateByName NVARCHAR(255),
LastUpdateDate DATETIME
)
GO
CREATE TABLE UserRoleRelation(
ID INT IDENTITY(1,1) PRIMARY KEY,
RoleID INT,
UserID NVARCHAR(255)
)
GO
CREATE TABLE [Permission](
PermissionKey NVARCHAR(100) PRIMARY KEY,
Title NVARCHAR(100),
[Description] NVARCHAR(500),
Module NVARCHAR(100),
RoleId INT,
[Status] INT,
CreateBy NVARCHAR(255),
CreatebyName NVARCHAR(255),
CreateDate DATETIME,
LastUpdateBy NVARCHAR(255),
LastUpdateByName NVARCHAR(255),
LastUpdateDate DATETIME
)
GO

SET IDENTITY_INSERT dbo.Roles ON
INSERT INTO dbo.Roles
        ( ID ,
          Title ,
          Description ,
          Status          
        )
VALUES  ( 1 ,
          N'超级管理员' , 
          N'超级管理员' , 
          1          
        )
SET IDENTITY_INSERT dbo.Roles OFF
GO

INSERT INTO dbo.UserRoleRelation( UserID,  RoleID)
SELECT TOP 1 UserID,1 FROM dbo.Users