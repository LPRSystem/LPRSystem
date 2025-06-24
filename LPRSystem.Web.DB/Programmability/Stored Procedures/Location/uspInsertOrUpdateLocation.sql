CREATE PROCEDURE [api].[uspInsertOrUpdateLocation]
(
	@Location [api].[Location] READONLY
)
WITH RECOMPILE
AS
BEGIN
    DECLARE @CurrentDate DATETIMEOFFSET = GetDate();
    DECLARE @CurrentUser  BIGINT;
    DECLARE @ReturnData [api].[Location];

    SELECT @CurrentUser  = ModifiedBy FROM @Location;

MERGE INTO [data].[Location] AS Target
USING (
  SELECT 
	    [LocationId]
	    [LocationName],
	    [Code],
	    [Address],
	    [CountryId],
	    [StateId],
	    [CityId],
	    [CreatedBy],
	    [CreatedOn],
	    [ModifiedBy],
	    [ModifiedOn],
	    [IsActive]
	    FROM @location 
    ) AS source
    ON Target.LocationId = source.LocationId
    WHEN MATCHED THEN
    UPDATE SET
	    [LocationName] = source.[LocationName]
	   ,[Code] = source.[Code]
	   ,[Address] = source.[Address]
	   ,[CountryId] = source.[CountryId]
	   ,[StateId] = source.[StateId]
	   ,[CityId] = source.[CityId]
	   ,[ModifiedBy] = source.[ModifiedBy]
	   ,[ModifiedOn] = source.[ModifiedOn]
	   ,[IsActive] = source.[IsActive]
 WHEN NOT MATCHED BY TARGET THEN 
 INSERT (
	    [LocationName]
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
	   )
	    VALUES (
				source.[LocationName]
				,source.[Code]
				,source.[Address]
				,source.[CountryId]
				,source.[StateId]
				,source.[CityId]
				,ISNULL(Source.[CreatedBy], @CurrentUser)
				,ISNULL(Source.[CreatedOn], @CurrentDate)
				,ISNULL(Source.[ModifiedBy], @CurrentUser)
				,ISNULL(Source.[ModifiedOn], @CurrentDate)
				,ISNULL(Source.[IsActive], 1)
				)
		OUTPUT
				inserted.[LocationName]
				,inserted.[Code]
				,inserted.[Address]
				,inserted.[CountryId]
				,inserted.[StateId]
				,inserted.[CityId]
				,inserted.[CreatedBy]
				,inserted.[CreatedOn]
				,inserted.[ModifiedBy]
				,inserted.[ModifiedOn]
				,inserted.[IsActive]
		INTO @ReturnData;

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
    FROM @ReturnData;
END
