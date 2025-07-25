﻿CREATE TABLE [data].[ParkingTicketPayment]
(
	[ParkingTicketPaymentId]   BIGINT         NOT NULL  PRIMARY KEY   IDENTITY(1,1),
	[ParkingTicketId]          BIGINT         NULL,
	[ATMId]                    BIGINT         NULL,
	[PaymentMethodId]          BIGINT         NULL,
	[PaymentReference]         VARCHAR(max)   NULL,
	[TotalAmount]              DECIMAL(10,2)  NULL,
	[PaidAmount]               DECIMAL(10,2)  NULL,
	[DueAmount]                DECIMAL(10,2)  NULL,
	[Status]                   VARCHAR(max)   NULL,
	[CreatedBy]                BIGINT         NULL,
	[CreatedOn]                DATETIMEOFFSET NULL,
	[ModifiedBy]               BIGINT         NULL,
	[ModifiedOn]               DATETIMEOFFSET NULL,
	[IsActive]                 BIT            NULL
	FOREIGN KEY  (ParkingTicketId) REFERENCES data.ParkingTicket(ParkingTicketId),
	FOREIGN KEY  (ATMId) REFERENCES data.ATMMachine(ATMId),
	FOREIGN KEY  (PaymentMethodId) REFERENCES data.PaymentMethod(Id)
)
