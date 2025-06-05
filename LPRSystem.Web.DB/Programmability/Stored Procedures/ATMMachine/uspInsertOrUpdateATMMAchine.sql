CREATE PROCEDURE [api].[uspInsertOrUpdateATMMAchine]
(
    @ATMMachine [api].[ATMMAchine] READONLY
)
WITH RECOMPILE
AS
BEGIN
    DECLARE @CurrentDate DATETIMEOFFSET;
    DECLARE @CurrentUser  BIGINT;
    DECLARE @ReturnData TABLE (
        [ATMId] BIGINT,
        [ATMCode] NVARCHAR(50),
        [LocationId] NVARCHAR(50),
        [CreatedBy] BIGINT,
        [CreatedOn] DATETIMEOFFSET,
        [ModifiedBy] BIGINT,
        [ModifiedOn] DATETIMEOFFSET,
        [IsActive] BIT
    );

    SET @CurrentDate = GETDATE();

    SELECT @CurrentUser  = ModifiedBy FROM @ATMMachine;

    MERGE INTO [data].[ATMMachine] AS Target
    USING (
        SELECT 
            input.[ATMId],
            input.[ATMCode],
            input.[LocationId],
            input.[CreatedBy],
            input.[CreatedOn],
            input.[ModifiedBy],
            input.[ModifiedOn],
            input.[IsActive]
        FROM @ATMMachine AS input
        LEFT JOIN [data].[ATMMachine] AS cur ON input.ATMId = cur.ATMId
    ) AS source
    ON Target.ATMId = source.ATMId
    WHEN MATCHED THEN
        UPDATE SET 
            [ATMCode] = source.[ATMCode],
            [LocationId] = source.[LocationId],
            [ModifiedBy] = @CurrentUser ,
            [ModifiedOn] = @CurrentDate,
            [IsActive] = source.[IsActive]
    WHEN NOT MATCHED BY TARGET THEN 
        INSERT (
            [ATMCode],
            [LocationId],
            [CreatedBy],
            [CreatedOn],
            [ModifiedBy],
            [ModifiedOn],
            [IsActive]
        )
        VALUES (
            source.[ATMCode],
            source.[LocationId],
            source.[CreatedBy],
            source.[CreatedOn],
            source.[ModifiedBy],
            source.[ModifiedOn],
            source.[IsActive]
        )
    OUTPUT 
        inserted.[ATMId],
        inserted.[ATMCode],
        inserted.[LocationId],
        inserted.[CreatedBy],
        inserted.[CreatedOn],
        inserted.[ModifiedBy],
        inserted.[ModifiedOn],
        inserted.[IsActive]
    INTO @ReturnData;

    SELECT 
        [ATMId],
        [ATMCode],
        [LocationId],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn],
        [IsActive]
    FROM @ReturnData;
END