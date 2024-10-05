CREATE TABLE [MasterDataManagement.OrganizationStructure].[BusinessEntityType]
(
	[BusinessEntityTypeId] INT IDENTITY(1,1) NOT NULL,
	[BusinessEntityCategoryId] INT NOT NULL,
	[BusinessEntityTypeName] nvarchar(100) NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_BusinessEntityType_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_BusinessEntityType_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_BusinessEntityType_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_BusinessEntityType_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_BusinessEntityType] PRIMARY KEY CLUSTERED 
(
               [BusinessEntityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]