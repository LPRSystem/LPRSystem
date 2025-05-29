CREATE TYPE [api].[State] as Table
(
	[StateId] bigint NOT NULL PRIMARY KEY identity(1,1),
	[CountryId] bigint not null,
	[Name] varchar(max),
	[Description] varchar(max),
	[StateCode] varchar(max),
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (CountryId) REFERENCES data.Country(CountryId)
)