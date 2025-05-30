CREATE PROCEDURE [api].[uspInsertOrUpdateUser]
(
	@User [api].[User] READONLY
)

WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate datetimeoffset;
DECLARE @CurrentUser bigint;
DECLARE @ReturnData [api].[User];

set @CurrentDate = GETDATE();

Select @CurrentUser = ModifiedBy from @User;

Merge INTO [data].[User] AS Target
Using(
		Select input.[Id]
             ,input.[FirstName]
             ,input.[LastName]
             ,input.[Phone]
             ,input.[Email]
             ,input.[PasswordHash]
             ,input.[PasswordSalt]
             ,input.[RoleId]
             ,input.[IsBlocked]
             ,input.[LastPasswordChangedOn]
             ,input.[CreatedBy]
             ,input.[CreatedOn]
             ,input.[ModifiedBy]
             ,input.[ModifiedOn]
             ,input.[IsActive]
             From @User input
             LEFT JOIN
             [data].[User] cur
             on input.Id = cur.Id
             ) as source
             ON Target.Id = source.Id
             WHEN Matched Then 
             UPDATE
             SET
                [FirstName] = source.[FirstName]
		        ,[LastName] = source.[LastName]
		        ,[Phone] = source.[Phone]
				,[Email] = source.[Email]
		        ,[RoleId] =source.[RoleId]
		        ,[IsBlocked] =source.[IsBlocked]
		        ,[LastPasswordChangedOn] =source.[LastPasswordChangedOn]
                ,[ModifiedBy]   = @CurrentUser
                ,[ModifiedOn]   = @CurrentDate
                ,[IsActive]     = source.[IsActive]
                 WHEN NOT MATCHED BY TARGET THEN 
           INSERT 
           ([FirstName]
           ,[LastName]
		   ,[Phone]
		   ,[Email]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[RoleId]
           ,[IsBlocked]
           ,[LastPasswordChangedOn]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive])
           values
           (source.[FirstName]
           ,source.[LastName]
           ,source.[Phone]
		   ,source.[Email]
           ,source.[PasswordHash]
           ,source.[PasswordSalt]
           ,source.[RoleId]
           ,source.[IsBlocked]
           ,source.[LastPasswordChangedOn]
           ,source.[CreatedBy]
           ,source.[CreatedOn]
           ,source.[ModifiedBy]
           ,source.[ModifiedOn]
           ,source.[IsActive])
           OUTPUT 
			inserted.[Id]
           ,inserted.[FirstName]
           ,inserted.[LastName]
           ,inserted.[Phone]
		   ,inserted.[Email]
           ,inserted.[RoleId]
           ,inserted.[IsBlocked]
           ,inserted.[LastPasswordChangedOn]
           ,inserted.[CreatedBy]
           ,inserted.[CreatedOn]
           ,inserted.[ModifiedBy]
           ,inserted.[ModifiedOn]
           ,inserted.[IsActive]
           INTO 
           @ReturnData(
            [Id]
           ,[FirstName]
           ,[LastName]
		   ,[Phone]
		   ,[Email]
		   ,[RoleId]
		   ,[IsBlocked]
		   ,[LastPasswordChangedOn]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]);

           SELECT 
		    [Id]
           ,[FirstName]
           ,[LastName]
		   ,[Phone]
		   ,[Email]
           ,[RoleId]
           ,[IsBlocked]
           ,[LastPasswordChangedOn]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[IsActive]
           FROM 
            
            @ReturnData

END
	
