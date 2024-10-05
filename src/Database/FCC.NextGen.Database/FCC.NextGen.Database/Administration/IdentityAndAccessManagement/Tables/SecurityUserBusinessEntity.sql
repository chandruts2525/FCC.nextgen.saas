CREATE TABLE [Administration.IdentityAndAccessManagement].[SecurityUserBusinessEntity]
(
    [UserBusinessEntityID] INT IDENTITY(1,1) NOT NULL,
	[UserID] INT FOREIGN KEY REFERENCES [Administration.IdentityAndAccessManagement].[SecurityUser](UserID) NOT NULL,
	[BusinessEntityID] INT NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityUserBusinessEntity_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_SecurityUserBusinessEntity_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_SecurityUserBusinessEntity_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_SecurityUserBusinessEntity_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_SecurityUserBusinessEntityy_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_SecurityUserBusinessEntity] PRIMARY KEY CLUSTERED 
(
               [UserBusinessEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]