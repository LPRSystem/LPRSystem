CREATE TABLE [data].[ATMMachine]
(
	[ATMId]				BIGINT      NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MachineName]		VARCHAR(MAX)    NULL,
	[BankName]			VARCHAR(max)    Null,
	[CashAvailable]		DECIMAL(18, 2)  NULL DEFAULT 0.00,
	[LocationId]		BIGINT          NULL,
	[CreatedOn]			datetimeoffset  null,
	[CreatedBy]			bigint		    null,
	[ModifiedOn]		datetimeoffset  null,
	[ModifiedBy]		bigint			null,
	[IsActive]			bit				null,
	FOREIGN KEY  (LocationId) REFERENCES data.Location(LocationId)
)
