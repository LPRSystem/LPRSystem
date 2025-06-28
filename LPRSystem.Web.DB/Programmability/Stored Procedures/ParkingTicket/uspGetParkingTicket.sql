CREATE PROCEDURE [api].[uspGetParkingTicket]
	
	WITH RECOMPILE

	As
	BEGIN

	SELECT 
	      [ParkingTicketId],
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
   END