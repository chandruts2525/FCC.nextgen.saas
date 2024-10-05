CREATE TABLE [Administration.IdentityandAccessManagement].[SecurityRoleAttachment]
(
    [RoleUserMappingID] INT IDENTITY(1,1) NOT NULL,
	[url] NVARCHAR(500) NOT NULL,
	[FileName] NVARCHAR(100) NOT NULL,
	[RoleID] INT  NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityRoleAttachment_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_SecurityRoleAttachment_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_SecurityRoleAttachment_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_SecurityRoleAttachment_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_SecurityRoleAttachment_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy][Core].[User] NULL
 CONSTRAINT [PK_SecurityRoleAttachment] PRIMARY KEY CLUSTERED 
(
               [RoleUserMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]