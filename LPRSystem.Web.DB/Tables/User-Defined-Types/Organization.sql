CREATE TYPE [api].[Organization] as Table (
    [Id]                            bigint                 NULL,
	[Name]                          varchar(max)           NULL,
	[Code]                          varchar(max)           NULL,
	[Address]						varchar(max)           NULL,
	[Email]							varchar(max)           NULL,
	[Phone]                         varchar(max)           NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL	)