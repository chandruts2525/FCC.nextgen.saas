CREATE TABLE [MasterDataManagement.Core].[CountryRegion]
(
	[CountryRegionCode] nvarchar(3) NOT NULL,
	[Name] [Core].[Name] NOT NULL,
	[IsActive] bit CONSTRAINT [DF_CountryRegion_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_CountryRegion_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_CountryRegion_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_CountryRegion_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL,
	CONSTRAINT [PK_CountryRegion_CountryRegionCode] PRIMARY KEY CLUSTERED ([CountryRegionCode] ASC)
)
