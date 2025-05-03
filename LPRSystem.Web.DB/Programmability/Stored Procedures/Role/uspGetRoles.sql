CREATE PROCEDURE [api].[uspGetRoles]

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

END
