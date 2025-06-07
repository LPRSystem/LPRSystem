CREATE PROCEDURE [api].[uspGetLocation]

WITH RECOMPILE

AS

BEGIN

SELECT
		[LocationId]
	   ,[LocationName]
	   ,[Code]
	   ,[Address]
	   ,[CountryId]
	   ,[StateId]
	   ,[CityId]
	   ,[CreatedBy]
	   ,[CreatedOn]
	   ,[ModifiedBy]
	   ,[ModifiedOn]
	   ,[IsActive]
	FROM [data].[Location]
END
