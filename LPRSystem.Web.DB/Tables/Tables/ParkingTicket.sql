﻿CREATE TABLE [data].[ParkingTicket]
(
	[ParkingTicketId]		BIGINT			NOT NULL	PRIMARY KEY		IDENTITY(1,1),
	[ATMId]					BIGINT			NOT NULL,
	[ParkingTicketCode]		VARCHAR(MAX)	NULL,
	[ParkingTicketRefrence] VARCHAR(MAX)	NULL,
	[ParkedOn]				DATETIMEOFFSET	NULL,
	[ParkingDurationFrom]	DATETIMEOFFSET	NULL,
	[ParkingDurationTo]		DATETIMEOFFSET	NULL,
	[TotalDuration]			bigint          NULL,
    [ParkingPriceId]		bigint          NULL,
	[VehicleNumber]			VARCHAR(MAX)	NULL,
	[PhoneNumber]			VARCHAR(MAX)	NULL,
	[IsExtended]            BIT             NULL,
	[ExtendedOn]            DATETIMEOFFSET	NULL,
	[ExtendedDurationFrom]	DATETIMEOFFSET	NULL,
	[ExtendedDurationTo]	DATETIMEOFFSET	NULL,
	[ActualAmount]          DECIMAL(10,2)	NULL,
	[ExtendedAmount]        DECIMAL(10,2)	NULL,
	[TotalAmount]           DECIMAL(10,2)	NULL,
	[Status]                varchar(max)    NULL,
	[CreatedBy]             BIGINT          NULL,
	[CreatedOn]             DATETIMEOFFSET  NULL,
	[ModifiedBy]            BIGINT          NULL,
	[ModifiedOn]            DATETIMEOFFSET  NULL,
	[IsActive]              BIT             NULL
	FOREIGN KEY  (ParkingPriceId) REFERENCES data.ParkingPrice(ParkingPriceId),
	FOREIGN KEY  (ATMId) REFERENCES data.ATMMachine(ATMId)
)
