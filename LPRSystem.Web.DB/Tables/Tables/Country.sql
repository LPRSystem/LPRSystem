CREATE TABLE [data].[Country]
(
[CountryId]						bigint			NOT NULL		PRIMARY KEY identity(1,1),
[Name]							varchar(max)        NULL,
[Description]					varchar(max)		NULL,
[CountryCode]				    varchar(max)		NULL,
[CreatedOn]						datetimeoffset		NULL,
[CreatedBy]						bigint				NULL,
[ModifiedOn]					datetimeoffset		NULL,
[ModifiedBy]					bigint				NULL,
[IsActive]						bit					NULL
)
