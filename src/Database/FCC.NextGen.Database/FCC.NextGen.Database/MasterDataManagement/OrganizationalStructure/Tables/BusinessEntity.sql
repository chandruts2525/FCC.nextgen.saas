CREATE TABLE [MasterDataManagement.OrganizationStructure].[BusinessEntity]
(
	[BusinessEntityID] INT IDENTITY(1,1) NOT NULL,
	[BusinessEntityTypeID] INT  NOT NULL,
	[BusinessEntityName] [Core].[Name] NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntity_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_BusinessEntity_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_BusinessEntity_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_BusinessEntity_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_BusinessEntity_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_BusinessEntity] PRIMARY KEY CLUSTERED 
(
               [BusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
