CREATE TABLE [dbo].[ParkingTicketPayment]
(
	[ParkingTicketPaymentId]   BIGINT         NOT NULL  PRIMARY KEY   IDENTITY(1,1),
	[ParkingTicketId]          BIGINT         NULL,
	[ATMId]                    BIGINT         NULL,
	[PaymentMethodId]          BIGINT         NULL,
	[PaymentReference]         VARCHAR(max)   NULL,
	[TotalAmount]              DECIMAL        NULL,
	[PaidAmount]               DECIMAL        NULL,
	[DueAmount]                DECIMAL        NULL,
	[Status]                   VARCHAR(max)   NULL,
	[CreatedBy]                BIGINT         NULL,
	[CreatedOn]                DATETIMEOFFSET NULL,
	[ModifiedBy]               BIGINT         NULL,
	[ModifiedOn]               DATETIMEOFFSET NULL,
	[IsActive]                 BIT            NULL
)
