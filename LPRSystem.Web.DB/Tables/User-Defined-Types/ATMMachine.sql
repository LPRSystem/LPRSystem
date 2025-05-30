CREATE TYPE [api].[ATMMachine] as Table
(
	[ATMId]				bigint			NULL,
	[MachineName ]		varchar(max)	NULL,
	[BankName]			VARCHAR(max)    Null,
	[CashAvailable]		DECIMAL(18, 2)  NULL DEFAULT 0.00,
	[LocationId]		varchar(max)	NULL,
	[CreatedOn]			datetimeoffset  Null,
	[CreatedBy]			bigint			Null,
	[ModifiedOn]		datetimeoffset	Null,
	[ModifiedBy]		bigint			Null,
	[IsActive]			bit				Null
)