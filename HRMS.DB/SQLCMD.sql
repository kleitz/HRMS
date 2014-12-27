USE master;
GO

-- 如果已经存在数据库，则将其删除
IF EXISTS(SELECT name from sys.databases WHERE name=N'hrdb')
BEGIN
    DROP DATABASE hrdb;
END

-- 创建人事管理系统数据库
CREATE DATABASE hrdb
ON 
PRIMARY
(
    NAME = hrdb_dat,
	FILENAME = 'D:\项目\HRMS\HRMS.DB\hrdb.mdf',
	SIZE = 10MB,
	MAXSIZE = 100MB,
	FILEGROWTH = 1MB
)
LOG ON
(
    NAME = hrdb_log,
	FILENAME = 'D:\项目\HRMS\HRMS.DB\hrdb.ldf',
	SIZE = 5MB,
	MAXSIZE = 50MB,
	FILEGROWTH = 1MB
);
GO

USE hrdb;
GO

-- 如果HR表存在，则删除
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[HR]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE [dbo].[HR];
GO

-- 创建HR登录信息表
CREATE TABLE HR
(
    Id uniqueidentifier PRIMARY KEY NOT NULL DEFAULT (newid()),
	UserName nvarchar(15) NOT NULL,
	[Password] nvarchar(35) NOT NULL,
	IsLocked bit NOT NULL DEFAULT 0,
	IsDeleted bit NOT NULL DEFAULT 0
);
GO

-- 添加管理员账号
INSERT INTO HR(UserName, [Password]) VALUES('admin', '32d4a8a0f3bd608e78df1fc3bd47bef5');

UPDATE HR SET IsLocked = 0;

SELECT Id, UserName, [Password], IsLocked, IsDeleted FROM HR WHERE IsDeleted = 0;


-- 如果Department表已经存在，则删除
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[Department]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE [dbo].[Department];
GO


-- 创建部门信息表
CREATE TABLE Department
(
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	Name nvarchar(15) NOT NULL,
	IsCanceled bit NOT NULL DEFAULT 0
);
GO

-- 初始化部门信息表
INSERT INTO Department(Name) VALUES('行政部');
INSERT INTO Department(Name) VALUES('工程部');
INSERT INTO Department(Name) VALUES('研发部');
INSERT INTO Department(Name) VALUES('质检部');
INSERT INTO Department(Name) VALUES('后勤部');
INSERT INTO Department(Name) VALUES('人事部');

SELECT Id, Name, IsCanceled FROM Department;



-- 如果存在GeneralCategory表，则删除
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[GeneralCategory]') AND OBJECTPROPERTY(id, 'IsUserTable')=1)
DROP TABLE [dbo].[GeneralCategory];
GO

-- 创建GeneralCategory表
CREATE TABLE GeneralCategory
(
    Id int PRIMARY KEY NOT NULL,
	Name nvarchar(10) NOT NULL,
	Category nvarchar(10) NOT NULL
);
GO

-- 初始化GeneralCategory表
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(10, '男', '性别');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(11, '女', '性别');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(12, '未知', '性别');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(20, '未婚', '婚姻状况');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(21, '已婚', '婚姻状况');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(30, '群众', '政治面貌');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(31, '团员', '政治面貌');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(32, '党员', '政治面貌');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(40, '专科', '学历');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(41, '本科', '学历');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(42, '硕士', '学历');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(43, '博士', '学历');

SELECT * FROM GeneralCategory;



-- 如果Employee表已经存在，则删除
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[Employee]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE Employee;
GO

-- 创建Employee表
CREATE TABLE Employee
(
    Id char(18) UNIQUE NOT NULL,
	Name nvarchar(20) NOT NULL,
	Gender int NOT NULL,
	Birthday date NOT NULL,
	EntryDate date NOT NULL,
	MaritalStatus int NOT NULL,
	PoliticalStatus int NOT NULL,
	Nation nvarchar(10) NOT NULL DEFAULT '汉族',
	Birthplace nvarchar(20) NOT NULL,
	Degree int NOT NULL,
	Major nvarchar(20) NOT NULL,
	School nvarchar(20) NOT NULL,
	[Address] nvarchar(80) NOT NULL,
	Email nvarchar(50) NULL,
	Telephone varchar(12) NOT NULL,
	BankAccount varchar(20) NOT NULL,
	Department int NOT NULL,
	JobTitle nvarchar(15) NOT NULL,
	StaffID varchar(10) PRIMARY KEY NOT NULL,
	ContractPeriod char(23) NOT NULL,
	Remark nvarchar(100) NULL,
	Salary int NOT NULL,
	IsFired bit NOT NULL DEFAULT 0 
);
GO

-- 初始化Employee表
INSERT INTO Employee(Id, Name, Gender, Birthday, EntryDate, MaritalStatus, PoliticalStatus, Nation, 
					 Birthplace, Degree, Major, School, [Address], Email, Telephone, BankAccount, 
					 Department, JobTitle, StaffID, ContractPeriod, Remark, Salary)
			  VALUES('411526199001105423', '刘明坤', 10, '19900110', '20130308', 20, 30, '汉族', 
			         '河南', 41, '软件工程', '华南理工大学', '北京市海淀区西二旗中关村软件园', 
					 'hryspa-lmk@163.com', '13560169455', '620222558990888', 3, '软件工程师', 
					 'RD0010', '2013.03.08 - 2015.03.08', 'Coding', 8000);

SELECT * FROM Employee WHERE IsFired = 0 AND Department IN
(SELECT Id FROM Department WHERE Id = 3);



-- 如果表SystemLog已经存在，则删除
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SystemLog]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SystemLog;
GO

-- 创建SystemLog表
CREATE TABLE SystemLog
(
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[Type] nchar(4) NOT NULL,
	Operator nvarchar(15) NOT NULL,
	TableName nvarchar(15) NOT NULL,
	PrimaryKey nvarchar(30) NOT NULL,
	Describe nvarchar(50) NOT NULL,
	[Time] datetime NOT NULL
);
GO



-- 如果表SalarySheetHeader已经存在，删除它
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SalarySheetHeader]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SalarySheetHeader;
GO

-- 创建SalarySheetHeader表
CREATE TABLE SalarySheetHeader
(
    Id uniqueidentifier PRIMARY KEY NOT NULL,
	[Year] int NOT NULL,
	[Month] int NOT NULL,
	DepartmentId int NOT NULL,
	IsSettled bit NOT NULL DEFAULT 0
);
GO

SELECT * FROM SalarySheetHeader;


-- 如果表SalarySheet存在，删除它
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SalarySheet]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SalarySheet;
GO

-- 创建表SalarySheet
CREATE TABLE SalarySheet
(
    StaffID varchar(10) NOT NULL,
	Name nvarchar(20) NOT NULL, 
	SheetID uniqueidentifier NOT NULL
	        FOREIGN KEY REFERENCES SalarySheetHeader(Id),
    BaseSalary int NOT NULL,
	Bonus int NOT NULL,
	Fine int NOT NULL
);
GO

SELECT * FROM SalarySheet;


-- 查询数据库中用户表
SELECT name FROM dbo.sysobjects WHERE xtype='U' ORDER BY name;