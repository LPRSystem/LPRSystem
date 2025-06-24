CREATE PROCEDURE [api].[uspGetLocation]

WITH RECOMPILE

AS

BEGIN

SELECT
		loc.[LocationId]
	   ,loc.[LocationName]
	   ,loc.[Code]
	   ,loc.[Address]
	   ,loc.[CountryId]
	   ,cun.[Name] as CountryName
	   ,loc.[StateId]
	   ,sta.[Name] AS StateName
	   ,loc.[CityId]
	   ,cty.[Name] AS CityName
	   ,loc.[CreatedBy]
	   ,loc.[CreatedOn]
	   ,loc.[ModifiedBy]
	   ,loc.[ModifiedOn]
	   ,loc.[IsActive]
	FROM [data].[Location] loc
	  left Join [data].[City] cty ON loc.CityId = cty.CityId
	  left Join [data].[State] sta ON loc.StateId = sta.StateId
	  left Join [data].[Country] cun ON loc.CountryId = cun.CountryId
END
