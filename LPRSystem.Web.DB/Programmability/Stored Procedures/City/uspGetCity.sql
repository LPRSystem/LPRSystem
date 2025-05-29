CREATE PROCEDURE [dbo].[uspGetCity]
	WITH RECOMPILE

AS

BEGIN

  SELECT
	   [CityId]
	  ,[StateId]
	  ,[CountryId]
	  ,[Name]
	  ,[Description]
	  ,[CityCode]
	  ,[CreatedOn]
	  ,[CreatedBy]
	  ,[ModifiedOn]
	  ,[ModifiedBy]
	  ,[IsActive]
  FROM [data].[City]

END
