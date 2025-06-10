CREATE PROCEDURE [dbo].[uspGetLocationById]
(

@locationId bigint

)
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

  WHERE LocationId=@locationId

END

