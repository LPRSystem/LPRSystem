CREATE TYPE [api].[User] as Table(
    [Id]				Bigint                NULL,
	[FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
	[PasswordHash]      nvarchar(max)                   NULL,
	[PasswordSalt]      nvarchar(max)                   NULL,
	[RoleId]            bigint                          NULL,
	[IsBlocked]         bit                             NULL,
	[LastPasswordChangedOn] datetimeoffset              NULL,
	[CreatedBy]			Bigint				NULL,
	[CreatedOn]			datetimeoffset					NULL,
	[ModifiedBy]		Bigint				NULL,
	[ModifiedOn]		datetimeoffset					NULL,
	[IsActive]			bit								NULL
)
	
