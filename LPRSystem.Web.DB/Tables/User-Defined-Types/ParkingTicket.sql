CREATE TYPE [api].[ParkingTicket] as TABLE
(
	[ParkingTicketId]				BIGINT		   NULL,
	[ParkingTicketCode]				Varchar(Max)   NULL,
	[ParkingTicketRefrence]			Varchar(Max)   NULL,
	[ParkedOn]						DATETIMEOFFSET NULL,
	[ParkingDurationFrom]			DATETIMEOFFSET NULL,
	[ParkingDurationTo]				DATETIMEOFFSET NULL,
	[TotalDuration]					AS DATEDIFF(MINUTE, ParkingDurationFrom, ParkingDurationTo),
	[Price]							DECIMAL(10,2)  NULL,
	[VehicleNumber]					VARCHAR(MAX)   NULL,
	[PhoneNumber]					VARCHAR(MAX)   NULL,
	[CreatedBy]						BIGINT          NULL,
	[CreatedOn]						DATETIMEOFFSET  NULL,
	[ModifiedBy]					BIGINT          NULL,
	[ModifiedOn]					DATETIMEOFFSET  NULL,
	[IsActive]						BIT             NULL
)