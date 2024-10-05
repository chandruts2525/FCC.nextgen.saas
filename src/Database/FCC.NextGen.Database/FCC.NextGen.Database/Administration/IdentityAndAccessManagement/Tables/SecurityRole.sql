CREATE TABLE [Administration.IdentityAndAccessManagement].[SecurityRole](
        [RoleId] [int] IDENTITY(1,1) NOT NULL,
        [RoleName] [Core].[Name] NOT NULL,
        [IsActive] bit CONSTRAINT [DF_SecurityRole_IsActive] DEFAULT 1 NOT NULL,
        [IsDeleted] bit CONSTRAINT [DF_SecurityRole_IsDeleted] DEFAULT 0 NOT NULL,
        [IsLocked] bit CONSTRAINT [DF_SecurityRole_IsLocked] DEFAULT 0 NOT NULL,
        [CreatedDate] datetime NOT NULL CONSTRAINT [DF_SecurityRole_CreatedDate] DEFAULT getdate(),
        [CreatedBy] [Core].[User] NOT NULL,
        [ModifiedDate] datetime NULL,
        [ModifiedBy] [Core].[User] NULL
        CONSTRAINT [PK_SecurityRole] PRIMARY KEY CLUSTERED 
(
               [RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]