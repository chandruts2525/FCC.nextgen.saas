/*
View NAME: [VW_SecurityUserinformation] 
Created By: DHESH KUMAAR A
Created Date: 11/15/2023 (mm/dd/yyyy)
*/

CREATE VIEW [dbo].[VW_SecurityUserinformation]    
AS     
Select U.[UserID],U.[LastName],U.[FirstName],U.[LoginEmail],CASE WHEN U.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS [Status],
[MaximumATOMDevices],BE.[BusinessEntityName] AS EmployeeName,U.[IsDeleted]   
FROM       
[Administration.IdentityAndAccessManagement].[SecurityUserBusinessEntity] UBE       
INNER JOIN [MasterDataManagement.OrganizationStructure].[BusinessEntity] BE       
ON UBE.[BusinessEntityID] = BE.[BusinessEntityID]   
INNER JOIN [MasterDataManagement.OrganizationStructure].[BusinessEntityType] C  
ON BE.BusinessEntityTypeID = C.BusinessEntityTypeId AND BusinessEntityTypeName = 'EMPLOYEE' AND UBE.IsDeleted = 0
RIGHT OUTER JOIN [Administration.IdentityandAccessManagement].[SecurityUser] U      
ON U.[UserID] = UBE.[UserID]