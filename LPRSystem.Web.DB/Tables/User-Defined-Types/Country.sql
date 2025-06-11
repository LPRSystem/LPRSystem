CREATE TYPE [api].[Country] as Table
(
	[CountryId]			bigint			NULL,
	[Name]				varchar(max)	NULL,
	[Description]		varchar(max)	NULL,
	[CountryCode]		varchar(max)	NULL,
	[CreatedOn]			datetimeoffset	NULL,
	[CreatedBy]			bigint			NULL,
	[ModifiedOn]		datetimeoffset  NULL,
	[ModifiedBy]	    bigint			NULL,
	[IsActive]			bit				NULL
)