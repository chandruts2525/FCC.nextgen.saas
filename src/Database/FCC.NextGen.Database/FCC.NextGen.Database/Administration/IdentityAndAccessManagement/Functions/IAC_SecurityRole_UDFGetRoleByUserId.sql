
	CREATE function [dbo].[IAC_SecurityRole_UDFGetRoleByUserId]
	(
     @UserId INT
	)
	returns Nvarchar(Max) as 
	BEGIN 
	
	Declare @RoleMapping Nvarchar(MAX)

	Declare @tblUserList table (UserId INT,UserName NVARCHAR(100), RoleName NVARCHAR(MAX),IsActive BIT)

		Declare  @tblUserList1 table (UserId INT,UserName NVARCHAR(100), RoleName NVARCHAR(MAX),IsActive BIT)

			INSERT INTO @tblUserList
			SELECT DISTINCT U.UserId,FirstName+' '+LastName UserName,R.RoleName,U.IsActive
   
			FROM [Administration.IdentityAndAccessManagement].[SecurityUser] U

			LEFT JOIN [Administration.IdentityAndAccessManagement].[SecurityUserRole] RUM ON RUM.UserID = U.UserID AND RUM.IsActive = 1
			LEFT JOIN [Administration.IdentityAndAccessManagement].[SecurityRole] R ON R.RoleId = RUM.RoleId


		GROUP BY U.UserId,U.FirstName,U.LastName,R.RoleName,U.IsActive
		INSERT INTO @tblUserList1
			SELECT DISTINCT UserId,UserName
			,STUFF((SELECT ', ' + CAST(RoleName AS VARCHAR(100)) [text()]
				FROM @tblUserList 
				WHERE UserId = t.UserId
				FOR XML PATH(''), TYPE)
			.value('.','NVARCHAR(MAX)'),1,2,' ') UserRole,[IsActive]
		FROM @tblUserList t
		where UserId= @UserId

		GROUP BY UserId,UserName,[IsActive]

		select  @RoleMapping=RoleName from @tblUserList1 where  UserId=@UserId

		return @RoleMapping
	END





