CREATE TABLE [data].[Location]
(
	[LocationId]       BIGINT				NOT NULL PRIMARY KEY IDENTITY(1,1),
	[LocationName]     VARCHAR(MAX)			NULL,
	[Code]			   VARCHAR(MAX)			NULL,
	[Address]		   VARCHAR(MAX)			NULL,
	[CountryId]		   BIGINT				NULL,
	[StateId]          BIGINT				NULL,
	[CityId]		   BIGINT				NULL,
	[CreatedBy]        bigint               NULL,
	[CreatedOn]        datetimeoffset       NULL,
	[ModifiedBy]       bigint               NULL,
	[ModifiedOn]       datetimeoffset	    NULL,
	[IsActive]         bit                  NULL
	FOREIGN KEY  (CountryId) REFERENCES data.Country(CountryId),
	FOREIGN KEY  (StateId) REFERENCES data.State(StateId),
	FOREIGN KEY  (CityId) REFERENCES data.City(CityId),
)
