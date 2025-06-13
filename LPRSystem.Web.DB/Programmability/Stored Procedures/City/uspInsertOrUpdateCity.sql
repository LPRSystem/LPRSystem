CREATE PROCEDURE [api].[uspInsertOrUpdateCity]
(
    @City [api].[City] READONLY
)
WITH RECOMPILE
AS
BEGIN
    DECLARE @CurrentDate DATETIMEOFFSET = GETDATE();
    DECLARE @CurrentUser BIGINT;
    DECLARE @ReturnData [api].[City];
    
    -- Get the current user from input parameter
    SELECT @CurrentUser = ModifiedBy FROM @City;

    MERGE INTO [data].[City] AS Target
    USING (
        SELECT 
            [CityId],
            [StateId],
            [CountryId],
            [Name],
            [Description],
            [CityCode],
            [CreatedOn],
            [CreatedBy],
            [ModifiedOn],
            [ModifiedBy],
            [IsActive]
        FROM @City
    ) AS Source
    ON Target.CityId = Source.CityId
    
    WHEN MATCHED THEN
        UPDATE SET
            [StateId] = Source.[StateId],
            [CountryId] = Source.[CountryId],
            [Name] = Source.[Name],
            [Description] = Source.[Description],
            [CityCode] = Source.[CityCode],
            [ModifiedOn] = @CurrentDate,
            [ModifiedBy] = @CurrentUser,
            [IsActive] = Source.[IsActive]
            
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (
            [StateId],
            [CountryId],
            [Name],
            [Description],
            [CityCode],
            [CreatedOn],
            [CreatedBy],
            [ModifiedOn],
            [ModifiedBy],
            [IsActive]
        )
        VALUES (
            Source.[StateId],
            Source.[CountryId],
            Source.[Name],
            Source.[Description],
            Source.[CityCode],
            ISNULL(Source.[CreatedOn], @CurrentDate),
            ISNULL(Source.[CreatedBy], @CurrentUser),
            ISNULL(Source.[ModifiedOn], @CurrentDate),
            ISNULL(Source.[ModifiedBy], @CurrentUser),
            ISNULL(Source.[IsActive], 1)
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
        [CityId],
        [StateId],
        [CountryId],
        [Name],
        [Description],
        [CityCode],
        [CreatedOn],
        [CreatedBy],
        [ModifiedOn],
        [ModifiedBy],
        [IsActive]
    FROM @ReturnData;
END