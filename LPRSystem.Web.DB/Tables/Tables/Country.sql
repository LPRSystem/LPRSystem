CREATE TABLE [data].[Country]
(
[CountryId] bigint NOT NULL PRIMARY KEY identity(1,1),
[Name] varchar(max),
[Description] varchar(max),
[CountryCode] varchar(max),
[CreatedOn] datetimeoffset null,
[CreatedBy] bigint null,
[ModifiedOn] datetimeoffset null,
[ModifiedBy] bigint null,
[IsActive] bit null
)
