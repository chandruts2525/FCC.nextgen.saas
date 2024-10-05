CREATE TABLE [MasterDataManagement.OrganizationStructure].[BusinessEntityContact]
(
	[BusinessEntityID] INT FOREIGN KEY REFERENCES [MasterDataManagement.OrganizationStructure].[BusinessEntity](BusinessEntityID) NOT NULL,
	[ContactTypeID] INT NOT NULL,
	[ContactName] [core].[Name] NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityContact_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_BusinessEntityContact_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_BusinessEntityContact_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_BusinessEntityContact_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_BusinessEntityContact_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_BusinessEntityContact] PRIMARY KEY CLUSTERED 
(
               [ContactTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
