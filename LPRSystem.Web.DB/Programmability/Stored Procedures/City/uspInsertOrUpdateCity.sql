CREATE PROCEDURE [api].[uspInsertOrUpdateCity]
(
	@city [api].[City] READONLY
)

WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate DATETIMEOFFSET;
DECLARE @CurrentUser BIGINT;
DECLARE @ReturnData [api].[City]

   SET @CurrentDate = GETDATE();

   SELECT @CurrentUser = ModifiedBy From @city;

   MERGE INTO [data].[city] AS TARGET

   USING (
		SELECT
			  input.[CityId],
			  input.[StateId],
			  input.[CountryId],
			  input.[Name],
			  input.[Description],
			  input.[CityCode],
			  input.[CreatedOn],
			  input.[CreatedBy],
			  input.[ModifiedOn],
			  input.[ModifiedBy],
			  input.[IsActive]
			  From @city AS input	
			  LEFT JOIN [data].[City] AS cty ON input.CityId = cty.CityId
			  ) AS source
			  ON TARGET.CityId = source.CityId
			  WHEN MATCHED THEN
			  UPDATE SET
						[StateId]    = source.[StateId],
						[CountryId]  = source.[CountryId],
						[Name]       = source.[Name],
						[Description]= source.[Description],
						[CityCode]   = source.[CityCode],
						[CreatedOn]  = source.[CreatedOn],
					    [CreatedBy]	 = source.[CreatedBy],
						[ModifiedOn] = source.[ModifiedOn],
						[ModifiedBy] = source.[ModifiedBy],
						[IsActive]   = source.[IsActive]
						WHEN NOT MATCHED BY TARGET THEN
			INSERT
			(
				[StateId] ,
				[CountryId] ,
				[Name] ,
				[Description],
				[CityCode] ,
				[CreatedOn] ,
				[CreatedBy],
				[ModifiedOn] ,
				[ModifiedBy],
				[IsActive]
			)
			VALUES
			(
				source.[StateId],
				source.[CountryId],
				source.[Name],
				source.[Description],
				source.[CityCode],
				source.[CreatedOn],
				source.[CreatedBy],
				source.[ModifiedOn],
				source.[ModifiedBy],
				source.[IsActive]
			    )
		OUTPUT
				inserted.[CityId],
				inserted.[StateId],
				inserted.[CountryId],
				inserted.[Name],
				inserted.[Description],
				inserted.[CityCode],
				inserted.[CreatedOn],
				inserted.[CreatedBy],
				inserted.[ModifiedOn],
				inserted.[ModifiedBy],
				inserted.[IsActive]
		INTO @ReturnData;

		SELECT
				[CityId] ,
				[StateId] ,
				[CountryId] ,
				[Name] ,
				[Description],
				[CityCode] ,
				[CreatedOn] ,
				[CreatedBy],
				[ModifiedOn] ,
				[ModifiedBy],
				[IsActive]
			FROM @ReturnData;

END
				
