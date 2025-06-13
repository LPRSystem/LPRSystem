CREATE PROCEDURE [api].[uspGetCity]
	WITH RECOMPILE

AS

BEGIN

  SELECT
	   cty.[CityId]
	  ,cty.[StateId]
	  ,sta.[Name]
	  ,cty.[CountryId]
	  ,cun.[Name]
	  ,cty.[Description]
	  ,cty.[CityCode]
	  ,cty.[CreatedOn]
	  ,cty.[CreatedBy]
	  ,cty.[ModifiedOn]
	  ,cty.[ModifiedBy]
	  ,cty.[IsActive]
	  FROM [data].[City] cty
	  Left Join [data].[State] sta ON cty.StateId = sta.StateId
	  Left Join [data].[Country] cun ON cty.CountryId = cun.CountryId


END
