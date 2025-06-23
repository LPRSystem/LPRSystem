--CREATE PROCEDURE [api].[uspGetParkingTicket]
	
--	WITH RECOMPILE

--	As
--	BEGIN

--	SELECT 
--	      [ParkingTicketId],
--		  [ParkingTicketCode],
--		  [ParkingTicketRefrence],
--		  [ParkedOn],
--		  [ParkingDurationFrom],
--		  [ParkingDurationTo],
--		  [TotalDuration],
--		  [Price],
--		  [VehicleNumber],
--		  [PhoneNumber],
--		  [CreatedBy],
--		  [CreatedOn],
--		  [ModifiedBy],
--		  [ModifiedOn],
--		  [IsActive]
--		  FROM [data].[ParkingTicket]
--   END