CREATE TABLE [MasterDataManagement.Core].[StateProvince]
(
	[StateProvinceID] INT IDENTITY(1,1) NOT NULL,
	[StateProvinceCode] NCHAR(3) NOT NULL,
	[CountryRegionCode] NVARCHAR(3) NOT NULL,
	[IsOnlyStateProvinceFlag] [Core].[Flag] CONSTRAINT DF_StateProvince_IsOnlyStateProvinceFlag DEFAULT ((1)) NOT NULL,
	[Name] [Core].[Name] NOT NULL,
	[rowguid] UNIQUEIDENTIFIER CONSTRAINT [DF_StateProvince_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[IsActive] bit CONSTRAINT [DF_StateProvince_IsActive] DEFAULT 1 NOT NULL,
	[IsDeleted] bit CONSTRAINT [DF_StateProvince_IsDeleted] DEFAULT 0 NOT NULL,
	[IsLocked] bit CONSTRAINT [DF_StateProvince_IsLocked] DEFAULT 0 NOT NULL,
	[CreatedDate] datetime NOT NULL CONSTRAINT [DF_StateProvince_CreatedDate] DEFAULT getdate(),
	[CreatedBy] [Core].[User] NOT NULL,
	[ModifiedDate] datetime NULL,
	[ModifiedBy] [Core].[User] NULL,
	CONSTRAINT PK_StateProvince_StateProvinceID PRIMARY KEY CLUSTERED ([StateProvinceID] ASC),
	CONSTRAINT FK_StateProvince_CountryRegion_CountryRegionCode FOREIGN KEY ([CountryRegionCode]) REFERENCES [MasterDataManagement.Core].[CountryRegion] ([CountryRegionCode])
)
