CREATE TABLE [data].[Organization]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[Name]                          varchar(max)           NULL,
	[Code]                          varchar(max)           NULL,
	[Address]						varchar(max)           NULL,
	[Email]							varchar(max)           NULL,
	[Phone]                         varchar(max)           NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)
