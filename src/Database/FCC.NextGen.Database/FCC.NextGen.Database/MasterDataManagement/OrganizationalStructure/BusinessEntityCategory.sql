CREATE TABLE [MasterDataManagement.OrganizationStructure].[BusinessEntityCategory]
(
	[BusinessEntityCategoryId] INT IDENTITY(1,1) NOT NULL,
	[BusinessEntityCategoryName] [core].[Name] NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_BusinessEntityCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_BusinessEntityCategory_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_BusinessEntityCategory_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_BusinessEntityCategory_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_BusinessEntityCategory_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_BusinessEntityCategory] PRIMARY KEY CLUSTERED 
(
               [BusinessEntityCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
