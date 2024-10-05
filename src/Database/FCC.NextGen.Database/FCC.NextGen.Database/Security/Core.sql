CREATE SCHEMA [Core] 
	AUTHORIZATION [dbo];

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value=N'Contains objects related to user defined data types', @level0type= N'SCHEMA', @level0name = N'Core';
