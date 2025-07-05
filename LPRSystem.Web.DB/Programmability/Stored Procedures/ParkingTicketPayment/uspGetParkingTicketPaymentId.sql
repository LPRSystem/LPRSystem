CREATE PROCEDURE [api].[uspGetParkingTicketPaymentId]
	(
	@ParkingTicketPaymentId  bigint
	)
	WITH RECOMPILE

	AS 

	BEGIN

	SELECT
	    ParkingTicketPaymentId
		,ParkingTicketId
		,ATMId
		,PaymentMethodId
		,PaymentReference
		,TotalAmount
		,PaidAmount
		,DueAmount
		,Status
		,CreatedBy
		,CreatedOn
		,ModifiedBy
		,ModifiedOn
		,IsActive

	FROM [data].[ParkingTicketPayment]

    WHERE Id=@ParkingTicketPaymentId

    END