CREATE TYPE [api].[State] as Table
(
	[StateId]			bigint				NULL,
	[CountryId]			bigint				NULL,
	[Name]				varchar(max)        NULL,
	[Description]		varchar(max)		NULL,
	[StateCode]			varchar(max)		NULL,
	[CreatedOn]		    datetimeoffset		NULL,
	[CreatedBy]			bigint				NULL,
	[ModifiedOn]		datetimeoffset		NULL,
	[ModifiedBy]		bigint				NULL,
	[IsActive]			bit					NULL
)