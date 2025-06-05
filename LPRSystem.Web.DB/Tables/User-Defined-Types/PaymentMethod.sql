CREATE TYPE [api].[PaymentMethod] as Table (
    [Id]                            bigint                 NULL, 
	[Name]							varchar(max)           NULL,
	[Code]							varchar(max)           NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)