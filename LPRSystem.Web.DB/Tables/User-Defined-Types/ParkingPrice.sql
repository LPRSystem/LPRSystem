Create Type [api].[ParkingPrice] as Table
(
    [ParkingPriceId]        BIGINT          NULL,
	[Duration]              VARCHAR(MAX)    NULL,
	[Price]                 DECIMAL(10,2)   NULL,
	[CreatedBy]             BIGINT          NULL,
	[CreatedOn]             DATETIMEOFFSET  NULL,
	[ModifiedBy]            BIGINT          NULL,
	[ModifiedOn]            DATETIMEOFFSET  NULL,
	[IsActive]              BIT             NULL
)