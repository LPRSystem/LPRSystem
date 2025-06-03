CREATE PROCEDURE [api].[uspInsertOrUpdateATMMAchine]
(
	@ATMMachine [api].[ATMMAchine] Readonly
)
WITH RECOMPILE

AS

BEGIN

DECLARE	@CurrentDate datetimeoffset;
DECLARE	@CurrentUser bigint;
DECLARE @RetrunData [api].[ATMMAchin];

set @CurrentDate = GETDATE();

Select @CurrentUser= ModifiedBy from  @ATMMachine;

Merge INTO [data].[ATMMachine] AS Target

Using (
       select input.[ATMId]
             ,input.[ATMCode]
             ,input.[LocationId]
             ,input.[CreatedBy]
             ,input.[CreatedOn]
             ,input.[ModifiedBy]
             ,input.[ModifiedOn]
             ,input.[IsActive]
             FROM @ATMMachine input
             LEFT JOIN 
             [data].[ATMMachine] cur
             on input.ATMId = cur.ATMId
             ) as source
             ON target.ATMId= source.ATMId
             WHEN MATCHED THEN
             UPDATE 
             SET 
             [ATMCode]         = source.[ATMCode]
            ,[LocationId]      = source.[LocationId]
            ,[ModifiedBy]   = @CurrentUser
            ,[ModifiedOn]   = @CurrentDate
            ,[IsActive]     = source.[IsActive]
           WHEN NOT MATCHED BY TARGET THEN 
           INSERT 
           (
            [ATMCode]
           ,[LocationId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive])
           values
           (
            source.[ATMCode]
           ,source.[LocationId]
           ,source.[CreatedBy]
           ,source.[CreatedOn]
           ,source.[ModifiedBy]
           ,source.[ModifiedOn]
           ,source.[IsActive])
           OUTPUT 
            inserted.[ATMId]
           ,inserted.[ATMCode]
           ,inserted.[LocationId]
           ,inserted.[CreatedBy]
           ,inserted.[CreatedOn]
           ,inserted.[ModifiedBy]
           ,inserted.[ModifiedOn]
           ,inserted.[IsActive]
           INTO 
           @RetrunData(
            [ATMId]
           ,[ATMCode]
           ,[LocationId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]);

  SELECT 
            [ATMId]
           ,[ATMCode]
           ,[LocationId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]
           FROM 
            
            @RetrunData
 END



