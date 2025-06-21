CREATE PROCEDURE [api].[uspAuthenticateUser]
(
 @username varchar(50)
)

WITH RECOMPILE

AS

BEGIN
		SELECT
				  [Id],
				  [FirstName],
				  [LastName],
				  [Email],
				  [Phone],
				  [PasswordHash],
				  [PasswordSalt],
				  [RoleId],
				  [IsBlocked],
				  [LastPasswordChangedOn],
				  [CreatedBy],
				  [CreatedOn],
				  [ModifiedBy],
				  [ModifiedOn],
				  [IsActive]
				  FROM [data].[User]
			      WHERE 
				  Email=@username
				  or 
				  Phone =@username
END