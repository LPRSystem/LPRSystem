CREATE PROCEDURE [api].[uspInsertOrUpdateRole]
(
  @Role [api].[Role] READONLY
)

WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate datetimeoffset;
DECLARE @CurrentUser bigint;
Declare @RetrunData [api].[Role];

set @CurrentDate = GETDATE();

Select @CurrentUser= ModifiedBy from  @Role;

Merge INTO [lookup].[Role] AS Target
Using (
       select input.[Id]
             ,input.[Name]
             ,input.[Code]
             ,input.[CreatedBy]
             ,input.[CreatedOn]
             ,input.[ModifiedBy]
             ,input.[ModifiedOn]
             ,input.[IsActive]
             FROM @Role input
             LEFT JOIN 
             [lookup].[Role] cur
             on input.Id = cur.Id
             ) as source
             ON target.Id= source.Id
             WHEN MATCHED THEN
             UPDATE 
             SET 
             [Name]         = source.[Name]
            ,[Code]         = source.[Code]
            ,[ModifiedBy]   = @CurrentUser
            ,[ModifiedOn]   = @CurrentDate
            ,[IsActive]     = source.[IsActive]
           WHEN NOT MATCHED BY TARGET THEN 
           INSERT 
           ([Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive])
           values
           (source.[Name]
           ,source.[Code]
           ,source.[CreatedBy]
           ,source.[CreatedOn]
           ,source.[ModifiedBy]
           ,source.[ModifiedOn]
           ,source.[IsActive])
           OUTPUT 
            inserted.[Id]
           ,inserted.[Name]
           ,inserted.[Code]
           ,inserted.[CreatedBy]
           ,inserted.[CreatedOn]
           ,inserted.[ModifiedBy]
           ,inserted.[ModifiedOn]
           ,inserted.[IsActive]
           INTO 
           @RetrunData(
            [Id]
           ,[Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]);

           SELECT 
            [Id]
           ,[Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]
           FROM 
            
            @RetrunData
 END




