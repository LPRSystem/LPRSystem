CREATE PROCEDURE [api].[uspGetATMById]
	(
	@ATMId  bigint
	)
	WITH RECOMPILE

	AS 

	BEGIN

	SELECT
	    ATMId
		,ParkingTicketId
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

    WHERE Id=@ATMId

    END