CREATE TABLE [Administration.IdentityAndAccessManagement].[SecurityUserRole](
        [UserRoleId] [int] IDENTITY(1,1) NOT NULL,
        [RoleID] INT FOREIGN KEY REFERENCES [Administration.IdentityandAccessManagement].[SecurityRole](RoleID) NOT NULL,
	    [UserID] INT FOREIGN KEY REFERENCES [Administration.IdentityandAccessManagement].[SecurityUser](UserID) NOT NULL,
        [IsActive] bit CONSTRAINT [DF_SecurityUserRole_IsActive] DEFAULT 1 NOT NULL,
        [IsDeleted] bit CONSTRAINT [DF_SecurityUserRole_IsDeleted] DEFAULT 0 NOT NULL,
        [IsLocked] bit CONSTRAINT [DF_SecurityUserRole_IsLocked] DEFAULT 0 NOT NULL,
        [CreatedDate] datetime NOT NULL CONSTRAINT [DF_SecurityUserRole_CreatedDate] DEFAULT getdate(),
        [CreatedBy] [Core].[User] NOT NULL,
        [ModifiedDate] datetime NULL,
        [ModifiedBy] [Core].[User] NULL
CONSTRAINT [PK_SecurityUserRole] PRIMARY KEY CLUSTERED 
(
               [UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]