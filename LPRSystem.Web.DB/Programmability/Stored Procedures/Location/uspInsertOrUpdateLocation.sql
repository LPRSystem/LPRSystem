CREATE PROCEDURE [api].[uspInsertOrUpdateLocation]
	(
		@location [api].[Location] READONLY
	)
	WITH RECOMPILE
AS
BEGIN
    DECLARE @CurrentDate DATETIMEOFFSET;
    DECLARE @CurrentUser  BIGINT;
    DECLARE @ReturnData TABLE (
				  [LocationId] BIGINT
				 ,[LocationName] VARCHAR(MAX)
				 ,[Code] VARCHAR(MAX)
				 ,[Address] VARCHAR(MAX)
				 ,[CountryId] BIGINT
				 ,[StateId] BIGINT
				 ,[CityId]  BIGINT
				 ,[CreatedBy] BIGINT
				 ,[CreatedOn] DATETIMEOFFSET
				 ,[ModifiedBy] BIGINT
				 ,[ModifiedOn] DATETIMEOFFSET
                 ,[IsActive] BIT
				 );

	  SET @CurrentDate = GETDATE();

    SELECT @CurrentUser  = ModifiedBy FROM @Location;

    MERGE INTO [data].[Location] AS Target

USING (
  SELECT 
	    input.[LocationId]
	   ,input.[LocationName]
	   ,input.[Code]
	   ,input.[Address]
	   ,input.[CountryId]
	   ,input.[StateId]
	   ,input.[CityId]
	   ,input.[CreatedBy]
	   ,input.[CreatedOn]
	   ,input.[ModifiedBy]
	   ,input.[ModifiedOn]
	   ,input.[IsActive]
	    FROM @location AS input
        LEFT JOIN [data].[Location] AS loc ON input.LocationId = loc.LocationId
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
				,source.[CreatedBy]
				,source.[CreatedOn]
				,source.[ModifiedBy]
				,source.[ModifiedOn]
				,source.[IsActive]
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
