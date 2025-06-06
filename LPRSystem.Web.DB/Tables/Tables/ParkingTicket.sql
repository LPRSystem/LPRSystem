CREATE TABLE [data].[ParkingTicket]
(
	[ParkingTicketId]		BIGINT			NOT NULL	PRIMARY KEY		IDENTITY(1,1),
	[ParkingTicketCode]		VARCHAR(MAX)	NULL,
	[ParkingTicketRefrence] VARCHAR(MAX)	NULL,
	[ParkedOn]				DATETIMEOFFSET	NULL,
	[ParkingDurationFrom]	DATETIMEOFFSET	NULL,
	[ParkingDurationTo]		DATETIMEOFFSET	NULL,
	[TotalDuration]			AS DATEDIFF(MINUTE, ParkingDurationFrom, ParkingDurationTo),
	[Price]					DECIMAL(10,2)	NULL,
	[VehicleNumber]			VARCHAR(MAX)	NULL,
	[PhoneNumber]			VARCHAR(MAX)	NULL,
	[CreatedBy]             BIGINT          NULL,
	[CreatedOn]             DATETIMEOFFSET  NULL,
	[ModifiedBy]            BIGINT          NULL,
	[ModifiedOn]            DATETIMEOFFSET  NULL,
	[IsActive]              BIT             NULL
)
