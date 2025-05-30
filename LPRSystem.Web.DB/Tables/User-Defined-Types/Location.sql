CREATE TYPE [api].[Location] as Table (
    [LocationId]                    bigint                 NULL,
	[LocationName]                  varchar(max)           NULL,
	[Code]                          varchar(max)           NULL,
	[Address]						varchar(max)           NULL,
	[CountryId]						bigint                 NULL,
	[StateId]                       bigint                 NULL,
	[CityId]                        bigint                 NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL	)