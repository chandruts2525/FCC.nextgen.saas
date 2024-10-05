CREATE TABLE [Administration.IdentityAndAccessManagement].[SecurityUser](
    [UserID] INT IDENTITY(1,1) NOT NULL,
	[LoginEmail] NVARCHAR(100) NOT NULL,
	[FirstName] [Core].[Name] NOT NULL,
	[LastName] [Core].[Name] NOT NULL,
	[Status] bit NULL,
	[RoleId] INT FOREIGN KEY REFERENCES [Administration.IdentityandAccessManagement].[SecurityRole](RoleId) NOT NULL,
	[EmployeeId] INT NULL,
	[MaximumATOMDevices] INT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityUser_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_SecurityUser_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_SecurityUser_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_SecurityUser_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_SecurityUser_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_SecurityUser] PRIMARY KEY CLUSTERED 
(
               [UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]