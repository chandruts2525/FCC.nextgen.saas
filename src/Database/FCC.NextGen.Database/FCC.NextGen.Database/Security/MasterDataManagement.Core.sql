CREATE SCHEMA [MasterDataManagement.Core] 
	AUTHORIZATION [dbo];

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value=N'Contains objects related to lookup and master data', @level0type= N'SCHEMA', @level0name = N'MasterDataManagement.Core';
