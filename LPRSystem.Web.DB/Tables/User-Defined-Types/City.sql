CREATE TYPE [api].[City] as Table
(
	[CityId] bigint NOT NULL PRIMARY KEY identity(1,1),
	[StateId] bigint  null,
	[CountryId] bigint  null,
	[Name] varchar(max),
	[Description] varchar(max),
	[CityCode] varchar(max),
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (StateId) REFERENCES data.State(StateId),
	FOREIGN KEY (CountryId) REFERENCES data.Country(CountryId)
)
