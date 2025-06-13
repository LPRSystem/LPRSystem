CREATE PROCEDURE [api].[uspGetOrganizationById]
(

@Id bigint

)
WITH RECOMPILE

AS

BEGIN

  SELECT
		[Id]
	  ,[Name]
	  ,[Code]
	  ,[Address]
	  ,[EMail]
	  ,[Phone]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [data].[Organization]

  WHERE Id=@Id

END