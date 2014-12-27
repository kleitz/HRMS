USE master;
GO

-- ����Ѿ��������ݿ⣬����ɾ��
IF EXISTS(SELECT name from sys.databases WHERE name=N'hrdb')
BEGIN
    DROP DATABASE hrdb;
END

-- �������¹���ϵͳ���ݿ�
CREATE DATABASE hrdb
ON 
PRIMARY
(
    NAME = hrdb_dat,
	FILENAME = 'D:\��Ŀ\HRMS\HRMS.DB\hrdb.mdf',
	SIZE = 10MB,
	MAXSIZE = 100MB,
	FILEGROWTH = 1MB
)
LOG ON
(
    NAME = hrdb_log,
	FILENAME = 'D:\��Ŀ\HRMS\HRMS.DB\hrdb.ldf',
	SIZE = 5MB,
	MAXSIZE = 50MB,
	FILEGROWTH = 1MB
);
GO

USE hrdb;
GO

-- ���HR����ڣ���ɾ��
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[HR]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE [dbo].[HR];
GO

-- ����HR��¼��Ϣ��
CREATE TABLE HR
(
    Id uniqueidentifier PRIMARY KEY NOT NULL DEFAULT (newid()),
	UserName nvarchar(15) NOT NULL,
	[Password] nvarchar(35) NOT NULL,
	IsLocked bit NOT NULL DEFAULT 0,
	IsDeleted bit NOT NULL DEFAULT 0
);
GO

-- ��ӹ���Ա�˺�
INSERT INTO HR(UserName, [Password]) VALUES('admin', '32d4a8a0f3bd608e78df1fc3bd47bef5');

UPDATE HR SET IsLocked = 0;

SELECT Id, UserName, [Password], IsLocked, IsDeleted FROM HR WHERE IsDeleted = 0;


-- ���Department���Ѿ����ڣ���ɾ��
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[Department]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE [dbo].[Department];
GO


-- ����������Ϣ��
CREATE TABLE Department
(
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	Name nvarchar(15) NOT NULL,
	IsCanceled bit NOT NULL DEFAULT 0
);
GO

-- ��ʼ��������Ϣ��
INSERT INTO Department(Name) VALUES('������');
INSERT INTO Department(Name) VALUES('���̲�');
INSERT INTO Department(Name) VALUES('�з���');
INSERT INTO Department(Name) VALUES('�ʼ첿');
INSERT INTO Department(Name) VALUES('���ڲ�');
INSERT INTO Department(Name) VALUES('���²�');

SELECT Id, Name, IsCanceled FROM Department;



-- �������GeneralCategory����ɾ��
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[GeneralCategory]') AND OBJECTPROPERTY(id, 'IsUserTable')=1)
DROP TABLE [dbo].[GeneralCategory];
GO

-- ����GeneralCategory��
CREATE TABLE GeneralCategory
(
    Id int PRIMARY KEY NOT NULL,
	Name nvarchar(10) NOT NULL,
	Category nvarchar(10) NOT NULL
);
GO

-- ��ʼ��GeneralCategory��
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(10, '��', '�Ա�');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(11, 'Ů', '�Ա�');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(12, 'δ֪', '�Ա�');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(20, 'δ��', '����״��');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(21, '�ѻ�', '����״��');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(30, 'Ⱥ��', '������ò');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(31, '��Ա', '������ò');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(32, '��Ա', '������ò');

INSERT INTO GeneralCategory(Id, Name, Category) VALUES(40, 'ר��', 'ѧ��');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(41, '����', 'ѧ��');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(42, '˶ʿ', 'ѧ��');
INSERT INTO GeneralCategory(Id, Name, Category) VALUES(43, '��ʿ', 'ѧ��');

SELECT * FROM GeneralCategory;



-- ���Employee���Ѿ����ڣ���ɾ��
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[Employee]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE Employee;
GO

-- ����Employee��
CREATE TABLE Employee
(
    Id char(18) UNIQUE NOT NULL,
	Name nvarchar(20) NOT NULL,
	Gender int NOT NULL,
	Birthday date NOT NULL,
	EntryDate date NOT NULL,
	MaritalStatus int NOT NULL,
	PoliticalStatus int NOT NULL,
	Nation nvarchar(10) NOT NULL DEFAULT '����',
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

-- ��ʼ��Employee��
INSERT INTO Employee(Id, Name, Gender, Birthday, EntryDate, MaritalStatus, PoliticalStatus, Nation, 
					 Birthplace, Degree, Major, School, [Address], Email, Telephone, BankAccount, 
					 Department, JobTitle, StaffID, ContractPeriod, Remark, Salary)
			  VALUES('411526199001105423', '������', 10, '19900110', '20130308', 20, 30, '����', 
			         '����', 41, '�������', '��������ѧ', '�����к������������йش����԰', 
					 'hryspa-lmk@163.com', '13560169455', '620222558990888', 3, '�������ʦ', 
					 'RD0010', '2013.03.08 - 2015.03.08', 'Coding', 8000);

SELECT * FROM Employee WHERE IsFired = 0 AND Department IN
(SELECT Id FROM Department WHERE Id = 3);



-- �����SystemLog�Ѿ����ڣ���ɾ��
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SystemLog]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SystemLog;
GO

-- ����SystemLog��
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



-- �����SalarySheetHeader�Ѿ����ڣ�ɾ����
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SalarySheetHeader]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SalarySheetHeader;
GO

-- ����SalarySheetHeader��
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


-- �����SalarySheet���ڣ�ɾ����
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID(N'[dbo].[SalarySheet]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
DROP TABLE SalarySheet;
GO

-- ������SalarySheet
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


-- ��ѯ���ݿ����û���
SELECT name FROM dbo.sysobjects WHERE xtype='U' ORDER BY name;