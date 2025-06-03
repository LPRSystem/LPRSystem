CREATE PROCEDURE [api].[uspInsertOrUpdateOrganization]
(
  @Organization [api].[Organization] READONLY
)

WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate datetimeoffset;
DECLARE @CurrentUser bigint;
Declare @RetrunData [api].[Organization];

set @CurrentDate = GETDATE();   

Select @CurrentUser= ModifiedBy from  @Organization;

Merge INTO [data].[Organization] AS Target
Using (
       select input.[Id]
             ,input.[Name]
             ,input.[Code]
             ,input.[Address]
             ,input.[Email]
             ,input.[Phone]
             ,input.[CreatedBy]
             ,input.[CreatedOn]
             ,input.[ModifiedBy]
             ,input.[ModifiedOn]
             ,input.[IsActive]
             FROM @Organization input
             LEFT JOIN 
             [data].[Organization] cur
             on input.Id = cur.Id
             ) as source
             ON target.Id= source.Id
             WHEN MATCHED THEN
             UPDATE 
             SET 
             [Name]         = source.[Name]
            ,[Code]         = source.[Code]
            ,[Address]      = source.[Address]
            ,[Email]        = source.[Email]
            ,[Phone]         = source.[Phone]
            ,[ModifiedBy]   = @CurrentUser
            ,[ModifiedOn]   = @CurrentDate
            ,[IsActive]     = source.[IsActive]
           WHEN NOT MATCHED BY TARGET THEN 
           INSERT 
           ([Name]
           ,[Code]
           ,[Address]
           ,[Email]
           ,[Phone]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive])
           values
           (source.[Name]
           ,source.[Code]
           ,source.[Address]
           ,source.[Email]
           ,source.[Phone]
           ,source.[CreatedBy]
           ,source.[CreatedOn]
           ,source.[ModifiedBy]
           ,source.[ModifiedOn]
           ,source.[IsActive])
           OUTPUT 
            inserted.[Id]
           ,inserted.[Name]
           ,inserted.[Code]
           ,inserted.[Address]
           ,inserted.[Email]
           ,inserted.[Phone]
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
           ,[Address]
           ,[Email]
           ,[Phone]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]);

  SELECT 
            [Id]
           ,[Name]
           ,[Code]
           ,[Address]
           ,[Email]
           ,[Phone]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]
           FROM 
            
            @RetrunData
 END
