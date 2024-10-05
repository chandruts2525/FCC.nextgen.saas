CREATE TABLE [MasterDataManagement.OrganizationStructure].[BusinessEntityPerson]
(
    [BusinessEntityID] INT FOREIGN KEY REFERENCES [MasterDataManagement.OrganizationStructure].[BusinessEntity](BusinessEntityID) NOT NULL,
	[BusinessEntityTypeId] INT FOREIGN KEY REFERENCES [MasterDataManagement.OrganizationStructure].[BusinessEntityType](BusinessEntityTypeID) NOT NULL,
	[NameStyle] BIT NOT NULL,
	[Title] NVARCHAR(8) NULL,
	[FirstName] [Core].[Name] NOT NULL,
	[MiddleName] [Core].[Name] NULL,
	[LastName] [Core].[Name] NOT NULL,
	[Suffix] NVARCHAR(10) NULL,
	[AdditionalContactInfo] XML NULL,
	[Demographics] XML NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityPerson_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_BusinessEntityPerson_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_BusinessEntityPerson_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_BusinessEntityPerson_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_BusinessEntityPerson_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
)