CREATE TABLE [User] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(30) NOT NULL,
	Password char(48) NOT NULL,  -- 48 characters because Base64(20 bytes for password + 16 bytes for salt).Length == 48 
	Token char(48) NOT NULL,
);

CREATE TABLE [Account] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	Number char(26) NOT NULL,
	Balance money NOT NULL,
	Owner bigint NOT NULL INDEX IX_Account_Owner NONCLUSTERED,
	CONSTRAINT FK_AccountOwner_UserId FOREIGN KEY (Owner) REFERENCES [User] (Id),
);

CREATE TABLE [Operation] 
(
	Id bigint PRIMARY KEY IDENTITY(1,1),
	Title varchar(200) NOT NULL,
	AccountNumber char(20) NOT NULL,
	Amount money NOT NULL,
	Balance money NOT NULL,
	Type varchar(50) NOT NULL, -- maybe will be changed for int (enum)
	CreationDate DateTime NOT NULL INDEX IX_Operation_CreationDate NONCLUSTERED,
);