﻿CREATE SCHEMA [Sales] 
	AUTHORIZATION [dbo];

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value=N'Contains objects related to sales', @level0type= N'SCHEMA', @level0name = N'Sales';
