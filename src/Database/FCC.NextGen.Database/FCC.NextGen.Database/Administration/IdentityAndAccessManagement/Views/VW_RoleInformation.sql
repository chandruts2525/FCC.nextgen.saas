/*
View NAME: [VW_RoleInformation] 
Created By: Divyabharathi M
Created Date: 11/08/2023 (mm/dd/yyyy)
*/  
  
/* made changes in the query by placing the where condition in different place*/

CREATE VIEW [dbo].[VW_RoleInformation]
AS
SELECT R.RoleId, R.RoleName, R.CreatedBy, R.IsActive, COUNT(RUM.UserID) AS AssignedUser
FROM     [Administration.IdentityAndAccessManagement].SecurityRole AS R LEFT OUTER JOIN
                  [Administration.IdentityAndAccessManagement].SecurityUserRole AS RUM ON R.RoleId = RUM.RoleID and RUM.IsDeleted = 0
WHERE  (R.IsDeleted = 0) 
GROUP BY R.RoleId, R.RoleName, R.CreatedBy, R.IsActive
GO
