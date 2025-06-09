CREATE PROCEDURE [api].[uspGetCountry]

WITH RECOMPILE

AS

BEGIN

  SELECT
		 [CountryId] 
		,[Name]
		,[Description] 
		,[CountryCode] 
		,[CreatedOn]
		,[CreatedBy]
		,[ModifiedOn]
		,[ModifiedBy]
		,[IsActive]
  FROM [data].[Country]

END
