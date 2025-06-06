CREATE PROCEDURE [api].[uspInsertOrUpdateParkingTicket]
(
	@ParkingTicket [api].[ParkingTicket] READONLY
)
WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate datetimeoffset;
DECLARE @CurrentUser bigint;
Declare @RetrunData [api].[ParkingTicket];

set @CurrentDate = GETDATE(); 

Select @CurrentUser= ModifiedBy from  @ParkingTicket;

Merge INTO [data].[ParkingTicket] AS Target
Using (
   select input.[ParkingTicketId],
		  input.[ParkingTicketCode],
		  input.[ParkingTicketRefrence],
		  input.[ParkedOn],
		  input.[ParkingDurationFrom],
		  input.[ParkingDurationTo],
		  input.[TotalDuration],
		  input.[Price],
		  input.[VehicleNumber],
		  input.[PhoneNumber],
		  input.[CreatedBy],
		  input.[CreatedOn],
		  input.[ModifiedBy],
		  input.[ModifiedOn],
		  input.[IsActive]
		  FROM @ParkingTicket input
		  LEFT JOIN 
		  [data].[ParkingTicket] par
		  on input.[ParkingTicketId] = par.[ParkingTicketId]
          ) as source
		  ON target.[ParkingTicketId] = source.[ParkingTicketId]
		  WHEN MATCHED THEN
		  UPDATE
		  SET
		  [ParkingTicketId] = source.[ParkingTicketId],
		  [ParkingTicketCode]= source,
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [Price],
		  [VehicleNumber],
		  [PhoneNumber],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]
