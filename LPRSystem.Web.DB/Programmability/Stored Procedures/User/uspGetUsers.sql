CREATE PROCEDURE [api].[uspGetUsers]

WITH RECOMPILE

AS

BEGIN

	SELECT
		  [Id],
		  [FirstName],
		  [LastName],
		  [Phone],
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
