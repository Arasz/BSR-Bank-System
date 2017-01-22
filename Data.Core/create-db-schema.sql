CREATE TABLE [User] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(30) NOT NULL INDEX IX_User_Name NONCLUSTERED,
	Password char(48) NOT NULL,  -- 48 characters because Base64(20 bytes for password + 16 bytes for salt).Length == 48 
	CONSTRAINT Unique_Name UNIQUE(Name),
);

CREATE TABLE [Account] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	Number char(26) NOT NULL INDEX IX_Account_Number NONCLUSTERED,
	Balance money NOT NULL,
	UserId bigint NOT NULL INDEX IX_Account_UserId NONCLUSTERED,
	CONSTRAINT FK_AccountOwner_UserId FOREIGN KEY (UserId) REFERENCES [User] (Id),
	CONSTRAINT Unique_Number UNIQUE(Number),
);

CREATE TABLE [Operation] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	AccountId bigint FOREIGN KEY REFERENCES [Account](Id),
	Title varchar(200) NOT NULL,
	Source char(26) NOT NULL,
	Target char(26) NOT NULL,
	Credit money NOT NULL, -- ma
	Debit money NOT NULL, -- winien
	Amount money NOT NULL,
	Balance money NOT NULL,
	Type varchar(50) NOT NULL,
	CreationDate DateTime NOT NULL INDEX IX_Operation_CreationDate NONCLUSTERED,
);