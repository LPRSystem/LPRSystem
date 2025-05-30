CREATE TYPE [api].[User] as Table(
    [Id]				bigint                          NULL,
	[FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
	[PasswordHash]      nvarchar(max)                   NULL,
	[PasswordSalt]      nvarchar(max)                   NULL,
	[RoleId]            bigint                          NULL,
	[IsBlocked]         bit                             NULL,
	[LastPasswordChangedOn] datetimeoffset              NULL,
	[CreatedBy]			bigint				NULL,
	[CreatedOn]			datetimeoffset					NULL,
	[ModifiedBy]		bigint				NULL,
	[ModifiedOn]		datetimeoffset					NULL,
	[IsActive]			bit								NULL
)
	
