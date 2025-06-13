CREATE PROCEDURE [api].[uspGetCities]

WITH RECOMPILE 

AS

 BEGIN

  SELECT
	   cty.[CityId]
	  ,cty.[StateId]
	  ,cty.[Name]
	  ,sta.[Name] AS StateName
	  ,cty.[CountryId]
	  ,cun.[Name] as CountryName
	  ,cty.[Description]
	  ,cty.[CityCode]
	  ,cty.[CreatedOn]
	  ,cty.[CreatedBy]
	  ,cty.[ModifiedOn]
	  ,cty.[ModifiedBy]
	  ,cty.[IsActive]
	   FROM [data].[City] cty
	  left Join [data].[State] sta ON cty.StateId = sta.StateId
	  left Join [data].[Country] cun ON cty.CountryId = cun.CountryId

END