CREATE PROCEDURE [api].[uspGetParkingTicketById]
	(
	@ParkingTicketId   bigint
	)
	WITH RECOMPILE

	AS 

	BEGIN

	SELECT
	    
		ParkingTicketId
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

    WHERE Id=@ParkingTicketId

    END