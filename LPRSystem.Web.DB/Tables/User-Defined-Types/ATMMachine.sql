CREATE TYPE [api].[ATMMachine] as Table
(
	[ATMId]				bigint			NULL,
	[ATMCode]			varchar(max)	NULL,
	[LocationId]		bigint			NULL,
	[CreatedOn]			datetimeoffset  Null,
	[CreatedBy]			bigint			Null,
	[ModifiedOn]		datetimeoffset	Null,
	[ModifiedBy]		bigint			Null,
	[IsActive]			bit				Null
)