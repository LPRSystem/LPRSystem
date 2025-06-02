CREATE TABLE [data].[ATMMachine]
(
	[ATMId]				BIGINT      NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ATMCode]		VARCHAR(MAX)    NULL,
	[LocationId]		BIGINT          NULL,
	[CreatedOn]			datetimeoffset  null,
	[CreatedBy]			bigint		    null,
	[ModifiedOn]		datetimeoffset  null,
	[ModifiedBy]		bigint			null,
	[IsActive]			bit				null,
	FOREIGN KEY  (LocationId) REFERENCES data.Location(LocationId)
)
