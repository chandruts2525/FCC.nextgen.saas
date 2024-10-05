CREATE TABLE [MasterDataManagement.Core].[Address]
(
	[AddressID] INT IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[AddressLine1] NVARCHAR(60) NOT NULL,
	[AddressLine2] NVARCHAR(60) NULL,
	[City] NVARCHAR(30) NOT NULL,
	[StateProvinceID] INT NOT NULL,
	[PostalCode] NVARCHAR(15) NOT NULL,
	[SpatialLocation] [Sys].[Geography] NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_Address_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_Address_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_Address_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_Address_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_Address_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL
)
