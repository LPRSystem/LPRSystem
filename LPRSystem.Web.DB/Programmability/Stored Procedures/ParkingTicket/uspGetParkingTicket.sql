CREATE PROCEDURE [api].[uspGetParkingTicketById]
  (
     @ParkingTicketId BIGINT
  )
	WITH RECOMPILE

	AS

	BEGIN

	SELECT 
	      [ParkingTicketId],
		  [ATMId],
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [ParkingPriceId],
		  [VehicleNumber],
		  [PhoneNumber],
		  [IsExtended],
		  [ExtendedOn],
	      [ExtendedDurationFrom],
	      [ExtendedDurationTo],
	      [ActualAmount],
	      [ExtendedAmount],
	      [TotalAmount],
		  [Status],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]
		  FROM [data].[ParkingTicket]
		  WHERE 
		  ParkingTicketId=@ParkingTicketId
   END