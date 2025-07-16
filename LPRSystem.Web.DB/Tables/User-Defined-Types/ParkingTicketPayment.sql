Create TYPE [api].[ParkingTicketPayment] AS TABLE
(
    [ParkingTicketPaymentId]   BIGINT         NULL,
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
)