CREATE SCHEMA [Administration.IdentityAndAccessManagement] 
	AUTHORIZATION [dbo];

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value=N'Contains objects related to user, roles and permissions', @level0type= N'SCHEMA', @level0name = N'Administration.IdentityAndAccessManagement';
