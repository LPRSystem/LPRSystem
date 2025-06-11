CREATE PROCEDURE [api].[uspGetCountryById]
(
 @countryId  bigint
)
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

    WHERE CountryId=@countryId

    END