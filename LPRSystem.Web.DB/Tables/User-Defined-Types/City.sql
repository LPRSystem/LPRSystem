CREATE TYPE [api].[City] as Table
(
	[CityId]		bigint			null,
	[StateId]		bigint			null,
	[CountryId]		bigint			null,
	[Name]			varchar(max)	null,
	[Description]	varchar(max)    null,
	[CityCode]		varchar(max)	null,
	[CreatedOn]		datetimeoffset  null,
	[CreatedBy]		bigint          null,
	[ModifiedOn]	datetimeoffset  null,
	[ModifiedBy]	bigint			null,
	[IsActive]		bit				null
)
