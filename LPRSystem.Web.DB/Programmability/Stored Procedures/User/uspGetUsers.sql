CREATE PROCEDURE [dbo].[uspGetUsers]

WITH RECOMPILE

AS

BEGIN

	SELECT
		  [Id],
		  [FirstName],
		  [LastName],
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

END
