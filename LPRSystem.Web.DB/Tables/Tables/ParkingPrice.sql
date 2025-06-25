CREATE TABLE [data].[ParkingPrice]
(
	[ParkingPriceId]        BIGINT          NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Duration]              VARCHAR(MAX)    NULL,
	[Price]                 DECIMAL(10,2)   NULL,
	[CreatedBy]             BIGINT          NULL,
	[CreatedOn]             DATETIMEOFFSET  NULL,
	[ModifiedBy]            BIGINT          NULL,
	[ModifiedOn]            DATETIMEOFFSET  NULL,
	[IsActive]              BIT             NULL
)
