CREATE SCHEMA [MasterDataManagement.OrganizationStructure] 
	AUTHORIZATION [dbo];

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value=N'Contains objects related to organization and related entities', @level0type= N'SCHEMA', @level0name = N'MasterDataManagement.OrganizationStructure';
