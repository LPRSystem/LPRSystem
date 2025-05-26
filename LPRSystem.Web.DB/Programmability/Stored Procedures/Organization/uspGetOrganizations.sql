CREATE PROCEDURE [api].[uspGetOrganizations]

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

END