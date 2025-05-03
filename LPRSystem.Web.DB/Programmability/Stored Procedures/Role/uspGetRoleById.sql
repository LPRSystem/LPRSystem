CREATE PROCEDURE [api].[uspGetRoleById]
(

@roleId bigint

)
WITH RECOMPILE

AS

BEGIN

  SELECT
	   [Id]
	  ,[Name]
	  ,[Code]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [lookup].[Role]

  WHERE Id=@roleId

END
